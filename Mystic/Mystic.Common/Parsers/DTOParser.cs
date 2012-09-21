/*
 * Copyright (C) 2012 The Mysticode Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mystic.Common.DTO;

namespace Mystic.Common.Parsers
{
    public abstract class DTOParser
    {
        protected sealed class DTOParserCache
        {
            private static readonly Lazy<DTOParserCache> _lazy = new Lazy<DTOParserCache>(() => new DTOParserCache());

            public Dictionary<Type, object> Ordinals = new Dictionary<Type, object>();

            private DTOParserCache()
            {

            }

            public static DTOParserCache Instance { get { return _lazy.Value; } }
        }

        // TODO: Verify performance.
        protected virtual T Populate<T>(IDataReader reader)
        {
            object generic;
            var type = typeof(T);
            if (DTOParserCache.Instance.Ordinals.ContainsKey(type))
            {
                generic = DTOParserCache.Instance.Ordinals[type];
            }
            else
            {
                generic = Activator.CreateInstance<T>();
                foreach (var property in type.GetProperties())
                {
                    int ordinal = reader.GetOrdinal(property.Name);
                    property.SetValue(generic, ordinal, null);
                }
                DTOParserCache.Instance.Ordinals.Add(type, generic);
            }
            return (T)generic;
        }

        public abstract DTOBase PopulateDTO(IDataReader reader);
    }
}
