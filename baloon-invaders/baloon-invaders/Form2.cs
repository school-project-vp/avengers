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
    public partial class Form2 : Form
    {
        Form1 f;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 game = new Form1();
            button1.Text = "Start New Game";
            DialogResult r = game.ShowDialog();
            if (r == DialogResult.Cancel)
            {
                f = game;
                if (f.lives < 0)
                {
                    button3.Visible = false;
                }
                else
                {
                    button3.Visible = true;

                }
              
                

            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 g = f;
            DialogResult r = g.ShowDialog();
            if (r == DialogResult.Cancel)
            {
                f = g; 
                if (f.lives < 0)
                {
                    button3.Visible = false;
                }
                else
                {
                    button3.Visible = true;

                }
            }else
            {
                button3.Visible = false;

            }
        }
    }
}
