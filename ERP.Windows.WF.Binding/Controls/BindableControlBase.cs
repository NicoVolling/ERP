using ERP.Business.Objects;
using ERP.Windows.WF.Base;
using ERP.Windows.WF.Binding.Components;
using ERP.Windows.WF.Binding.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Windows.WF.Binding.Controls
{
    public partial class BindableControlBase : UserControl
    {
        private int descriptionWidth;
        private string info;
        private InputStatus status;
        private string userFriendlyName;

        public BindableControlBase()
        {
            InitializeComponent();
        }

        public event EventHandler AfterBound;

        public event EventHandler<BusinessObject> ButtonClick;

        public event EventHandler ControlValueChanged;

        public event EventHandler FormatRequest;

        public event EventHandler StatusChanged;

        [Category("Bindung")]
        public bool Access
        {
            get => !lbl_access.Visible; set
            {
                lbl_access.Visible = !value;
                ControlPanel.Visible = value;
                panel_button.Enabled = value;
            }
        }

        [Category("Bindung")]
        public string BindingDestination { get; set; }

        [Category("Bindung")]
        public int DescriptionWidth
        {
            get => descriptionWidth;
            set
            {
                descriptionWidth = value; if (descriptionWidth != 0)
                {
                    lbl_description.Width = descriptionWidth;
                }
            }
        }

        [Category("Bindung")]
        public bool ReadOnly
        { get => GetReadOnly(); set { SetReadOnly(value); } }

        [Category("Bindung")]
        public bool SearchButtonActive { get => panel_button.Visible; set => panel_button.Visible = value; }

        [Category("Bindung")]
        public SelectionDialogStarter SelectionDialogStarter { get; set; }

        [Category("Bindung")]
        public InputStatus Status
        {
            get => status;
            set
            {
                bool changed = status != value;
                status = value;
                if (changed)
                {
                    OnStatusChanged();
                }
            }
        }

        [Category("Bindung")]
        public bool StatusVisible { get => panel_led.Visible; set => panel_led.Visible = value; }

        [Category("Bindung")]
        public string UserFriendlyName
        {
            get => userFriendlyName;
            set
            {
                userFriendlyName = value;
                if (string.IsNullOrEmpty(userFriendlyName))
                {
                    lbl_description.Visible = false;
                }
                else
                {
                    lbl_description.Visible = true;
                }
                lbl_description.Text = $"{userFriendlyName}:";
            }
        }

        public int GetUserfriendlyNameWidth()
        {
            if (string.IsNullOrEmpty(UserFriendlyName)) { return 0; }
            return (int)lbl_description.CreateGraphics().MeasureString(lbl_description.Text, lbl_description.Font).Width + lbl_description.Padding.Left + lbl_description.Padding.Right;
        }

        public void OnBound(DataContext DataContext)
        {
            if (GetControl() is Control Control)
            {
                Control.Enter += (s, e) => { FocusChanged(true); };
                Control.Leave += (s, e) => { FocusChanged(false); };
                Control.GotFocus += (s, e) => { FocusChanged(true); };
                Control.LostFocus += (s, e) => { FocusChanged(false); };
                FocusChanged(Control.Focused);
            }
            AfterBound?.Invoke(this, EventArgs.Empty);
        }

        public void SetUserfriendlyNameWidth(int Value)
        {
            lbl_description.Width = Value;
        }

        protected void FocusChanged(bool Focused)
        {
            if (Status == InputStatus.Error && !Focused)
            {
                if (MessageBox.Show("Achtung!\nIn dem aktuell ausgewählten Eingabefeld liegt ein Fehler vor.\nMöchten Sie abbrechen, Ihre Eingabe wiederholen oder Fortfahren?", "Fehler in Eingabefeld", MessageBoxButtons.CancelTryContinue, MessageBoxIcon.Exclamation) is DialogResult DR)
                {
                    if (DR == DialogResult.Cancel)
                    {
                        Format();
                    }
                    else if (DR == DialogResult.TryAgain)
                    {
                        GetControl()?.Select();
                    }
                    else if (DR == DialogResult.Continue)
                    {
                        if (Focused)
                        {
                            this.BackColor = StatusLed.ForeColor;
                        }
                        else
                        {
                            this.BackColor = MainPanel.BackColor;
                        }
                    }
                }
            }
            else
            {
                if (Focused)
                {
                    this.BackColor = StatusLed.ForeColor;
                }
                else
                {
                    this.BackColor = MainPanel.BackColor;
                }
                if (Status != InputStatus.Error)
                {
                    Format();
                }
            }
        }

        protected void Format()
        {
            FormatRequest?.Invoke(this, EventArgs.Empty);
        }

        protected Control GetControl()
        {
            if (this is IBindable Bindable)
            {
                return Bindable.GetMainControl();
            }
            return null;
        }

        protected virtual bool GetReadOnly()
        {
            if (GetControl() is Control Control)
            {
                if (Control.GetType().GetProperty("ReadOnly") is PropertyInfo PI)
                {
                    try
                    {
                        return (bool)PI.GetValue(Control);
                    }
                    catch { }
                }
            }
            return false;
        }

        protected virtual void OnControlValueChanged()
        {
            ControlValueChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            this.Padding = new Padding(3, 0, 0, 0);
        }

        protected virtual void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
            if (status == InputStatus.OK)
            {
                StatusLed.ForeColor = Color.LimeGreen;
            }
            else if (status == InputStatus.Error)
            {
                StatusLed.ForeColor = Color.Red;
            }
            else if (status == InputStatus.Null)
            {
                StatusLed.ForeColor = Color.White;
            }

            if (GetControl()?.Focused ?? false)
            {
                this.BackColor = StatusLed.ForeColor;
            }
            else
            {
                this.BackColor = MainPanel.BackColor;
            }
        }

        protected virtual void SetReadOnly(bool Value)
        {
            if (GetControl() is Control Control)
            {
                if (Control.GetType().GetProperty("ReadOnly") is PropertyInfo PI)
                {
                    try
                    {
                        PI.SetValue(Control, Value);
                        if (Value)
                        {
                            Control.ForeColor = Color.LightGray;
                            Control.BackColor = Color.FromArgb(35, 35, 35);
                        }
                        else
                        {
                            Control.ForeColor = Control.Parent.ForeColor;
                            Control.BackColor = Control.Parent.BackColor;
                        }
                    }
                    catch { }
                }
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (SelectionDialogStarter != null)
            {
                SelectionDialogStarter.DataContext = (this.FindForm() as BindingForm)?.DataContext;
                if (SelectionDialogStarter.DataContext != null)
                {
                    BusinessObject BO = SelectionDialogStarter.OpenDialog();
                    ButtonClick?.Invoke(this, BO);
                }
                else
                {
                    MessageBox.Show("Fehler: DataContext konnte nicht gesetzt werden.");
                }
            }
            else
            {
                ButtonClick?.Invoke(this, null);
            }
        }
    }
}