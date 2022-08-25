using ERP.Business.Objects.Attributes;
using ERP.Test.Client.WebApp.Shared;
using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;
using ERP.Web.Razor.Components.Base;
using ERP.Web.Razor.Components.Bindables;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                IEnumerable<BaseComponent> childs = GetChilds<BaseComponent>(nameof(NewPerson));
                if (childs.Any())
                {
                    foreach (BaseComponent child in childs)
                    {
                        if (child is IBaseBindableComponent BBC)
                        {
                            BBC.Reset();
                        }
                    }
                }
            }
        }

        public Person Person { get; set; }

        public bool tesbool { get; set; }

        private string CanSave { get => NewPersonAnyError ? "disabled" : null; }

        private bool NewPersonAnyError { get => GetChilds<TextInput>(nameof(NewPerson)).Any(o => o.Error); }

        private IEnumerable<Person> PersonList { get => Client.GetObjects(Guid.Empty, Client.GetList(Guid.Empty).Select(o => o).ToArray()).OrderBy(o => o.Firstname).ThenBy(o => o.Name).ThenBy(o => o.Birthday); }

        private IEnumerable<(PropertyInfo PI, ShowGUIAttribute SGA)> PersonPropertyTuple { get => typeof(Person).GetProperties().Select<PropertyInfo, (PropertyInfo PI, ShowGUIAttribute SGA)>(o => new(o, o.GetCustomAttribute<ShowGUIAttribute>())).Where(o => o.SGA != null).OrderBy(o => o.SGA.ID); }

        public void New()
        {
            NewPerson = new();
        }

        protected override void OnChildAdded(BaseComponent Child)
        {
            base.OnChildAdded(Child);
            if (Child is BaseComponent BC && BC.Group == nameof(NewPerson) && BC is IBaseBindableComponent BBC)
            {
                BBC.DestinationValueChanged += (s, e) =>
                {
                    foreach (BaseComponent iBC in GetChilds<BaseComponent>(BC.Group))
                    {
                        if (iBC is IBaseBindableComponent iBBC && iBBC.PropertyName != BBC.PropertyName)
                        {
                            refresh = false;
                            iBC.Refresh();
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