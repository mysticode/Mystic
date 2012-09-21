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
using Mystic.Common.DTO;

namespace Mystic.Common.Parsers
{
    public abstract class DTOBaseParser : DTOParser
    {
        private class Ordinals
        {
            public int Id { get; set; }
        }

        private readonly Ordinals _ordinals;

        public DTOBaseParser(System.Data.IDataReader reader)
        {
            _ordinals = base.Populate<Ordinals>(reader);
        }
        protected virtual T PopulateId<T>(System.Data.IDataReader reader, T t) where T : DTOBase
        {
            if (!reader.IsDBNull(_ordinals.Id))
            {
                t.Id = reader.GetInt32(_ordinals.Id);
            }

            return t;
        }
    }
}
