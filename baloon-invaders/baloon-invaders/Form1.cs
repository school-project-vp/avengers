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
        static int MinionWidth = 90;
        static int MinionHeight = 90;
        public enum Nasoka {LEFT = 0, RIGHT = 1};
        public Minion[,] Minions = new Minion[5,3];
        public bool[] Koloni = new bool[5];
        Nasoka nasoka;
       
        public Form1()
        {
            Initialize();
            InitializeComponent();
        }
        public void Initialize() {
            DoubleBuffered = true;
            nasoka = Nasoka.RIGHT;
            int startX = 100;
            int startY = 0;
            for (int i = 0; i < 5; i++) {
                Koloni[i] = true;
                for (int j = 0; j < 3; j++) {
                    Minions[i, j] = new Minion(new Point(startX + ((i+1)*MinionWidth), startY+((j)*MinionHeight)));
                }
            }
            timer1 = new Timer();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            nasoka = promeniNasoka(nasoka);
            
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 3; j++) {
                    Minions[i, j].Move(nasoka);
                }
            }
            Invalidate(true);
        }
        public Nasoka promeniNasoka(Nasoka nasoka) {
            int najdesenIndeks = 0;
            for (int i = 0; i < 5; i++) {
                if (Koloni[i] == true) {
                    najdesenIndeks = i;
                }
            }
            int najlevIndeks = 5;
            for (int i = 4; i >= 0; i--) {
                if (Koloni[i] == true) {
                    najlevIndeks = i;
                }
            }
            Minion najdesen = Minions[najdesenIndeks, 0];
            Minion najlev = Minions[najlevIndeks, 0];
            Nasoka azuriranaNasoka=nasoka; 
            if (nasoka == Nasoka.RIGHT) {
                if (najdesen.Position.X + 20 > this.Width - 140) {
                    azuriranaNasoka = Nasoka.LEFT;
                }
                return azuriranaNasoka;
            }
            if (nasoka == Nasoka.LEFT) {
                if (najlev.Position.X - 20 < 0) {
                    azuriranaNasoka = Nasoka.RIGHT;
                }
                return azuriranaNasoka;
            }
            return azuriranaNasoka;
        }
        private void Form1_Paint(object sender, PaintEventArgs e) {
            Bitmap objBitmap = new Bitmap(Resources.background, new Size(this.Width, this.Height));
            e.Graphics.DrawImage(objBitmap, new Point(0, 0));
           

            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 3; j++) {
                    Minions[i, j].Draw(e.Graphics);
                }
            }
        }
    }
}
