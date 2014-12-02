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

using System.Runtime.CompilerServices;
using Commons.Utils;

namespace Commons.Collections.Map
{
	public class ReferenceMap<K, V> : AbstractHashMap<K, V>
	{
		private const int DefaultCapacity = 16;
		private static readonly Equator<K> referenceEquator = (x1, x2) => ReferenceEquals(x1, x2);
		public ReferenceMap() : base(DefaultCapacity, referenceEquator)
		{
		}

		public ReferenceMap(int capacity) : base(capacity, referenceEquator)
		{
		}

		protected override long HashIndex(K key)
		{
			var hash = RuntimeHelpers.GetHashCode(key);
			return hash & (Capacity - 1);
		}
	}
}
