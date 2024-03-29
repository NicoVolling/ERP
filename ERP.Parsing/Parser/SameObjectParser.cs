﻿using ERP.Business.Objects.Attributes;

namespace ERP.Parsing.Parser
{
    public class SameObjectParser<T> : IParser
    {
        public SameObjectParser()
        {
            this.IsDefault1 = o => o is null;
            this.GetDefault1 = () => default;
        }

        public SameObjectParser(Func<T, bool> IsDefault, Func<T> GetDefault)
        {
            this.IsDefault1 = IsDefault;
            this.GetDefault1 = GetDefault;
        }

        public Type Type1 => typeof(Object);

        public Type Type2 => typeof(Object);

        private Func<T> GetDefault1 { get; set; }

        private Func<T, bool> IsDefault1 { get; set; }

        public bool CanParse(object Object, Type TargetType)
        {
            return Object.GetType().Equals(TargetType);
        }

        public object GetDefault(Type TargeType)
        {
            return GetDefault1();
        }

        public bool IsDefault(object Object)
        {
            return Object is T Obj && IsDefault1(Obj);
        }

        public object Parse(object Object, Type TargetType, ShowGUIAttribute ShowGUIAttribute, out bool Error)
        {
            Error = false;
            return Object;
        }
    }
}