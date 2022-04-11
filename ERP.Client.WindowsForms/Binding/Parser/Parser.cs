using ERP.Exceptions.ErpExceptions;

namespace ERP.Client.WindowsForms.Binding.Parser
{
    public class Parser<T1, T2> : IParser
    {
        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1)
        {
            Func<T1, T1, bool> Compare1;
            Func<T2, T2, bool> Compare2;
            Func<T1, bool> IsDefault1;
            Func<T2, bool> IsDefault2;
            DefaultComparer(out Compare1, out Compare2);
            DefaultDefaultFunc(out IsDefault1, out IsDefault2);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                () => default(T1),
                () => default(T2));
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T1, T1, bool> Compare1, Func<T2, T2, bool> Compare2)
        {
            Func<T1, bool> IsDefault1;
            Func<T2, bool> IsDefault2;
            DefaultDefaultFunc(out IsDefault1, out IsDefault2);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                () => default(T1),
                () => default(T2));
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T1, T1, bool> Compare1)
        {
            Func<T1, T1, bool> tmp;
            Func<T2, T2, bool> Compare2;
            Func<T1, bool> IsDefault1;
            Func<T2, bool> IsDefault2;
            DefaultComparer(out tmp, out Compare2);
            DefaultDefaultFunc(out IsDefault1, out IsDefault2);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                () => default(T1),
                () => default(T2));
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T2, T2, bool> Compare2)
        {
            Func<T1, T1, bool> Compare1;
            Func<T2, T2, bool> tmp;
            Func<T1, bool> IsDefault1;
            Func<T2, bool> IsDefault2;
            DefaultComparer(out Compare1, out tmp);
            DefaultDefaultFunc(out IsDefault1, out IsDefault2);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                () => default(T1),
                () => default(T2));
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T1, bool> IsDefault1, Func<T2, bool> IsDefault2, Func<Object> GetDefault1, Func<Object> GetDefault2)
        {
            Func<T1, T1, bool> Compare1;
            Func<T2, T2, bool> Compare2;
            DefaultComparer(out Compare1, out Compare2);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                GetDefault1,
                GetDefault2);
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T1, bool> IsDefault1, Func<Object> GetDefault1)
        {
            Func<T1, T1, bool> Compare1;
            Func<T2, T2, bool> Compare2;
            Func<T1, bool> tmp;
            Func<T2, bool> IsDefault2;
            DefaultComparer(out Compare1, out Compare2);
            DefaultDefaultFunc(out tmp, out IsDefault2);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                GetDefault1,
                () => default(T2));
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T2, bool> IsDefault2, Func<Object> GetDefault2)
        {
            Func<T1, T1, bool> Compare1;
            Func<T2, T2, bool> Compare2;
            Func<T1, bool> IsDefault1;
            Func<T2, bool> tmp;
            DefaultComparer(out Compare1, out Compare2);
            DefaultDefaultFunc(out IsDefault1, out tmp);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                () => default(T1),
                GetDefault2);
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T1, T1, bool> Compare1, Func<T1, bool> IsDefault1, Func<Object> GetDefault1)
        {
            Func<T1, T1, bool> tmp1;
            Func<T2, T2, bool> Compare2;
            Func<T1, bool> tmp;
            Func<T2, bool> IsDefault2;
            DefaultComparer(out tmp1, out Compare2);
            DefaultDefaultFunc(out tmp, out IsDefault2);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                GetDefault1,
                () => default(T2));
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T2, T2, bool> Compare2, Func<T2, bool> IsDefault2, Func<Object> GetDefault2)
        {
            Func<T1, T1, bool> Compare1;
            Func<T2, T2, bool> tmp1;
            Func<T1, bool> IsDefault1;
            Func<T2, bool> tmp;
            DefaultComparer(out Compare1, out tmp1);
            DefaultDefaultFunc(out IsDefault1, out tmp);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                () => default(T1),
                GetDefault2);
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T1, T1, bool> Compare1, Func<T2, bool> IsDefault2, Func<Object> GetDefault2)
        {
            Func<T1, T1, bool> tmp1;
            Func<T2, T2, bool> Compare2;
            Func<T1, bool> IsDefault1;
            Func<T2, bool> tmp;
            DefaultComparer(out tmp1, out Compare2);
            DefaultDefaultFunc(out IsDefault1, out tmp);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                () => default(T1),
                GetDefault2);
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T2, T2, bool> Compare2, Func<T1, bool> IsDefault1, Func<Object> GetDefault1)
        {
            Func<T1, T1, bool> Compare1;
            Func<T2, T2, bool> tmp1;
            Func<T1, bool> tmp;
            Func<T2, bool> IsDefault2;
            DefaultComparer(out Compare1, out tmp1);
            DefaultDefaultFunc(out tmp, out IsDefault2);
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                GetDefault1,
                () => default(T2));
        }

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T1, T1, bool> Compare1, Func<T2, T2, bool> Compare2, Func<T1, bool> IsDefault1, Func<T2, bool> IsDefault2, Func<Object> GetDefault1, Func<Object> GetDefault2)
        {
            Ctor(OnParse1To2,
                OnParse2To1,
                Compare1,
                Compare2,
                PrepareIsDefault(IsDefault1),
                PrepareIsDefault(IsDefault2),
                GetDefault1,
                GetDefault2);
        }

        public Func<T1, T2> OnParse1To2 { get; private set; }
        public Func<T2, T1> OnParse2To1 { get; private set; }
        public Type Type1 { get => typeof(T1); }

        public Type Type2 { get => typeof(T2); }
        private Func<Object, Object, bool> Compare1 { get; set; }

        private Func<Object, Object, bool> Compare2 { get; set; }

        private Func<Object> GetDefault1 { get; set; }
        private Func<Object> GetDefault2 { get; set; }
        private Func<Object, bool> IsDefault1 { get; set; }

        private Func<Object, bool> IsDefault2 { get; set; }

        public bool CanParse(Object Object, Type TargetType)
        {
            bool canParse = false;

            canParse = canParse || (TargetType == typeof(T2) && (Object is T1 || IsDefault1(Object)));

            canParse = canParse || (TargetType == typeof(T1) && (Object is T2 || IsDefault2(Object)));

            return canParse;
        }

        public Object GetDefault(Type TargeType)
        {
            if (TargeType == Type1) { return GetDefault1(); }
            if (TargeType == Type2) { return GetDefault2(); }
            return null;
        }

        public bool IsDefault(Object Object, Type TargetType)
        {
            if (Object is T1 t1)
            {
                return IsDefault1(t1);
            }
            if (Object is T2 t2)
            {
                return IsDefault2(t2);
            }
            return false;
        }

        public Object Parse(Object Object, Type TargetType)
        {
            if (CanParse(Object, TargetType))
            {
                if (IsDefault1(Object) && Type2 == TargetType)
                {
                    return GetDefault2();
                }
                if (IsDefault2(Object) && Type1 == TargetType)
                {
                    return GetDefault1();
                }
                if (Object is T1 T1)
                {
                    return OnParse1To2(T1);
                }

                if (Object is T2 T2)
                {
                    return OnParse2To1(T2);
                }
            }
            throw new ErpException("Parser incompatible");
        }

        private static void DefaultComparer(out Func<T1, T1, bool> Compare1, out Func<T2, T2, bool> Compare2)
        {
            Compare1 = (a, b) =>
            {
                if (a is T1 O1 && b is T1 O2) { return O1.Equals(O2); }
                return b == null && a == null;
            };

            Compare2 = (a, b) =>
            {
                if (a is T2 O1 && b is T2 O2) { return O1.Equals(O2); }
                return b == null && a == null;
            };
        }

        private static Func<Object, bool> PrepareIsDefault<T>(Func<T, bool> IsDefault)
        {
            return o => (o is T Obj && IsDefault(Obj)) || o is null;
        }

        private void Ctor(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1, Func<T1, T1, bool> Compare1, Func<T2, T2, bool> Compare2, Func<Object, bool> IsDefault1, Func<Object, bool> IsDefault2, Func<Object> GetDefault1, Func<Object> GetDefault2)
        {
            this.OnParse1To2 = OnParse1To2;
            this.OnParse2To1 = OnParse2To1;
            this.IsDefault1 = IsDefault1;
            this.IsDefault2 = IsDefault2;
            this.GetDefault1 = GetDefault1;
            this.GetDefault2 = GetDefault2;
            this.Compare1 = (a, b) =>
            {
                if (a is T1 O1 && b is T1 O2) { return Compare1(O1, O2); }
                return b == null && a == null;
            };
            this.Compare2 = (a, b) =>
            {
                if (a is T2 O1 && b is T2 O2) { return Compare2(O1, O2); }
                return b == null && a == null;
            };
        }

        private void DefaultDefaultFunc(out Func<T1, bool> IsDefault1, out Func<T2, bool> IsDefault2)
        {
            IsDefault1 = o => o is T1 T1 && Compare1(T1, GetDefault1());

            IsDefault2 = o => o is T2 T2 && Compare2(T2, GetDefault2());
        }
    }
}