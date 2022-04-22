using ERP.Windows.WF.Binding.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Windows.WF.Binding.Controls
{
    public class BindableControlPanel : Panel
    {
        public BindableControlPanel()
        {
        }

        public void HandleControlAdded(Control Control)
        {
            Control.ControlAdded += (s, e) => { HandleControlAdded(e.Control); };
            foreach (Control Control1 in Control.Controls)
            {
                HandleControlAdded(Control1);
            }
            if (Control is BindableControlBase BCB)
            {
                BCB.AfterBound += (s, e) =>
                {
                    if (BCB.ParentForm is BindingForm BF)
                    {
                        int maxWidth = 0;

                        foreach (IBindable Bindable in BF.BindingMaster.Bindables)
                        {
                            if (Bindable is BindableControlBase BCB)
                            {
                                if (BCB.GetUserfriendlyNameWidth() is int i && i > maxWidth)
                                {
                                    maxWidth = i;
                                }
                            }
                        }

                        foreach (IBindable Bindable in BF.BindingMaster.Bindables)
                        {
                            if (Bindable is BindableControlBase BCB)
                            {
                                BCB.SetUserfriendlyNameWidth(maxWidth + 2);
                            }
                        }
                    }
                };
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            HandleControlAdded(e.Control);
        }
    }
}