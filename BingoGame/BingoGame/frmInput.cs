#region Neede Namespaces
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
    public partial class frmInput : Form
    {
        public frmInput()
        {
            InitializeComponent();
        }

        #region Events
        private void frmInput_Load(object sender, EventArgs e)
        {
            this.MdiParent = GLOBALS.MainForm;
            this.AcceptButton = btnProceed;
            GLOBALS.MainForm.DisableStrips();
            txtInputBox.MaxLength = 1;
            txtInputBox.TextAlign = HorizontalAlignment.Center;
            txtInputBox.KeyPress += txtInputBox_KeyPress;
            btnProceed.Click += btnProceed_Click;
        }

        public void txtInputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsControl(e.KeyChar) || (Char.IsDigit(e.KeyChar) && (Convert.ToInt32(e.KeyChar) >= 49 && Convert.ToInt32(e.KeyChar) <= 54)) ? false : true;
        }

        public void btnProceed_Click(object sender, EventArgs e)
        {
            if (txtInputBox.Text.Length > 0)
            {
                GLOBALS.NoOfCards = Convert.ToInt32(txtInputBox.Text);
                this.Close();
                GLOBALS.MainForm.GenerateBingoCard(GLOBALS.NoOfCards);
            }
        }
        #endregion
    }
}
