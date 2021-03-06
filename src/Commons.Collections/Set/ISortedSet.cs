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
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Collections.Set
{
    /// <summary>
    /// Stub interface for tree set data structure.
    /// </summary>
    /// <typeparam name="T">The type of the set items.</typeparam>
    [CLSCompliant(true)]
    public interface ISortedSet<T> : IStrictSet<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// Removes the minimum value in the set.
        /// </summary>
        void RemoveMin();

        /// <summary>
        /// Removes the maximum value in the set.
        /// </summary>
        void RemoveMax();

        /// <summary>
        /// Retrieves the maximum value in the set.
        /// </summary>
        T Max { get; }

        /// <summary>
        /// Retrieves the minimum value in the set.
        /// </summary>
        T Min { get; }

        bool IsEmpty { get; }
    }
}
