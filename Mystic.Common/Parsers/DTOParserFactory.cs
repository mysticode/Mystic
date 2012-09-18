using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mystic.Common.DTO;

namespace Mystic.Common.Parsers
{
    public static class DTOParserFactory
    {
        public static DTOParser GetParserOf<T>(System.Data.IDataReader reader)
        {
            if (typeof(T) == typeof(Credential))
            {
                return new CredentialParser(reader);
            }

            return null;
        }
    }
}
