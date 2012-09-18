using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mystic.Common.DTO
{
    public abstract class DTOBase
    {
        // TODO: Let's not expose these setters to the whole world. Figure this out. It may mean moving the Parsers* back
        // into the Common library.
        public virtual int Id { get; set; }
        public virtual bool IsNew { get; set; }
    }   
}
