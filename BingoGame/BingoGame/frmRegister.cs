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
    public partial class frmRegister : Form
    {
        #region Ctors
        public frmRegister()
        {
            InitializeComponent();
            this.MdiParent = GLOBALS.MainForm;
            this.AcceptButton = btnRegister;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
        }
        #endregion

        #region Events
        private void frmRegister_Load(object sender, EventArgs e)
        {
            btnRegister.Click += new EventHandler(Register_Click);
            tsbCancel.Click += tsbCancel_Click;
            number.IsNumberOnly = true;
            password.IsPassword = true;
            confirm.IsPassword = true;

            GLOBALS.MainForm.DisableStrips();
            if (GLOBALS.CurrentAcct != null)
                LoadCurrent();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            GLOBALS.MainForm.EnableStrips();
            this.Close();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            bool blank = ValidateBlank();
            if (!blank)
                MessageBox.Show("Missing Input!");
            else
            {
                bool pw = ValidatePW();
                if (!pw)
                    MessageBox.Show("Password does not match!");
                else
                {
                    bool email = ValidateEmail();
                    if (!email)
                        MessageBox.Show("Incorrect Email!");
                    else
                    {
                        int tempID = GLOBALS.CurrentAcct == null ? GLOBALS.RegisteredAccts.Count + 1 : GLOBALS.CurrentAcct.PlayerID;

                        Account acct = new Account()
                        {
                            PlayerID = tempID,
                            Username = username.Text,
                            Password = password.Text,
                            Firstname = fname.Text,
                            Middlename = mname.Text,
                            Lastname = lname.Text,
                            Birthdate = dtpBdate.Value,
                            Email = email_.Text,
                            Sms = number.Text
                        };

                        MailService mail = new MailService(email_.Text);
                        SMSService sms = new SMSService(number.Text);
                        AccountProcessor ap = new AccountProcessor();

                        ap.AccountProcessed += mail.OnAccountRegistered;
                        ap.AccountProcessed += sms.OnAccountRegistered;
                        ap.RegisterAccount(acct);

                        GLOBALS.ReadFile();
                        GLOBALS.MainForm.EnableStrips();
                        this.Close();
                    }
                }
            }

        }
        #endregion

        #region Methods
        private void LoadCurrent()
        {
            username.Text = GLOBALS.CurrentAcct.Username;
            password.Text = GLOBALS.CurrentAcct.Password;
            fname.Text = GLOBALS.CurrentAcct.Firstname;
            mname.Text = GLOBALS.CurrentAcct.Middlename;
            lname.Text = GLOBALS.CurrentAcct.Lastname;
            dtpBdate.Value = GLOBALS.CurrentAcct.Birthdate;
            email_.Text = GLOBALS.CurrentAcct.Email;
            number.Text = GLOBALS.CurrentAcct.Sms;
        }

        private bool ValidatePW()
        {
            return password.Text == confirm.Text;
        }

        private bool ValidateEmail()
        {
            return email_.Text.Contains("@") && email_.Text.Contains(".com");
        }

        private bool ValidateBlank()
        {
            return username.Text.Length > 0 && password.Text.Length > 0 && confirm.Text.Length > 0 && fname.Text.Length > 0 && mname.Text.Length > 0 && lname.Text.Length > 0 && email_.Text.Length > 0 && number.Text.Length > 0;
        }
        #endregion
    }
}
