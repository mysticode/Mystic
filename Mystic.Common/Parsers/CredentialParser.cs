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
