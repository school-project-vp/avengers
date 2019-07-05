using baloon_invaders.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baloon_invaders
{
    public class gauntlet
    {
        public Point Position = new Point();
        public int Interval { get; set; }
        public Image ImageGauntlet { get; set; }
        public Rectangle ImageBounds = new Rectangle(0, 0, 10, 10);
        public bool delete { get; set; }

        public gauntlet()
        {
            //Position = p;
            Interval = 10;
            ImageGauntlet = Resources.gauntlet;
            ImageBounds.Height = ImageGauntlet.Height;
            delete = false;
        }


        public bool Hit(thor t)
        {
            if (Position.X  >= t.center.X && Position.X + 80<= t.center.X  && Position.Y + 80 >= t.center.Y)
            {
                return true;
            }
            return false;
        }

        public void MoveDown(int limit)
        {
            Position.Y = Position.Y + 30;
            if (Position.Y > limit - ImageBounds.Height)
            {
                delete = true;
            }


        }
        public void Draw(Graphics g)
        {
            g.DrawImage(ImageGauntlet, Position.X, Position.Y);
        }
    }
}