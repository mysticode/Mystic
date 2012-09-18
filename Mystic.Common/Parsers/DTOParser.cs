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
