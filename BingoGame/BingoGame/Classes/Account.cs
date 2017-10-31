#region Needed Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace BingoGame.Classes
{
    class Account
    {
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string Sms { get; set; }

        public Account() { }

        public Account(string Username, string Password, string Firstname, string Middlename, string Lastname, DateTime Birthdate, string Email, string Sms)
        {
            this.Username = Username;
            this.Password = Password;
            this.Firstname = Firstname;
            this.Middlename = Middlename;
            this.Lastname = Lastname;
            this.Birthdate = Birthdate;
            this.Email = Email;
            this.Sms = Sms;
        }
    }

    class MailService
    {
        string email;

        public MailService(string email)
        {
            this.email = email;
        }

        public void OnAccountRegistered(object sender, EventArgs e)
        {
            MessageBox.Show("Sending an email notification to " + email);
        }
    }

    class SMSService
    {
        string sms;

        public SMSService(string sms)
        {
            this.sms = sms;
        }

        public void OnAccountRegistered(object sender, EventArgs e)
        {
            MessageBox.Show("Sending an sms notification to " + sms);
        }
    }

    class AccountEventArgs : EventArgs
    {
        public Account Account { get; set; }

        public AccountEventArgs(Account Account)
        {
            this.Account = Account;
        }
    }

    class AccountProcessor
    {
        public delegate void AccountProcessorEventHandler(object sender, AccountEventArgs args);
        public event AccountProcessorEventHandler AccountProcessed;

        private void OnAccountRegistered(Account account)
        {
            if (AccountProcessed != null)
                AccountProcessed(this, new AccountEventArgs(account));
        }

        public void RegisterAccount(Account acct)
        {
            bool p;
            string t;

            if (GLOBALS.CurrentAcct == null)
            {
                t = "register";
                p = GLOBALS.WriteFile(acct);
            }
            else
            {
                t = "save";
                p = GLOBALS.EditFile(acct);
            }

            if (p)
                MessageBox.Show("Sucessfully " + t + " account");
            else
                MessageBox.Show("Unable to " + t + " account");

            OnAccountRegistered(acct);
        }
    }
}
