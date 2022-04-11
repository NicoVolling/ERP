using ERP.Client.WindowsForms.Binding;
using ERP.Client.WindowsForms.Binding.Parser;
using System.Windows.Forms;

namespace ERP.Client.WindowsForms.Controls.BindableControls
{
    public partial class B_TextBox : BindableControl
    {
        private bool textchanged = true;

        public B_TextBox()
        {
            InitializeComponent();
        }

        public TextBox TextBox { get => textBox1; }

        protected override void OnBound()
        {
            textBox1.TextChanged += (s, e) =>
            {
                if (textchanged)
                {
                    textchanged = false;

                    if (!Parser.IsDefault(ParseToObject(textBox1.Text), TargetType) || textBox1.Text.Length == 0 || HasError)
                    {
                        Status = BindingStatus.Unsaved;
                        SaveData();
                    }
                    else
                    {
                        textchanged = true;
                        textBox1.Text = Parser.GetDefault(typeof(string)).ToString();
                    }
                    textchanged = true;
                }
            };
        }

        protected override IParser OnGetParser()
        {
            return IParser.GetParser(typeof(string), TargetType);
        }

        protected override void OnLoadData()
        {
            textBox1.Text = Get()?.ToString();
            Status = string.IsNullOrEmpty(textBox1.Text) ? BindingStatus.NullOrDefault : BindingStatus.Saved;
        }

        protected override void OnSaveData()
        {
            Set(textBox1.Text);
        }
    }
}