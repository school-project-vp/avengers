using baloon_invaders.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baloon_invaders
{
    public class hammer
    {
        public Point position { get; set; }
        public Image img { get; set; }

        public hammer(Point p)
        {
            position = p;
            img = Resources.thor_hammer;
        }

        public void Draw(Graphics g)
        {
            g.DrawImageUnscaled(img, position);
        }

        public void Move()
        {
            position = new Point(position.X, position.Y - 20);
        }

        public bool IsGone()
        {
            if (position.Y + 20 < 0)
            {
                return true;
            }
            return false;
        }
    }
}
