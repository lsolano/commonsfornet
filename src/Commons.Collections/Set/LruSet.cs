﻿// Copyright CommonsForNET
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
using Commons.Collections.Map;

namespace Commons.Collections.Set
{
    [CLSCompliant(true)]
    public class LruSet<T> : AbstractHashedSet<T>, IStrictSet<T>, IReadOnlyStrictSet<T>
    {
        public LruSet()
            : base(new LruMap<T, object>())
        {
        }

        public LruSet(int fullSize)
            : base(new LruMap<T, object>(fullSize))
        {
        }

        public LruSet(int fullSize, IEqualityComparer<T> comparer)
            : base(new LruMap<T, object>(fullSize, comparer))
        {
        }

        public LruSet(int fullSize, Equator<T> equator)
            : base(new LruMap<T, object>(fullSize, equator))
        {
        }
    }
}
