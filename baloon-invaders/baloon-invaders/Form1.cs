using baloon_invaders.Properties;
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
        public int score { get; set; }
        public thor t { get; set; }
        public bool hasHammer { get; set; }
        public hammer h { get; set; }
        public Timer timer { get; set; }
        static int MinionWidth = 90;
        static int MinionHeight = 90;
        public enum Nasoka { LEFT = 0, RIGHT = 1 };
        public minion[,] Minions = new minion[5, 3];
        public bool[] Koloni = new bool[5];
        Nasoka nasoka;

        public Form1()
        {
            Initialize();
            InitializeComponent();
            t = new thor(this.Height, this.Width);
            DoubleBuffered = true;
            score = 0;
            hasHammer = false;
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Start();
        }

        public void Initialize()
        {
            DoubleBuffered = true;
            nasoka = Nasoka.RIGHT;
            int startX = 100;
            int startY = 0;
            for (int i = 0; i < 5; i++)
            {
                Koloni[i] = true;
                for (int j = 0; j < 3; j++)
                {
                    Minions[i, j] = new minion(new Point(startX + ((i + 1) * MinionWidth), startY + ((j) * MinionHeight)));
                }
            }
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nasoka = promeniNasoka(nasoka);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Minions[i, j].Move(nasoka);
                }
            }
            Invalidate(true);
        }
        public Nasoka promeniNasoka(Nasoka nasoka)
        {
            int najdesenIndeks = 0;
            for (int i = 0; i < 5; i++)
            {
                if (Koloni[i] == true)
                {
                    najdesenIndeks = i;
                }
            }
            int najlevIndeks = 5;
            for (int i = 4; i >= 0; i--)
            {
                if (Koloni[i] == true)
                {
                    najlevIndeks = i;
                }
            }
            minion najdesen = Minions[najdesenIndeks, 0];
            minion najlev = Minions[najlevIndeks, 0];
            Nasoka azuriranaNasoka = nasoka;
            if (nasoka == Nasoka.RIGHT)
            {
                if (najdesen.Position.X + 20 > this.Width - 140)
                {
                    azuriranaNasoka = Nasoka.LEFT;
                }
                return azuriranaNasoka;
            }
            if (nasoka == Nasoka.LEFT)
            {
                if (najlev.Position.X - 20 < 0)
                {
                    azuriranaNasoka = Nasoka.RIGHT;
                }
                return azuriranaNasoka;
            }
            return azuriranaNasoka;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            t.Move(this.Width);
            if (hasHammer)
            {
                h.Move();
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Minions[i, j].Alive && h.Hit(Minions[i, j]))
                        {
                            Minions[i, j].Alive = false;
                            hasHammer = false;
                            score += 10;
                        }
                    }
                }
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
            Bitmap objBitmap = new Bitmap(Resources.background, new Size(this.Width, this.Height));
            e.Graphics.DrawImage(objBitmap, new Point(0, 0));

            label1.Text = "Score:" + score.ToString();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Minions[i, j].Draw(e.Graphics);
                }
            }
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
            if (e.KeyChar == ' ')
            {
                hasHammer = true;
                h = new hammer(new Point(t.center.X + 50, t.center.Y));
            }
            return;
        }

    }
}
