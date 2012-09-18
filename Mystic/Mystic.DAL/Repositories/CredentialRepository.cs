using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mystic.Common.DTO;

namespace Mystic.DAL.Repositories
{
    public class CredentialRepository : DalBase
    {

        public static Credential GetBy(int id)
        {
            var command = GetDbSprocCommand("Credential_GetById");
            command.Parameters.Add(CreateParameter("@id", id));
            return GetSingle<Credential>(ref command);
        }

        public static Credential GetBy(string emailAddress)
        {
            var command = GetDbSprocCommand("Credential_GetByEmailAddress");
            command.Parameters.Add(CreateParameter("@emailAddress", emailAddress));
            return GetSingle<Credential>(ref command);
        }

    }
}
