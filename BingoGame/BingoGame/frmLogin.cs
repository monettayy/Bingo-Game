#region Needed Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BingoGame.Classes;
#endregion

namespace BingoGame
{
    public partial class frmLogin : Form
    {
        #region Fields || Properties || Ctors
        public frmLogin()
        {
            InitializeComponent();
            this.MdiParent = GLOBALS.MainForm;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancel;
        }
        #endregion

        #region Events
        private void frmLogin_Load(object sender, EventArgs e)
        {
            btnCancel.Click += btnCancel_Click;
            linkRegister.Click += linkRegister_Click;
            txtPassword.Enter += txtEnter;
            txtPassword.TextChanged += password_Enter;
            btnLogin.Click += btnLogin_Click;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Account a = GLOBALS.AccountCheck(txtUsername.Text, txtPassword.Text);

            if (a != null)
            {
                MessageBox.Show("Welcome " + a.Firstname + "!");
                GLOBALS.CurrentAcct = a;
                GLOBALS.MainForm.EnableStrips();
                this.Close();
            }
            else
                MessageBox.Show("Account does not exist!");
        }

        private void txtEnter(object sender, EventArgs e)
        {
            txtPassword.Text = "";
        }

        private void password_Enter(object sender, EventArgs e)
        {
            txtPassword.IsPassword = txtPassword.Text.Length == 0 || txtPassword.Text == "" ? false : true;
        }

        private void linkRegister_Click(object sender, EventArgs e)
        {
            new frmRegister().Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GLOBALS.MainForm.Close();
        }
        #endregion
    }
}
