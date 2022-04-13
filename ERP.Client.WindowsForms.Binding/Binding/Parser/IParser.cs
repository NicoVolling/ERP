using ERP.Business.Objects;
using ERP.Exceptions.ErpExceptions;

namespace ERP.Client.WindowsForms.Binding.Parser
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
            if (parsers.FirstOrDefault(o => (o.Type1 == Type1 && o.Type2 == Type2) || o.Type2 == Type1 && o.Type1 == Type2) is IParser Parser)
            {
                return Parser;
            }
            if (Type1 == Type2) { return new SameObjectParser<Object>(); }
            return null;
        }

        public bool CanParse(Object Object, Type TargetType);

        public Object GetDefault(Type TargeType);

        public bool IsDefault(Object Object, Type TargetType);

        public Object Parse(Object Object, Type TargetType);

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
                o => int.Parse(o),
                IsInt32Default,
                GetInt32Default
                ));

            AddParser(new StringParser<Guid>(
                o => Guid.Parse(o),
                IsGuidDefault,
                GetGuidDefault
                ));

            AddParser(new StringParser<double>(
                o => double.Parse(o),
                o => o.ToString("N2"),
                IsDoubleDefault,
                GetDoubleDefault
                ));

            AddParser(new StringParser());

            AddParser(new StringParser<DateOnly>(
                o => DateOnly.Parse(o),
                o => o.ToString("dd.MM.yyyy"),
                DateOnlyComparer
                ));

            AddParser(new StringParser<bool>(
                o => bool.Parse(o),
                o => o.ToString(),
                o => o == false,
                () => false
                ));

            AddParser(new StringParser<TimeOnly>(
                o => TimeOnly.Parse(o),
                o => o.ToString("dd.MM.yyyy"),
                TimeOnlyComparer
                ));

            AddParser(new StringParser<DateTime>(
                o => DateTime.Parse(o),
                o => o.ToString("dd.MM.yyyy"),
                DateTimeOnlyComparer
            ));

            AddParser(new SameObjectParser<BusinessObject>(
                o => o.IsEmpty(),
                () => BusinessObject.Empty
            ));
        }
    }
}