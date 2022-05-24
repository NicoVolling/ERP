using ERP.Business.Objects;
using System.Globalization;

namespace ERP.Parsing.Parser
{
    public interface IParser
    {
        public Type Type1 { get; }
        public Type Type2 { get; }

        private static List<IParser> parsers { get; } = new List<IParser>();

        public static void AddParser(IParser Parser)
        {
            parsers.Add(Parser);
        }

        public static IParser GetParser(Type Type1, Type Type2)
        {
            if (!parsers.Any()) { FillParsers(); }
            if (parsers.FirstOrDefault(o => o.Fits(Type1, Type2)) is IParser Parser)
            {
                return Parser;
            }
            if (Type1 == Type2 || Type1 == null || Type2 == null) { return new SameObjectParser<Object>(); }
            return null;
        }

        public bool CanParse(Object Object, Type TargetType);

        public bool Fits(Type Type1, Type Type2)
        {
            return (Type1 == this.Type1 && Type2 == this.Type2) || (Type1 == this.Type2 && Type2 == this.Type1);
        }

        public Object GetDefault(Type TargeType);

        public bool IsDefault(Object Object);

        public Object Parse(Object Object, Type TargetType, string FormatOptions, out bool Error);

        private static void FillParsers()
        {
            Func<int, bool> IsInt32Default = (i) =>
            {
                return i == -1;
            };
            Func<Object> GetInt32Default = () => -1;

            Func<Guid, bool> IsGuidDefault = (o) =>
            {
                return o == Guid.Empty;
            };
            Func<Object> GetGuidDefault = () => Guid.Empty;

            Func<double, bool> IsDoubleDefault = (i) =>
            {
                return i == -1;
            };
            Func<Object> GetDoubleDefault = () => -1;

            Func<DateOnly, DateOnly, bool> DateOnlyComparer = (a, b) => a.CompareTo(b) == 0;
            Func<TimeOnly, TimeOnly, bool> TimeOnlyComparer = (a, b) => a.CompareTo(b) == 0;
            Func<DateTime, DateTime, bool> DateTimeOnlyComparer = (a, b) => a.CompareTo(b) == 0;

            AddParser(new StringParser<int>(
                (o, fo) => int.Parse(o),
                IsInt32Default,
                GetInt32Default
                ));

            AddParser(new StringParser<Guid>(
                (o, fo) => Guid.Parse(o),
                IsGuidDefault,
                GetGuidDefault
                ));

            AddParser(new StringParser<double>(
                (o, fo) => double.Parse(o),
                (o, fo) => string.IsNullOrEmpty(fo) ? o.ToString("N2") : o.ToString(fo),
                IsDoubleDefault,
                GetDoubleDefault
                ));

            AddParser(new StringParser());

            AddParser(new StringParser<DateOnly>(
                (o, fo) => DateOnly.Parse(o),
                (o, fo) => string.IsNullOrEmpty(fo) ? o.ToString("dd.MM.yyyy") : o.ToString(fo),
                DateOnlyComparer
                ));

            AddParser(new StringParser<bool>(
                (o, fo) => bool.Parse(o),
                (o, fo) => o.ToString(),
                o => o == false,
                () => false
                ));

            AddParser(new StringParser<TimeOnly>(
                (o, fo) => TimeOnly.Parse(o),
                (o, fo) => string.IsNullOrEmpty(fo) ? o.ToString("HH:mm") : o.ToString(fo),
                TimeOnlyComparer
                ));

            AddParser(new StringParser<DateTime>(
                (o, fo) => DateTime.Parse(o),
                (o, fo) => string.IsNullOrEmpty(fo) ? o.ToString("dd.MM.yyyy HH:mm") : o.ToString(fo),
                DateTimeOnlyComparer
            ));

            AddParser(new SameObjectParser<BusinessObject>(
                o => o.IsEmpty(),
                () => BusinessObject.Empty
            ));
        }
    }
}