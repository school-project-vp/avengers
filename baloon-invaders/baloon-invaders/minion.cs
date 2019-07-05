using baloon_invaders.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static baloon_invaders.Form1;

namespace baloon_invaders
{
    public class minion
    {
        public bool Alive { get; set; }
        public Point Position { get; set; }
        public Image ImageMinion { get; set; }


        public minion(Point position)
        {
            this.ImageMinion = Resources.minion_1;
            this.Alive = true;
            this.Position = position;
        }
        public void Draw(Graphics g)
        {
            if (Alive)
            {
                g.DrawImage(ImageMinion, Position);
            }
        }
        public void Move(Direction nasoka, int velocity)
        {
            if (nasoka == Direction.RIGHT)
            {
                Point newPosition = new Point(Position.X + velocity, Position.Y);
                Position = newPosition;
            }
            else
            {
                Point newPosition = new Point(Position.X - velocity, Position.Y);
                Position = newPosition;
            }
        }
        public void MoveDown()
        {
            this.Position = new Point(Position.X, Position.Y + 20);
        }
    }
}