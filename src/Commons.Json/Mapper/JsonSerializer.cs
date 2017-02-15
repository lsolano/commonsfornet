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
using System.Text;

namespace Commons.Json.Mapper
{
	public class JsonSerializer
    {
		public Func<object, string> BooleanSerializer { get; set; }
		public Func<object, string> NumberSerializer { get; set; }
		public Func<object, string> NullSerializer { get; set; }
		public Func<object, string> ArraySerializer { get; set; }
		public Func<object, string> DictSerializer { get; set; }
		public Func<object, string> EnumerableSerializer { get; set; }
		public Func<object, string> ObjectSerializer { get; set; }
		public Func<object, string> TimeSerializer { get; set; }
		public Func<object, string> StringSerializer { get; set; }
    }
}
