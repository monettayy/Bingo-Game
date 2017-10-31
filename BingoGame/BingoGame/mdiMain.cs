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
using System.Threading;
#endregion

namespace BingoGame
{
    public partial class mdiMain : Form
    {
        #region Fields || Properties || Ctors
        public List<frmBingoCard> BingoCards { get; set; }

        public mdiMain()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            BingoCards = new List<frmBingoCard>();
        }
        #endregion

        #region Events
        private void mdiMain_Load(object sender, EventArgs e)
        {
            DisableStrips();
            GLOBALS.ReadFile();
            tsbQuit.Click += tsbQuit_Click;
            tsbPlay.Click += tsbPlay_Click;
            tsbAcccount.Click += tsbAcccount_Click;
            new frmLogin().Show();
        }

        private void tsbAcccount_Click(object sender, EventArgs e)
        {
            new frmRegister().Show();
        }

        private void tsbPlay_Click(object sender, EventArgs e)
        {
            new frmInput().Show();
            RandomColor(0, 9);
        }

        private void tsbQuit_Click(object sender, EventArgs e)
        {
            GLOBALS.CurrentAcct = null;
            new frmLogin().Show();
        }
        #endregion

        #region Methods
        public void EnableStrips()
        {
            tsbAcccount.Enabled = true;
            tsbPlay.Enabled = true;
            tsbQuit.Enabled = true;
        }

        public void DisableStrips()
        {
            tsbAcccount.Enabled = false;
            tsbPlay.Enabled = false;
            tsbQuit.Enabled = false;
        }

        public void GenerateBingoCard(int noOfCards)
        {
            int x = 5, y = 5, margin = 3;
            int tp = GLOBALS.NoOfCards == 2 ? 0 : GLOBALS.NoOfCards == 3 || GLOBALS.NoOfCards == 4 ? 1 : 2;
            for (int i = 0; i < noOfCards; i++)
            {
                frmBingoCard bingoCard = new frmBingoCard(x, y);
                x += (bingoCard.Size.Width + margin);

                if (i == tp)
                {
                    x = 5;
                    y += (bingoCard.Size.Height + margin);
                }

                BingoCards.Add(bingoCard);
            }
            
            foreach (var f in BingoCards)
                f.Show();

            DisplayGame();
        }

        public void DisplayGame()
        {
            int tp = GLOBALS.NoOfCards <= 2 ? 1 : GLOBALS.NoOfCards == 3 || GLOBALS.NoOfCards == 4 ? 2 : 3;
            int toadd = (GLOBALS.NoOfCards <= 2 ? 8 : GLOBALS.NoOfCards == 3 || GLOBALS.NoOfCards == 4 ? 6 : 5) * tp;
            int px = new frmBingoCard().Size.Width * tp + toadd;
            frmGame game = new frmGame(this.Size.Width - px - 25, this.Size.Height - 71, px, 5);
            game.Show();
        }

        private void RandomColor(int min, int max)
        {
            List<int> random = new List<int>(5);
            GLOBALS.ColorListTitle = new List<Color>();
            GLOBALS.ColorListSlot = new List<Color>();

            for (int i = 0; i < 5; i++)
                while (true)
                {
                    Random r = new Random();
                    int index = r.Next(min, max);

                    if (random.Contains(index) == false)
                    {
                        GLOBALS.ColorListTitle.Add(GLOBALS.darkColor[index]);
                        GLOBALS.ColorListSlot.Add(GLOBALS.lightColor[index]);
                        random.Add(index);
                        break;
                    }
                    else
                        continue;
                }
        }
        #endregion
    }
}
