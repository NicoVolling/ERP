﻿using ERP.Business.Objects.Attributes;

namespace ERP.Parsing.Parser
{
    public class StringParser<T2> : Parser<string, T2>
    {
        public StringParser(Func<string, ShowGUIAttribute, T2> OnParse1To2) : base(OnParse1To2, T2ToString, IsStringDefault, GetStringDefault)
        {
        }

        public StringParser(Func<string, ShowGUIAttribute, T2> OnParse1To2, Func<T2, T2, bool> Compare2) : base(OnParse1To2, T2ToString, Compare2, IsStringDefault, GetStringDefault)
        {
        }

        public StringParser(Func<string, ShowGUIAttribute, T2> OnParse1To2, Func<T2, bool> IsDefault2, Func<object> GetDefault2) : base(OnParse1To2, T2ToString, IsStringDefault, IsDefault2, GetStringDefault, GetDefault2)
        {
        }

        public StringParser(Func<string, ShowGUIAttribute, T2> OnParse1To2, Func<T2, T2, bool> Compare2, Func<T2, bool> IsDefault2, Func<object> GetDefault2) : base(OnParse1To2, T2ToString, Compare2, IsStringDefault, IsDefault2, GetStringDefault, GetDefault2)
        {
        }

        public StringParser(Func<string, ShowGUIAttribute, T2> OnParse1To2, Func<T2, ShowGUIAttribute, string> OnParse2To1) : base(OnParse1To2, OnParse2To1, IsStringDefault, GetStringDefault)
        {
        }

        public StringParser(Func<string, ShowGUIAttribute, T2> OnParse1To2, Func<T2, ShowGUIAttribute, string> OnParse2To1, Func<T2, T2, bool> Compare2) : base(OnParse1To2, OnParse2To1, Compare2, IsStringDefault, GetStringDefault)
        {
        }

        public StringParser(Func<string, ShowGUIAttribute, T2> OnParse1To2, Func<T2, ShowGUIAttribute, string> OnParse2To1, Func<T2, bool> IsDefault2, Func<object> GetDefault2) : base(OnParse1To2, OnParse2To1, IsStringDefault, IsDefault2, GetStringDefault, GetDefault2)
        {
        }

        public StringParser(Func<string, ShowGUIAttribute, T2> OnParse1To2, Func<T2, ShowGUIAttribute, string> OnParse2To1, Func<T2, T2, bool> Compare2, Func<T2, bool> IsDefault2, Func<object> GetDefault2) : base(OnParse1To2, OnParse2To1, Compare2, IsStringDefault, IsDefault2, GetStringDefault, GetDefault2)
        {
        }

        public static Func<Object> GetStringDefault { get; } = () => string.Empty;

        public static Func<string, bool> IsStringDefault { get; } = (s) =>
          {
              return (s is string str && str.Length == 0) || s is null;
          };

        public static Func<T2, ShowGUIAttribute, string> T2ToString { get; } = (o, fo) => o.ToString();
    }

    public class StringParser : StringParser<string>
    {
        public StringParser() : base((o, fo) => o, IsStringDefault, GetStringDefault)
        {
        }
    }
}