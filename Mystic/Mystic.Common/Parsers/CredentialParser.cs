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
    public class CredentialParser : DTOBaseParser
    {
        private class Ordinals
        {
            public int EmailAddress { get; set; }
            public int Salt { get; set; }
            public int PasswordHash { get; set; }
            public int CreateDateTimeUtc { get; set; }
            public int FailedLoginAttempts { get; set; }
            public int LastLockoutDateTimeUtc { get; set; }
            public int IsEmailAddressVerified { get; set; }
        }

        private readonly Ordinals _ordinals;

        public CredentialParser(System.Data.IDataReader reader)
            : base(reader)
        {
            _ordinals = Populate<Ordinals>(reader);
        }
        public override DTOBase PopulateDTO(System.Data.IDataReader reader)
        {
            var credential = base.PopulateId(reader, new Credential());
            if (!reader.IsDBNull(_ordinals.EmailAddress)) { credential.EmailAddress = reader.GetString(_ordinals.EmailAddress); }
            if (!reader.IsDBNull(_ordinals.Salt)) { credential.Salt = reader.GetString(_ordinals.Salt); }
            if (!reader.IsDBNull(_ordinals.PasswordHash)) { credential.PasswordHash = reader.GetString(_ordinals.PasswordHash); }
            if (!reader.IsDBNull(_ordinals.CreateDateTimeUtc)) { credential.CreateDateTimeUtc = reader.GetDateTime(_ordinals.CreateDateTimeUtc); }
            credential.IsNew = false;
            return credential;
        }
    }
}
