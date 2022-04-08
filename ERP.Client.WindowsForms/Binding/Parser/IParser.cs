using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Binding.Parser
{
    public interface IParser
    { 
        public Type Type1 { get; }
        public Type Type2 { get; }

        public bool CanParse(Object Object, Type TargetType);

        public Object Parse(Object Object, Type TargetType);

        public static Object ParseAny(Object Object, Type TargetType)
        {
            if (!parsers.Any()) { FillParsers(); }
            if (parsers.FirstOrDefault(o => o.CanParse(Object, TargetType)) is IParser Parser)
            {
                return Parser.Parse(Object, TargetType);
            }
            throw new ErpException("No parser found");
        }

        private static List<IParser> parsers { get; } = new List<IParser>();

        private static void FillParsers()
        {
            parsers.Add(new Parser<string, int>(o => int.Parse(o), o => o.ToString()));
            parsers.Add(new Parser<string, string>(o => o, o => o));
        }

        public static IParser GetParser(Type Type1, Type Type2) 
        {
            if (!parsers.Any()) { FillParsers(); }
            if (parsers.FirstOrDefault(o => (o.Type1 == Type1 && o.Type2 == Type2) || o.Type2 == Type1 && o.Type1 == Type2) is IParser Parser)
            {
                return Parser;
            }
            return null;
        }

    }
}
