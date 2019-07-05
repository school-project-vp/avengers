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
        public int flag = 0;
        public int score { get; set; }
        public thor t { get; set; }
        public bool hasHammer { get; set; }
        public hammer h { get; set; }
        public Timer timer { get; set; }
        static int MinionWidth = 90;
        static int MinionHeight = 90;
        public bool redFrame { get; set; }
        public minion attacker { get; set; }
        public List<bomb> bombs { get; set; }
        public int lives { get; set; }
        public enum Direction { LEFT = 0, RIGHT = 1 };
        public minion[,] Minions = new minion[5, 3];
        public bool[] Columns = new bool[5];
        Direction direction;
        public int velocity;
        thanos thanos;
        Timer timerThanos;
        Timer timerGauntlet;
        gauntletDoc doc;
        Random r;
        int generateGauntlet;
        int lvl;
        Timer timer3 = new Timer();
        Timer timer2 = new Timer();

        public Form1()
        {
            Initialize();
            InitializeComponent();
            t = new thor(this.Height, this.Width);
            DoubleBuffered = true;
            score = 0;
            lvl = 1;
            lives = 3;
            hasHammer = false;
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 10;
            timer.Enabled = true;
            timer.Start();
            thanos = new thanos();
            timerThanos = new Timer();
            timerThanos.Interval = 50;
            timerThanos.Tick += new EventHandler(timer3_Tick);
            //timerThanos.Start();

            timerGauntlet = new Timer();
            timerGauntlet.Interval = 50;
            timerGauntlet.Tick += new EventHandler(timer2_Tick);
           // timerGauntlet.Start();
            DoubleBuffered = true;
            doc = new gauntletDoc();
            generateGauntlet = 0;
           
        }

        public void Initialize()
        {
            redFrame = false;
            bombs = new List<bomb>();
            velocity = 20;
            direction = Direction.RIGHT;
            int startX = 100;
            int startY = 0;
            hasHammer = false;
            for (int i = 0; i < 5; i++)
            {
                Columns[i] = true;
                for (int j = 0; j < 3; j++)
                {
                    Minions[i, j] = new minion(new Point(startX + ((i + 1) * MinionWidth), startY + ((j) * MinionHeight)));
                }
            }
            
            timer3.Tick += new EventHandler(timer5_Tick);
            timer3.Start();
           
            timer2.Tick += new EventHandler(timer4_Tick);
            timer2.Interval = 2000;
            timer2.Start();
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 25;
            timer.Enabled = true;
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            direction = changeDirection(direction);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Minions[i, j].Move(direction, velocity);
                }
            }
            Invalidate(true);
        }
        public int getLeftIndex()
        {
            int leftIndex = 5;
            for (int i = 4; i >= 0; i--)
            {
                if (Columns[i] == true)
                {
                    leftIndex = i;
                }
            }
            return leftIndex;
            //ODI NA LEVEL 2
        }
        public int getRightIndex()
        {
            int rightIndex = 0;
            for (int i = 0; i < 5; i++)
            {
                if (Columns[i] == true)
                {
                    rightIndex = i;
                }
            }
            return rightIndex;
            //ODI NA LEVEL 2
        }
        public void MoveMinionsDown()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Minions[i, j].MoveDown();
                }
            }
        }
        public Direction changeDirection(Direction direction)
        {
            int lIndex = getLeftIndex();
            int rIndex = getRightIndex();

            minion right = Minions[rIndex, 0];
            minion left = Minions[lIndex, 0];
            Direction updatedDirection = direction;
            if (direction == Direction.RIGHT)
            {
                if (right.Position.X + 20 > this.Width - 140)
                {
                    updatedDirection = Direction.LEFT;
                }
                if (direction != updatedDirection)
                {
                    MoveMinionsDown();
                }
                return updatedDirection;
            }
            if (direction == Direction.LEFT)
            {
                if (left.Position.X - 20 < 0)
                {
                    updatedDirection = Direction.RIGHT;
                }
                if (direction != updatedDirection)
                {
                    MoveMinionsDown();
                }
                return updatedDirection;
            }
            //unreachable code
            return updatedDirection;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

            if (!thanos.rotateImage)
                thanos.MoveRight(Width);
            //thanos.MoveLeft();
            else
                thanos.MoveLeft();

            Invalidate(true);
        }
        public void updateVelocity()
        {
            int numberAlive = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Minions[i, j].Alive == true)
                    {
                        numberAlive++;
                    }
                }
            }
            if (numberAlive <= 3)
            {
                velocity = 50;
                timer3.Interval = 1000;
            }
        }
        public void updateColumns()
        {
            int counterAlive = 0;
            int i;
            int j;
            for (i = 0; i < 5; i++)
            {
                counterAlive = 0;
                for (j = 0; j < 3; j++)
                {
                    if (Minions[i, j].Alive == true)
                    {
                        counterAlive++;
                    }
                }
                if (counterAlive == 0)
                {
                    Columns[i] = false;
                }
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            r = new Random();
            if (generateGauntlet % 6 == 0)
            {
                Point Position = new Point(r.Next(0, Width - 10));
                doc.AddGauntlet(Width, Position);
            }
            ++generateGauntlet;
            doc.MoveGauntlets(Height);
            if (lvl == 2)
            {
                foreach (gauntlet g in doc.gauntlets)
                {
                    if (g.Hit(t))
                    {
                        lives--;
                        redFrame = true;
                        //AKO LIVES == 0 FRLI EXCEPTION ZA GAME OVER
                        if (lives < 0)
                        {
                            this.DialogResult = DialogResult.Ignore;
                            this.Close();
                        }
                    }
                }
            }
            Invalidate(true);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            addBomb();
            Invalidate(true);

        }
        public void deleteDoneBombs()
        {
            for (int i = 0; i < bombs.Count; i++)
            {
                if (bombs[i].IsDone == true)
                {
                    bombs.RemoveAt(i);
                }
            }
        }

        public bool checkAttack()
        {
            bool flag = false;
            foreach (bomb b in bombs)
            {
                flag = b.Check(t.center);
                if (flag)
                {
                    break;
                }
            }
            return flag;
        }
        public void addBomb()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int num = rand.Next(0, 15);
            attacker = MinionAttack(num);
            Point bombPosition = new Point(attacker.Position.X + 45, attacker.Position.Y + 45);
            bomb b = new bomb(bombPosition);
            bombs.Add(b);
        }
        public void moveBombs()
        {
            for (int i = 0; i < bombs.Count; i++)
            {
                if (bombs[i].IsDone == false)
                {
                    bombs[i].Move(this.Height);
                }
            }
        }
        public minion MinionAttack(int pos)
        {
            minion returned;
            int i = pos / 5;
            int j = pos % 5;
            returned = Minions[j, i];
            return returned;
        }
        private void timer5_Tick(object sender, EventArgs e)
        {
            if (bombs.Count != 0)
            {
                moveBombs();
                bool attack = checkAttack();
                if (attack)
                {
                    lives--;
                    redFrame = true;
                    //AKO LIVES == 0 FRLI EXCEPTION ZA GAME OVER
                    if (lives < 0)
                    {
                        this.DialogResult = DialogResult.Ignore;
                        this.Close();
                    }
                }
            }
            deleteDoneBombs();
            Invalidate(true);

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
                            if (score == 150)
                            {
                                lvl = 2;
                                timerThanos.Start();
                                timerGauntlet.Start();
                            }
                        }
                    }
                }
                if (h.IsGone())
                {
                    hasHammer = false;
                }
               
            }
            if (lvl == 2 && hasHammer)
            {
                if (h.hitTanos(thanos))
                {
                    thanos.health -= 10;
                    hasHammer = false;
                    if (thanos.health <= 0)
                    {
                        thanos.Died = true;
                        timerThanos.Stop();
                        MessageBox.Show("Great job!");
                        this.Close();
                    }
                }
            }
            
            Invalidate(true);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            Bitmap objBitmap = new Bitmap(Resources.background, new Size(this.Width, this.Height));
            e.Graphics.DrawImage(objBitmap, new Point(0, 0));
            if (redFrame)
            {

                Pen b = new Pen(Color.Red);
                b.Width = 10;
                e.Graphics.DrawRectangle(b, 0, 0, this.Width - 25, this.Height - 80);
                redFrame = false;
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Minions[i, j].Draw(e.Graphics);
                }
            }
            foreach (bomb b in bombs)
            {
                b.Draw(e.Graphics);
            }
            t.Draw(e.Graphics);
            if (hasHammer)
            {
                h.Draw(e.Graphics);
            }
            if (lvl == 2)
            {
                thanos.Draw(e.Graphics);
                doc.Draw(e.Graphics);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
            {
                t.changeDirection(nasoka.left);
            }
            else if (e.KeyCode.Equals(Keys.Right))
            {
                t.changeDirection(nasoka.right);
            }
            else
            {
                t.changeDirection(nasoka.stop);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            t.dir = nasoka.stop;
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
        private void statusStrip1_Paint(object sender, PaintEventArgs e)
        {
            ScoreLabel.Text = "Score:" + score.ToString();
            LivesLabel.Text = "Lives:" + lives.ToString();
            if (lvl == 2)
            {
                ThanosHealth.Visible = true;
                ThanosHealth.Text = "Thanos Health: " + thanos.health.ToString();
            }
        }
    }
}
