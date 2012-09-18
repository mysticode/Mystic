using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mystic.Common.DTO
{
    public class Credential : DTOBase
    {
        public string EmailAddress { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreateDateTimeUtc { get; set; }
    }
}
