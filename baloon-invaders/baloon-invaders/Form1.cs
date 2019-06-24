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
        public Timer timer { get; set; }

        public Form1()
        {
            InitializeComponent();
            t = new thor(this.Height, this.Width);
            DoubleBuffered = true;
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            t.Move(this.Width);
            Invalidate(true);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            t.Draw(e.Graphics);
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
    }
}
