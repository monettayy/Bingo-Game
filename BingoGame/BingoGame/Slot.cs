#region NeedeNamespaces
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BingoGame.Classes;
#endregion

namespace BingoGame
{
    public partial class Slot : Button
    {
        private SlotType typeOfSlot;
        [
            Description("Type of the bingo slot"),
            Category("Type"),
            DefaultValueAttribute(typeof(SlotType)),
            Browsable(true)
        ]

        public SlotType TypeOfSlot
        {
            get { return typeOfSlot; }
            set { typeOfSlot = value; }
        }

        public Slot() { }

        public Slot(string name, string text, int size, int x, int y, int margin, SlotType type, Color backColor) 
        {
            this.SuspendLayout();
            this.Name = name;
            this.Text = text;
            this.AutoSize = false;
            this.Size = new Size(size, size);
            this.Location = new Point(x, y);
            this.Margin = new Padding(margin, margin, margin, margin);
            this.typeOfSlot = type;

            this.TextAlign = ContentAlignment.MiddleCenter;
            this.Font = typeOfSlot == SlotType.Title ? new Font("Century Gothic", 28, FontStyle.Bold) : typeOfSlot == SlotType.Center ? new Font("Century Gothic", 12, FontStyle.Bold) : new Font("Century Gothic", 18, FontStyle.Bold);

            this.BackColor = backColor;
            this.ForeColor = typeOfSlot == SlotType.Title || typeOfSlot == SlotType.Center ? Color.White : Color.Black;
            this.FlatStyle = FlatStyle.Flat;
            this.ResumeLayout();

            if (typeOfSlot == SlotType.Number)
                this.Click += new EventHandler(Slot_Click);
        }

        private void Slot_Click(object sender, EventArgs e)
        {
            if (GLOBALS.RandNums.Contains(Convert.ToInt32(this.Text)))
                this.ForeColor = Color.White;
        }
    }
}
