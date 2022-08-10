using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Web.Razor.Components.Base
{
    public partial class BaseComponent : ComponentBase
    {
        public BaseComponent()
        {
            SetChildAction = (o) =>
            {
                if (ChildList is null)
                {
                    ChildList = new();
                }
                ChildList.Add(o);
                OnChildAdded(o);
            };
        }

        #region Parameters

        [Parameter]
        public string Group { get; set; }

        [Parameter]
        [EditorRequired]
        public Action<BaseComponent> SetChild { private get; init; }

        #endregion Parameters

        public List<BaseComponent> ChildList { get; private set; }

        protected BaseComponent Parent { get; private set; }

        protected Action<BaseComponent> SetChildAction { get; init; }

        public virtual void Refresh()
        {
            InvokeAsync(StateHasChanged);
        }

        public void RefreshChilds(string Group)
        {
            foreach (BaseComponent TI in GetChilds<BaseComponent>(Group))
            {
                TI.Refresh();
            }
        }

        protected IEnumerable<T> GetChilds<T>(string Group = null) where T : BaseComponent
        {
            Func<BaseComponent, bool> GroupCondition = o => true;
            if (Group != null)
            {
                GroupCondition = o => o.Group == Group;
            }
            if (ChildList is null)
            {
                return new List<T>();
            }
            return ChildList.Where(o => o is T).Where(GroupCondition).Cast<T>();
        }

        protected virtual void OnChildAdded(BaseComponent Child)
        {
            Child.Parent = this;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetChild?.Invoke(this);
        }
    }
}