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
using Commons.Collections.Queue;
using System.Threading;
using Commons.Utils;

namespace Test.Commons.Collections
{
    public class DefaultHandler<T> : IMessageHandler<T>
    {
        public void Handle(T message)
        {
            Thread.Sleep(10);
            RequestCount++;
        }

        public int RequestCount { get; set; }
    }

    public class ConcurrentHandler<T> : IMessageHandler<T>
    {
        private AtomicInt32 requestCount;
        public void Handle(T message)
        {
            requestCount.Increment();
        }

        public int RequestCount
        {
            get
            {
                return requestCount.Value;
            }
        }
    }
}
