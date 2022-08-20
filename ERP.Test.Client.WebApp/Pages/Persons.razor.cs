using ERP.Business.Objects.Attributes;
using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;
using ERP.Web.Razor.Components.Base;
using ERP.Web.Razor.Components.Bindables;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace ERP.Test.Client.WebApp.Pages
{
    public partial class Persons : BaseComponent
    {
        private Person newPerson;

        private bool refresh = true;
        public PersonClient Client { get; private set; }

        public Person NewPerson
        {
            get => newPerson;
            set
            {
                newPerson = value;
                RefreshChilds(nameof(NewPerson));
            }
        }

        public Person Person { get; set; }

        public bool tesbool { get; set; }

        private string CanSave { get => NewPersonAnyError ? "disabled" : null; }

        private bool NewPersonAnyError { get => GetChilds<TextInput>(nameof(NewPerson)).Any(o => o.Error); }

        private IEnumerable<Person> PersonList { get => Client.GetObjects(Guid.Empty, Client.GetList(Guid.Empty).Select(o => o.ID).ToArray()); }

        private IEnumerable<(PropertyInfo PI, ShowGUIAttribute SGA)> PersonPropertyTuple { get => typeof(Person).GetProperties().Select<PropertyInfo, (PropertyInfo PI, ShowGUIAttribute SGA)>(o => new(o, o.GetCustomAttribute<ShowGUIAttribute>())).Where(o => o.SGA != null).OrderBy(o => o.SGA.ID); }

        public void New()
        {
            NewPerson = new();
        }

        protected override void OnChildAdded(BaseComponent Child)
        {
            base.OnChildAdded(Child);
            if (Child is TextInput TI && TI.Group == nameof(NewPerson))
            {
                TI.DestinationValueChanged += (s, e) =>
                {
                    foreach (BaseComponent BC in GetChilds<BaseComponent>(TI.Group))
                    {
                        if ((BC is TextInput tI && tI.PropertyName != TI.PropertyName) || BC is not TextInput)
                        {
                            refresh = false;
                            BC.Refresh();
                            refresh = true;
                        }
                    }
                };
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            NewPerson = new();
            Client = new();
        }

        private void Delete(Guid ID)
        {
            Client.Delete(Guid.Empty, ID);
            Refresh();
        }

        private void Edit(Guid ID)
        {
            if (Client.GetExistence(Guid.Empty, ID))
            {
                Client.GetData(Guid.Empty, ID);
                NewPerson = Client.Data;
            }
        }

        private void Save()
        {
            try
            {
                if (!NewPersonAnyError)
                {
                    if (Client.GetExistence(Guid.Empty, NewPerson.ID))
                    {
                        Client.Change(Guid.Empty, NewPerson.ID, NewPerson);
                    }
                    else
                    {
                        Client.Create(Guid.Empty, NewPerson);
                    }
                    NewPerson = new();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}