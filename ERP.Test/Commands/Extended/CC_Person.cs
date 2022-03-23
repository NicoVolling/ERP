using ERP.BaseLib.Objects;
using ERP.Business.Server;
using ERP.Test.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.Commands.Extended
{
    public class CC_Person : BusinessObjectServer<Person>
    {

        private List<Person> list = new List<Person>(); 

        public override Result Change(User User, int ID, Person Data)
        {
            if(!ServerSide) { return GetClientResult(User, ID, Data); }
            Person Person = GetObjectByID(list, ID);
            Person.Deserialize(Data);
            return Result.OK;
        }

        public override Result Create(User User, Person Data)
        {
            if (!ServerSide) { return GetClientResult(User, Data); }
            list.Add(Data);
            Data.ID = list.Max(o => o.ID) + 1;
            return new Result(Data.Serialize());
        }

        public override Result Delete(User User, int ID)
        {
            if (!ServerSide) { return GetClientResult(User, ID); }
            Person Person = GetObjectByID(list, ID);
            list.Remove(Person);
            return Result.OK;
        }

        public override Result GetData(User User, int ID)
        {
            if (!ServerSide) { return GetClientResult(User, ID); }
            Person Person = GetObjectByID(list, ID);
            return new Result(Person.Serialize());
        }

        public override Result GetList(User User)
        {
            if (!ServerSide) { return GetClientResult(User); }
            return new Result(list.Select(o => o.Serialize()));
        }
    }
}
