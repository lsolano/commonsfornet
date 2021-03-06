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

namespace Commons.Collections.Queue
{
    /// <summary>
    /// A priority queue with minimum item on the top.
    /// </summary>
    /// <typeparam name="T">The type of the priority queue items.</typeparam>
    [CLSCompliant(true)]
    public class MinPriorityQueue<T> : AbstractPriorityQueue<T>, IPriorityQueue<T>
    {
        public MinPriorityQueue()
            : this(Comparer<T>.Default)
        {
        }

        public MinPriorityQueue(IComparer<T> comparer)
            : this(comparer.Compare)
        {
        }

        public MinPriorityQueue(Comparison<T> comparer) : this (null, comparer)
        {
        }

        public MinPriorityQueue(IEnumerable<T> items, Comparison<T> comparer) : base(items, (x1, x2) => comparer(x1, x2) < 0)
        {
        }
    }
}
