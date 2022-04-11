using ERP.Exceptions.ErpExceptions;

namespace ERP.Client.WindowsForms.Binding.Parser
{
    public interface IParser
    {
        public Type Type1 { get; }
        public Type Type2 { get; }

        private static List<IParser> parsers { get; } = new List<IParser>();

        public static IParser GetParser(Type Type1, Type Type2)
        {
            if (!parsers.Any()) { FillParsers(); }
            if (parsers.FirstOrDefault(o => (o.Type1 == Type1 && o.Type2 == Type2) || o.Type2 == Type1 && o.Type1 == Type2) is IParser Parser)
            {
                return Parser;
            }
            return null;
        }

        public static Object ParseAny(Object Object, Type TargetType)
        {
            if (!parsers.Any()) { FillParsers(); }
            if (parsers.FirstOrDefault(o => o.CanParse(Object, TargetType)) is IParser Parser)
            {
                return Parser.Parse(Object, TargetType);
            }
            throw new ErpException("No parser found");
        }

        public bool CanParse(Object Object, Type TargetType);

        public Object GetDefault(Type TargeType);

        public bool IsDefault(Object Object, Type TargetType);

        public Object Parse(Object Object, Type TargetType);

        private static void FillParsers()
        {
            Func<string, bool> IsStringDefault = (s) =>
            {
                return (s is string str && str.Length == 0) || s is null;
            };
            Func<Object> GetStringDefault = () => string.Empty;

            Func<int, bool> IsInt32Default = (i) =>
            {
                return i == -1;
            };
            Func<Object> GetInt32Default = () => -1;

            Func<DateTime, DateTime, bool> DateTimeComparer = (a, b) => a.CompareTo(b) == 0;
            Func<Object> GetDateTimeDefault = () => new DateTime();

            parsers.Add(new Parser<string, int>(
                o => int.Parse(o),
                o => o.ToString(),
                IsStringDefault,
                IsInt32Default,
                GetStringDefault,
                GetInt32Default));

            parsers.Add(new Parser<string, string>(
                o => o,
                o => o,
                IsStringDefault,
                IsStringDefault,
                GetStringDefault,
                GetStringDefault));

            parsers.Add(new Parser<string, DateTime>(
                o => DateTime.Parse(o),
                o => o.ToString("dd.MM.yyyy"),
                DateTimeComparer,
                IsStringDefault,
                GetStringDefault));
        }
    }
}