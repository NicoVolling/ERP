using System.ComponentModel;

namespace ERP.Client.WindowsForms.Controls.BindableControls
{
    public class BindableCollectionPanel : Panel
    {
        private int? fixDescriptionWidth = null;

        [Category("Darstellung")]
        public int? FixedDescriptionWidth
        { get => fixDescriptionWidth; set { fixDescriptionWidth = value; OnFixedDescriptionWidthChanged(); } }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is BindableControl Bindable)
            {
                Bindable.FixedDescriptionWidth = FixedDescriptionWidth;
            }
        }

        private void OnFixedDescriptionWidthChanged()
        {
            foreach (Control Control in Controls)
            {
                if (Control is BindableControl Bindable)
                {
                    Bindable.FixedDescriptionWidth = FixedDescriptionWidth;
                }
            }
        }
    }
}