using baloon_invaders.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baloon_invaders
{
    public enum direction
    {
        left,
        right,
        stop,
    }

    public class thor
    {
        public Point center { get; set; }
        Image img { get; set; }
        //Image limg { get; set; }
        public bool alive { get; set; }
        public direction dir { get; set; }

        public thor(int heught, int witdh)
        {
            center = new Point((witdh / 2)-80, heught-170);
            img = Resources.thor_right;
            //limg = Resources.lthor;
            alive = true;
            dir = direction.stop;
        }

        public void Draw(Graphics g)
        {
            g.DrawImageUnscaled(img, center);   
        }

        public void changeDirection(direction d)
        {
            dir = d;
            if (dir == direction.left)
            {
                img = Resources.thor_left;
            }
            if (dir == direction.right)
            {
                img = Resources.thor_right;
            }
        }

        public void Move(int width)
        {
            if(dir == direction.right)
            {
                if (center.X < width-146)
                {
                    center = new Point(center.X + 20, center.Y);
                }
            }
            else if (dir == direction.left)
            {
                if (center.X > 0+5)
                {
                    center = new Point(center.X - 20, center.Y);
                }
            }
            return;
        }
    }
}
