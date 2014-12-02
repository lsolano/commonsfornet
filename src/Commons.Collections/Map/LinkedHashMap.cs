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
using System.Collections.Generic;
using Commons.Utils;

namespace Commons.Collections.Map
{
    [CLSCompliant(true)]
    public class LinkedHashMap<K, V> : AbstractHashMap<K, V>, IOrderedMap<K, V>
    {
        private const int DefaultCapacity = 16;

        private LinkedHashEntry Header { get; set; }

        public LinkedHashMap()
            : this(DefaultCapacity)
        {
        }

        public LinkedHashMap(int capacity)
            : this(capacity, (x1, x2) => x1 == null ? x2 == null : x1.Equals(x2))
        {
        }

        public LinkedHashMap(int capacity, IEnumerable<KeyValuePair<K, V>> items)
            : this(capacity, items, (x1, x2) => x1 == null ? x2 == null : x1.Equals(x2))
        {
        }

        public LinkedHashMap(int capacity, Equator<K> equator)
            : this(capacity, null, equator)
        {
        }

        public LinkedHashMap(int capacity, IEnumerable<KeyValuePair<K, V>> items, Equator<K> equator)
            : base(capacity, equator)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    Add(item);
                }
            }
        }

        protected override long HashIndex(K key)
        {
            return key.GetHashCode() & (Capacity - 1);
        }

        public override bool Remove(K key)
        {
            Guarder.CheckNull(key);
            var removed = false;
			var entry = GetEntry(key) as LinkedHashEntry;
            if (entry != null)
            {
                if (base.Remove(key))
                {
                    entry.Before.After = entry.After;
                    entry.After.Before = entry.Before;
                    entry.Before = null;
                    entry.After = null;
                    removed = true;
                }
            }
            return removed;
        }

        public KeyValuePair<K, V> First
        {
            get
            {
                CheckEmpty("get first item");
                return new KeyValuePair<K, V>(Header.Key, Header.Value);
            }
        }

        public KeyValuePair<K, V> Last
        {
            get
            {
                CheckEmpty("get last item");
                return new KeyValuePair<K, V>(Header.Before.Key, Header.Before.Value);
            }
        }

        public KeyValuePair<K, V> After(K key)
        {
            Guarder.CheckNull(key);
            CheckEmpty("get After item");

			var entry = GetEntry(key) as LinkedHashEntry;
			if (entry == null)
			{
				throw new ArgumentException("The key does not exist.");
			}
			if (IsEqual(entry.Key, Header.Before.Key))
			{
				throw new ArgumentException("There is no more items after the searched key.");
			}

            return new KeyValuePair<K, V>(entry.After.Key, entry.After.Value);
        }

        public KeyValuePair<K, V> Before(K key)
        {
            Guarder.CheckNull(key);
            CheckEmpty("get Before item");
			var entry = GetEntry(key) as LinkedHashEntry;
			if (entry == null)
			{
				throw new ArgumentException("The key does not exist.");
			}
			if (IsEqual(entry.Key, Header.Key))
			{
				throw new ArgumentException("There is no item before the searched key.");
			}

            return new KeyValuePair<K, V>(entry.Before.Key, entry.Before.Value);
        }
 
        public KeyValuePair<K, V> GetIndex(int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("The order cannot be less than zero");
            }
            if (index >= Count)
            {
                throw new ArgumentException("The order is larger than the item count");
            }
            CheckEmpty("get item from index");

            var cursor = Header;
            if (index < (Count / 2))
            {
                for (var i = 0; i < index; i++)
                {
                    cursor = cursor.After;
                }
            }
            else
            {
                for (var i = Count; i > Count - index; i--)
                {
                    cursor = cursor.Before;
                }
            }

            return new KeyValuePair<K, V>(cursor.Key, cursor.Value);
        }

        public override IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
			return CreateEnumerator().GetEnumerator();
        }

		protected override void Rehash()
		{
			if (Header != null)
			{
				var newEntries = new HashEntry[Capacity];
				Count = 0;
				var cursor = Header;
				var oldHeader = Header;
				Header = null;
				Put(newEntries, CreateEntry(cursor.Key, cursor.Value));
				cursor = cursor.After;
				while (!ReferenceEquals(cursor, oldHeader))
				{
					Put(newEntries, CreateEntry(cursor.Key, cursor.Value));
					cursor = cursor.After;
				}
				Entries = newEntries;
			}
		}

        protected override AbstractHashMap<K, V>.HashEntry CreateEntry(K key, V value)
        {
            var linkedEntry = new LinkedHashEntry(key, value);
            if (Header == null)
            {
                Header = linkedEntry;
                Header.Before = Header.After = Header;
            }
            else
            {
                linkedEntry.After = Header;
                linkedEntry.Before = Header.Before;
                Header.Before.After = linkedEntry;
                Header.Before = linkedEntry;
            }

            return linkedEntry;
        }

		private IEnumerable<KeyValuePair<K, V>> CreateEnumerator()
		{
            var cursor = Header;
            if (null != cursor)
            {
				do
				{
					yield return new KeyValuePair<K, V>(cursor.Key, cursor.Value);
					cursor = cursor.After;
				} while (!ReferenceEquals(cursor, Header));
            }
		}

        private void CheckEmpty(string message)
        {
            if (null == Header)
            {
                throw new InvalidOperationException(string.Format("Invalid to {0}, the map is empty", message));
            }
        }

        protected class LinkedHashEntry : HashEntry
        {
            public LinkedHashEntry(K key, V value) : base(key, value)
            {
            }
            public LinkedHashEntry Before { get; set; }
            public LinkedHashEntry After { get; set; }
        }
    }
}
