﻿// Copyright CommonsForNET.
// Licensed to the Apache Software Foundation (ASF) under one or more
// contributor license agreements. See the NOTICE file distributed with
// this work for additional information regarding copyright ownership.
// The ASF licenses this file to You under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using System;
using System.Text;
using Commons.Collections.Collection;
using Commons.Utils;

namespace Commons.Collections.Map
{
    [CLSCompliant(true)]
    public class Customized32HashedMap<K, V> : AbstractHashedMap<K, V>, IDictionary<K, V>
#if NET45
        , IReadOnlyDictionary<K, V>
#endif
    {
        private const int DefaultCapacity = 64;

        private readonly Transformer<K, byte[]> transform;

        private readonly IHashStrategy hasher;

        public Customized32HashedMap() : this(DefaultCapacity)
        {
        }

        public Customized32HashedMap(int capacity)
            : this(capacity, x => Encoding.ASCII.GetBytes(x.ToString()))
        {
        }

        public Customized32HashedMap(int capacity, Transformer<K, byte[]> transformer)
            : this(capacity, new MurmurHash32(), transformer, EqualityComparer<K>.Default.Equals)
        {
        }

        public Customized32HashedMap(int capacity, Transformer<K, byte[]> transformer, IEqualityComparer<K> comparer) 
            : this(capacity, new MurmurHash32(), transformer, comparer.Equals)
        {
            transform = transformer;
        }

        public Customized32HashedMap(int capacity, Transformer<K, byte[]> transformer, Equator<K> isEqual)
            : this(capacity, new MurmurHash32(), transformer, isEqual)
        {
        }

        public Customized32HashedMap(int capacity, IHashStrategy hasher, Transformer<K, byte[]> transformer, Equator<K> isEqual)
            : base(capacity, new EquatorComparer<K>(isEqual))
        {
            transform = transformer;
            this.hasher = hasher;
        }

        public Customized32HashedMap(IDictionary<K, V> items, IHashStrategy hasher, Transformer<K, byte[]> transformer, Equator<K> isEqual)
            : this(items == null ? DefaultCapacity : items.Count, hasher, transformer, isEqual)
        {
            Guarder.CheckNull(hasher, transformer, isEqual);
            if (null != items)
            {
                foreach (var item in items)
                {
                    Add(item.Key, item.Value);
                }
            }
        }

        private long Hash(K key)
        {
            var bytes = transform(key);
            var hash = hasher.Hash(bytes);
            return hash[0];
        }

        protected override long HashIndex(K key)
        {
            var hash = Hash(key);
            return hash & (Capacity - 1);
        }

    }
}
