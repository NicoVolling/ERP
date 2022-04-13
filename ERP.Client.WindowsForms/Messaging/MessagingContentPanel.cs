using ERP.Client.WindowsForms.Controls.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Client.WindowsForms.Messaging
{
    public partial class MessagingContentPanel : ContentPanel
    {
        public MessagingContentPanel()
        {
            InitializeComponent();
        }

        public string Message { get => lbl_message.Text; set => lbl_message.Text = value; }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.BaseWindow.Close();
        }
    }
}