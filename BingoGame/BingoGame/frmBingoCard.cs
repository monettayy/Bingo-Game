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
using BingoGame.Classes; //include this always so that EZ nalang ang pag call kang classes 
#endregion

namespace BingoGame
{
    public partial class frmBingoCard : Form
    {
        #region Fields || Properties || Ctors
        int x = 12;
        int y = 12;
        int margin = 6;
        int size = 50;
        int min = 1;
        int max = 15;
        char[] bingo = { 'B', 'I', 'N', 'G', 'O' };
        
        public List<Slot> TitleSlots;
        public List<Slot> BingoSlots;

        public frmBingoCard() { }

        public frmBingoCard(int x, int y)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            TitleSlots = new List<Slot>();
            BingoSlots = new List<Slot>();
        }
        #endregion

        #region Events
        private void frmBingoCard_Load(object sender, EventArgs e)
        {
            this.MdiParent = GLOBALS.MainForm;

            PrintTitle();
            PrintSlots();
        }

        private void Center_Click(object sender, EventArgs e)
        {
            bool p = LineChecker();
            this.BackColor = p ? Color.White : Color.Black;
        }
        #endregion

        #region Methods
        private bool LineChecker()
        {
            bool check = false;

            for (int j = 0; j <= 20 ; j+=5)
                if (BingoSlots[j].ForeColor == Color.White && BingoSlots[j + 1].ForeColor == Color.White && BingoSlots[j + 2].ForeColor == Color.White && BingoSlots[j + 3].ForeColor == Color.White && BingoSlots[j + 4].ForeColor == Color.White)
                {
                    BingoSlots[j].BackColor = Color.Red;
                    BingoSlots[j + 1].BackColor = Color.Red;
                    BingoSlots[j + 2].BackColor = Color.Red;
                    BingoSlots[j + 3].BackColor = Color.Red;
                    BingoSlots[j + 4].BackColor = Color.Red;
                    check = true;
                }

            for (int j = 0; j < 5; j += 5)
                if (BingoSlots[j].ForeColor == Color.White && BingoSlots[j + 5].ForeColor == Color.White && BingoSlots[j + 10].ForeColor == Color.White && BingoSlots[j + 15].ForeColor == Color.White && BingoSlots[j + 20].ForeColor == Color.White)
                {
                    BingoSlots[j].BackColor = Color.Red;
                    BingoSlots[j + 5].BackColor = Color.Red;
                    BingoSlots[j + 10].BackColor = Color.Red;
                    BingoSlots[j + 15].BackColor = Color.Red;
                    BingoSlots[j + 20].BackColor = Color.Red;
                    check = true;
                }

            if (BingoSlots[0].ForeColor == Color.White && BingoSlots[6].ForeColor == Color.White && BingoSlots[18].ForeColor == Color.White && BingoSlots[24].ForeColor == Color.White)
            {
                BingoSlots[0].BackColor = Color.Red;
                BingoSlots[6].BackColor = Color.Red;
                BingoSlots[18].BackColor = Color.Red;
                BingoSlots[24].BackColor = Color.Red;
                check = true;
            }
            
            if (BingoSlots[4].ForeColor == Color.White && BingoSlots[8].ForeColor == Color.White && BingoSlots[16].ForeColor == Color.White && BingoSlots[20].ForeColor == Color.White)
            {
                BingoSlots[4].BackColor = Color.Red;
                BingoSlots[8].BackColor = Color.Red;
                BingoSlots[16].BackColor = Color.Red;
                BingoSlots[20].BackColor = Color.Red;
                check = true;
            }

            if (BingoSlots[0].ForeColor == Color.White && BingoSlots[4].ForeColor == Color.White && BingoSlots[20].ForeColor == Color.White && BingoSlots[24].ForeColor == Color.White)
            {
                BingoSlots[0].BackColor = Color.Red;
                BingoSlots[4].BackColor = Color.Red;
                BingoSlots[20].BackColor = Color.Red;
                BingoSlots[24].BackColor = Color.Red;
                check = true;
            }

            return check;
        }

        private void PrintSlots()
        {
            x = 12;
            y += (size + margin);
            List<int> randomNumber = new List<int>();
            
            SuspendLayout();
            for (int i = 1; i <= 5; i++, min += 15, max += 15, x += (size + margin), y = (size + margin * 3))
            {
                randomNumber = RandomNumber(min, max);

                for (int j = 1; j <= 5; j++, y += (size + margin))
                {
                    Slot slot;
                    if (i == 3 && j == 3)
                    {
                        slot = new Slot("center", "BINGO", size, x, y, margin, SlotType.Center, Color.Black);
                        slot.Click += Center_Click;
                    }
                    else
                        slot = new Slot("slot" + randomNumber[j - 1].ToString(), randomNumber[j - 1].ToString(), size, x, y, margin, SlotType.Number, GLOBALS.ColorListSlot[i - 1]);
                    
                    BingoSlots.Add(slot);
                    Controls.Add(slot);
                }

                randomNumber.Clear();
            }
            ResumeLayout();
        }

        private void PrintTitle()
        {
            SuspendLayout();
            for (int i = 1; i <= 5; i++, x += (size + margin))
            {
                Slot slot = new Slot("title" + bingo[i - 1].ToString(), bingo[i - 1].ToString(), size, x, y, margin, SlotType.Title, GLOBALS.ColorListTitle[i - 1]);
                TitleSlots.Add(slot);
                Controls.Add(slot);
            }
            ResumeLayout();
        }

        private List<int> RandomNumber(int min, int max)
        {
            List<int> random = new List<int>(5);

            for(int i=0; i<5; i++)
                while (true)
                {
                    Random r = new Random();
                    int t = r.Next(min, max);
                    if (random.Contains(t) == false)
                    {
                        random.Add(t);
                        break;
                    }
                    else
                        continue;
                }

            return random;
        }
        #endregion
    }
}
