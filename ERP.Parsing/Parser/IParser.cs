using ERP.Business.Objects;
using ERP.Business.Objects.Attributes;
using System.Globalization;

namespace ERP.Parsing.Parser
{
    public interface IParser
    {
        public Type Type1 { get; }

        public Type Type2 { get; }

        public bool CanParse(Object Object, Type TargetType);

        public bool Compare(Object Object1, Object Object2);

        public bool Fits(Type Type1, Type Type2)
        {
            return (Type1 == this.Type1 && Type2 == this.Type2) || (Type1 == this.Type2 && Type2 == this.Type1);
        }

        public Object GetDefault(Type TargeType);

        public bool IsDefault(Object Object);

        public Object Parse(Object Object, Type TargetType, ShowGUIAttribute ShowGUIAttribute, out bool Error);
    }
}