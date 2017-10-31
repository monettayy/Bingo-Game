#region Nedded Namespaces
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
    public partial class frmGame : Form
    {
        #region Fields || Properties || Ctors
        Label lblBoard;
        Label lblAlert;
        int GameSecInterval;
        int prepTime;
        int ctr;

        public frmGame()
        {
            InitializeComponent();
        }

        public frmGame(int width, int height, int x, int y)
        {
            InitializeComponent();
            GLOBALS.RandNums = new List<int>();
            lblBoard = new Label();
            lblAlert = new Label();
            GameSecInterval = GLOBALS.NoOfCards <= 2 ? 3 : GLOBALS.NoOfCards == 3 || GLOBALS.NoOfCards == 4 ? 5 : 7;
            prepTime = 10;
            ctr = 1;

            SuspendLayout();
            this.MdiParent = GLOBALS.MainForm;
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(width, height);
            this.Location = new Point(x, y);
            this.BackColor = Color.Black;
            ResumeLayout();
        }
        #endregion

        #region Events
        private void frmGame_Load(object sender, EventArgs e)
        {
            DisplayTitle();
            
            tsbCancel.Click += tsbCancel_Click;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer2.Interval = 2000;
            timer2.Tick += timer2_Tick;

            timer2.Start();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            foreach (var f in GLOBALS.MainForm.BingoCards)
                f.Close();
            GLOBALS.MainForm.EnableStrips();
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (ctr == prepTime)
            {
                ctr = GameSecInterval;
                timer1.Start();
                timer2.Stop();
            }
            else
            {
                LoadGameFlashScreen(ctr);
                ctr++;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GLOBALS.RandNums.Count == 34)
                timer1.Stop();

            if (ctr % GameSecInterval == 0)
            {
                DisplayBoard(GenerateRandomNumber());
                DisplayAlert();
            }
            ctr++;
        }
        #endregion

        #region Methods
        private void DisplayAlert()
        {
            if(GLOBALS.RandNums.Count >= 24 || GLOBALS.RandNums.Count < 34)
                lblAlert.Text = (34 - GLOBALS.RandNums.Count).ToString() + " Chance(s) Left";
            if(GLOBALS.RandNums.Count == 34)
                lblAlert.Text = "Game Ends";
        }

        private void LoadGameFlashScreen(int c)
        {
            if (c < 3)
                DisplayBoard("Click on your card's slot if it match the drawn number");
            else if (c > 2 && c < 6)
                DisplayBoard("Click on the center of the card to check for bingo");
            else if (c > 5 && c < 7)
                DisplayBoard("Ready?");
            else
                DisplayBoard("Game starts in " + (prepTime - c));
        }

        private void DisplayBoard(string text)
        {
            lblBoard.Font = new Font("Consolas", 40, FontStyle.Bold);
            lblBoard.Text = text;
        }

        private void DisplayBoard(int num)
        {
            int tp = GLOBALS.NoOfCards <= 2 ? 4 : GLOBALS.NoOfCards == 3 || GLOBALS.NoOfCards == 4 ? 2 : 0;
            lblBoard.Size = new Size(this.Size.Width - (100 * tp), this.Size.Height / 2);
            lblBoard.Location = new Point((100 * tp) / 2, 80);

            lblBoard.Font = new Font("Consolas", 200, FontStyle.Bold);
            lblBoard.Text = num.ToString();

            if(num < 16)
                lblBoard.BackColor = GLOBALS.ColorListTitle[0];
            else if (num > 15 && num < 31)
                lblBoard.BackColor = GLOBALS.ColorListTitle[1];
            else if (num > 30 && num < 46)
                lblBoard.BackColor = GLOBALS.ColorListTitle[2];
            else if (num > 45 && num < 61)
                lblBoard.BackColor = GLOBALS.ColorListTitle[3];
            else
                lblBoard.BackColor = GLOBALS.ColorListTitle[4];

            GLOBALS.RandNums.Add(num);
        }
        
        private int GenerateRandomNumber()
        {
            while (true)
            {
                Random r = new Random();
                int t = r.Next(1, 75);
                if (GLOBALS.RandNums.Contains(t) == false)
                    return t;
                else
                    continue;
            }
        }

        private void DisplayTitle()
        {
            SuspendLayout();
            Label label = new Label();
            label.AutoSize = false;
            label.Size = new Size(this.Size.Width, 80);
            label.Location = new Point(0, 20);
            label.Font = new Font("Consolas", 50, FontStyle.Bold);
            label.ForeColor = Color.White;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = "BINGO GAME";
            Controls.Add(label);

            lblBoard.AutoSize = false;
            lblBoard.Size = new Size(this.Size.Width, this.Size.Height / 2);
            lblBoard.Location = new Point(0, 80);
            lblBoard.Font = new Font("Consolas", 40, FontStyle.Bold);
            lblBoard.ForeColor = Color.White;
            lblBoard.TextAlign = ContentAlignment.MiddleCenter;
            lblBoard.Text = "Loading . . . ";
            Controls.Add(lblBoard);

            lblAlert.AutoSize = false;
            int tp = GLOBALS.NoOfCards <= 2 ? 4 : GLOBALS.NoOfCards == 3 || GLOBALS.NoOfCards == 4 ? 2 : 0;
            lblAlert.Size = new Size(this.Size.Width - (100 * tp), this.Size.Height - (this.Size.Height / 2 + 100));
            lblAlert.Location = new Point((100 * tp) / 2, this.Size.Height / 2 + 80);
            lblAlert.Font = new Font("Consolas", 35, FontStyle.Bold);
            lblAlert.ForeColor = Color.Red;
            lblAlert.TextAlign = ContentAlignment.BottomRight;
            lblAlert.Text = "";
            Controls.Add(lblAlert);
            ResumeLayout();
        }
        #endregion
    }
}
