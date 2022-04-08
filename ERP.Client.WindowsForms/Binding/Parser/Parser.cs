using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Binding.Parser
{
    public class Parser<T1, T2> : IParser
    {

        public Type Type1 { get => typeof(T1); }

        public Type Type2 { get => typeof(T2); }

        private Object defaultT1;

        private Object defaultT2;

        public Parser(Func<T1, T2> OnParse1To2, Func<T2, T1> OnParse2To1) 
        {
            this.OnParse1To2 = OnParse1To2;
            this.OnParse2To1 = OnParse2To1;
            this.defaultT1 = default(T1);
            this.defaultT2 = default(T2);
        }

        public Object Parse(Object Object, Type TargetType) 
        {
            if (CanParse(Object, TargetType))
            {
                if (Object is T1 T1) return OnParse1To2(T1);
                if (Object is T2 T2) return OnParse2To1(T2);
                if(Object == defaultT1 && Type2 == TargetType) 
                {
                    return default(T2);
                }
                if (Object == defaultT2 && Type1 == TargetType)
                {
                    return default(T1);
                }
            }
            throw new ErpException("Parser incompatible");
        }

        public bool CanParse(Object Object, Type TargetType) 
        {
            bool canParse = false;

            canParse = canParse || (TargetType == typeof(T2) && (Object is T1 || Object == defaultT1));

            canParse = canParse || (TargetType == typeof(T1) && (Object is T2 || Object == defaultT2));

            return canParse;
        }

        public Func<T1, T2> OnParse1To2 { get; init; }

        public Func<T2, T1> OnParse2To1 { get; init; }
    }
}
