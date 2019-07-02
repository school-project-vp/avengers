using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baloon_invaders
{
    public partial class Form1 : Form
    {
        public thor t { get; set; }
        public bool hasHammer { get; set; }
        public hammer h { get; set; }
        public Timer timer { get; set; }

        public Form1()
        {
            InitializeComponent();
            t = new thor(this.Height, this.Width);
            DoubleBuffered = true;
            hasHammer = false;
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            t.Move(this.Width);
            if (hasHammer)
            {
                h.Move();
                if (h.IsGone())
                {
                    hasHammer = false;
                }
            }
            Invalidate(true);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            t.Draw(e.Graphics);
            if (hasHammer)
            {
                h.Draw(e.Graphics);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
            {
                t.changeDirection(direction.left);
            }
            else if (e.KeyCode.Equals(Keys.Right))
            {
                t.changeDirection(direction.right);
            }
            else
            {
                t.changeDirection(direction.stop);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            t.dir = direction.stop;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (hasHammer)
            {
                return;
            }
            if (e.KeyChar== ' ')
            {
                hasHammer = true;
                h = new hammer(new Point(t.center.X+50,t.center.Y));
            }
            return;
        }
    }
}
