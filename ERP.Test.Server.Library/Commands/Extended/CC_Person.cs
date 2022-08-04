using ERP.Business.Server;
using ERP.Test.Public.Library.Objects;

namespace ERP.Test.Server.Library.Commands.Extended
{
    public class CC_Person : BusinessObjectServer<Person>
    {
        protected override IEnumerable<Person> OnGenerateSampleData()
        {
            return new List<Person>()
            {
                new() { Firstname = "Nico", Name = "Volling", Birthday = new DateOnly(2000, 03, 27)}
            };
        }
    }
}