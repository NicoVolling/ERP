using ERP.BaseLib.Serialization;
using ERP.Business.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Parsing.Parser
{
    public static class ParsingMaster
    {
        private static List<IParser> parsers { get; } = new List<IParser>();

        public static void AddParser(this Type Type1, IParser Parser)
        {
            AddParser(Parser);
        }

        public static IParser GetParser(this Type Type1, Type Type2)
        {
            if (!parsers.Any()) { FillParsers(); }
            if (parsers.FirstOrDefault(o => o.Fits(Type1, Type2)) is IParser Parser)
            {
                return Parser;
            }
            if (Type1 == Type2 || Type1 == null || Type2 == null) { return new SameObjectParser<Object>(); }
            return null;
        }

        private static void AddParser(IParser Parser)
        {
            parsers.Add(Parser);
        }

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

            //TODO: TryParse for all.
            AddParser(new StringParser<int>(
                (o, fo) => int.Parse(string.IsNullOrEmpty(fo?.Suffix) ? o : o.Replace(fo.Suffix, "")),
                (o, fo) => o.ToString() + fo?.Suffix,
                IsInt32Default,
                GetInt32Default
                ));

            AddParser(new StringParser<Guid>(
                (o, fo) => Guid.Parse(o),
                IsGuidDefault,
                GetGuidDefault
                ));

            AddParser(new StringParser<double>(
                (o, fo) => double.Parse(string.IsNullOrEmpty(fo?.Suffix) ? o : o.Replace(fo.Suffix, "")),
                (o, fo) => (string.IsNullOrEmpty(fo?.FormatOptions) ? o.ToString("N2") : o.ToString(fo.FormatOptions)) + fo?.Suffix,
                IsDoubleDefault,
                GetDoubleDefault
                ));

            AddParser(new StringParser());

            AddParser(new StringParser<DateOnly>(
                (o, fo) => DateOnly.Parse(string.IsNullOrEmpty(fo?.Suffix) ? o : o.Replace(fo.Suffix, "")),
                (o, fo) => (string.IsNullOrEmpty(fo?.FormatOptions) ? o.ToString("dd.MM.yyyy") : o.ToString(fo.FormatOptions)) + fo?.Suffix,
                DateOnlyComparer
                ));

            AddParser(new StringParser<bool>(
                (o, fo) => o.ToLower().Equals("true"),
                (o, fo) => o ? "true" : "false",
                o => o == false,
                () => false,
                () => "false"
                ));

            AddParser(new StringParser<TimeOnly>(
                (o, fo) => TimeOnly.Parse(string.IsNullOrEmpty(fo?.Suffix) ? o : o.Replace(fo.Suffix, "")),
                (o, fo) => (string.IsNullOrEmpty(fo?.FormatOptions) ? o.ToString("HH:mm") : o.ToString(fo.FormatOptions)) + fo?.Suffix,
                TimeOnlyComparer
                ));

            AddParser(new StringParser<DateTime>(
                (o, fo) => DateTime.Parse(string.IsNullOrEmpty(fo?.Suffix) ? o : o.Replace(fo.Suffix, "")),
                (o, fo) => (string.IsNullOrEmpty(fo?.FormatOptions) ? o.ToString("dd.MM.yyyy HH:mm") : o.ToString(fo.FormatOptions)) + fo?.Suffix,
                DateTimeOnlyComparer
            ));

            AddParser(new SameObjectParser<BusinessObject>(
                o => o.IsEmpty(),
                () => BusinessObject.Empty
            ));

            AddParser(new StringParser<Guid>(
                (o, fo) => Guid.Parse(o),
                (o, fo) => o.ToString()
                ));
        }
    }
}