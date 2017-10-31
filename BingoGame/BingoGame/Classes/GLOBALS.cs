#region Needed Namespaces
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace BingoGame.Classes
{
    public enum SlotType { Title, Number, Center}

    class GLOBALS
    {
        #region Fields || Properties
        public const string DBFile = "Accounts.txt";
        private static mdiMain _main = null;
        public static mdiMain MainForm { get { return _main = _main == null ? new mdiMain() : _main; } }
        public static int NoOfCards { get; set; }
        public static List<int> RandNums { get; set; }

        public static Color[] darkColor = new Color[] { Color.DimGray, Color.Maroon, Color.DarkGoldenrod, Color.MidnightBlue, Color.OrangeRed, Color.Indigo, Color.DarkGreen, Color.MediumVioletRed, Color.Teal };
        public static Color[] lightColor = new Color[] { Color.Gainsboro, Color.MistyRose, Color.Khaki, Color.LightSteelBlue, Color.Bisque, Color.Thistle, Color.LightGreen, Color.Pink, Color.LightCyan };
        public static List<Color> ColorListTitle { get; set; }
        public static List<Color> ColorListSlot { get; set; }
        public static Account CurrentAcct { get; set; }

        public static List<Account> RegisteredAccts { get; set; }
        #endregion

        #region Methods
        public static Account AccountCheck(string username, string password)
        {
            foreach (var a in RegisteredAccts)
                if (a.Username == username && a.Password == password)
                    return a;
            return null;
        }

        public static bool EditFile(Account a)
        {
            try
            {
                string[] arrLines = File.ReadAllLines(DBFile);
                string temp = a.PlayerID.ToString("0##") + "|" + a.Username + "|" + a.Password + "|" + a.Firstname + "|" + a.Middlename + "|" + a.Lastname + "|" + a.Birthdate.ToShortDateString() + "|" + a.Email + "|" + a.Sms;
                arrLines[a.PlayerID - 1] = temp;
                File.WriteAllLines(DBFile, arrLines);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ReadFile()
        {
            RegisteredAccts = new List<Account>();
            try
            {
                string line;
                using (StreamReader reader = new StreamReader(DBFile))
                {
                    while ((line = reader.ReadLine()) != null && line.Length != 0)
                    {
                        string[] lineArr = line.Split('|');
                        RegisteredAccts.Add(new Account
                            {
                                PlayerID = Convert.ToInt32(lineArr[0].Trim()),
                                Username = lineArr[1].Trim(),
                                Password = lineArr[2].Trim(),
                                Firstname = lineArr[3].Trim(),
                                Middlename = lineArr[4].Trim(),
                                Lastname = lineArr[5].Trim(),
                                Birthdate = Convert.ToDateTime(lineArr[6].Trim()),
                                Email = lineArr[7].Trim(),
                                Sms = lineArr[8].Trim()
                            });
                    }
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static bool WriteFile(Account a)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(DBFile))
                {
                    writer.Write(a.PlayerID.ToString("0##") + "|");
                    writer.Write(a.Username + "|");
                    writer.Write(a.Password + "|");
                    writer.Write(a.Firstname + "|");
                    writer.Write(a.Middlename + "|");
                    writer.Write(a.Lastname + "|");
                    writer.Write(a.Birthdate.ToShortDateString() + "|");
                    writer.Write(a.Email + "|");
                    writer.WriteLine(a.Sms);
                }
                return true;
            }
            catch
            {
                return true;
            }
        }
        #endregion
    }
}
