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
