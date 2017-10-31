#region Needed Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace fornaleza_finalExam
{
    public partial class myTextBox : TextBox
    {
        private bool isNumberOnly;
        [
            Description("Accept Number only"),
            Category("Validation"),
            DefaultValueAttribute(typeof(bool)),
            Browsable(true)
        ]

        public bool IsNumberOnly
        {
            get { return isNumberOnly; }
            set { isNumberOnly = value; }
        }

        private bool isPassword;
        [
            Description("For password type"),
            Category("Validation"),
            DefaultValueAttribute(typeof(bool)),
            Browsable(true)
        ]

        public bool IsPassword
        {
            get { return isPassword; }
            set { isPassword = value; }
        }

        public myTextBox()
        {
            this.GotFocus += new EventHandler(tGotFocus);
            this.LostFocus += new EventHandler(tLeaveFocus);
            this.TextChanged += new EventHandler(tTextChange);
            this.KeyPress += new KeyPressEventHandler(tKeyPress);
            this.KeyUp += new KeyEventHandler(tKeyUp);
        }

        #region Events
        private void tGotFocus(object sender, EventArgs e)
        {
            this.BackColor = Color.SkyBlue;
        }

        private void tLeaveFocus(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void tKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = this.isNumberOnly && !Char.IsDigit(e.KeyChar) ? true : false;
        }

        private void tKeyUp(object sender, KeyEventArgs e)
        {
            if(this.isPassword)
                this.PasswordChar = '*';
        }

        private void tTextChange(object sender, EventArgs e)
        {
            if (this.isPassword)
                this.PasswordChar = '*';
        }
        #endregion
    }
}
