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

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Commons.Messaging.Cache
{
    [CLSCompliant(false)]
    public class ContextCache : ICache<long, HttpContext>
    {
        private ConcurrentDictionary<long, HttpContext> contexts = new ConcurrentDictionary<long, HttpContext>();

        public void Add(long sequence, HttpContext context)
        {
            if (contexts.ContainsKey(sequence))
            {
                throw new InvalidOperationException("The sequence number is already added.");
            }
            contexts[sequence] = context;
        }

        public bool Contains(long key)
        {
            return contexts.ContainsKey(key);
        }

        public HttpContext From(long sequence)
        {
            if (contexts.ContainsKey(sequence))
            {
                return contexts[sequence];
            }
            return null;
        }

        public IEnumerator<HttpContext> GetEnumerator()
        {
            foreach(var kvp in contexts)
            {
                yield return kvp.Value;
            }
        }

        public void Remove(long sequence)
        {
            HttpContext context;
            if (!contexts.TryRemove(sequence, out context))
            {
                throw new InvalidOperationException("The sequence number does not exist in the context cache.");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}