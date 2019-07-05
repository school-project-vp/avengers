using Avengers.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avengers
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
            position = new Point(position.X, position.Y - 50);
        }

        public bool IsGone()
        {
            if (position.Y + 20 < 0)
            {
                return true;
            }
            return false;
        }

        public bool hitTanos(thanos t)
        {
            if (position.X >= t.Position.X && position.X + 10 <= t.Position.X + 103 && position.Y <= t.Position.Y + 124 && position.Y >= t.Position.Y)
            {
                return true;
            }
            return false;
        }

        public bool Hit(minion m)
        {
            if (position.X>=m.Position.X && position.X + 10 <= m.Position.X+90 && position.Y <= m.Position.Y + 90&&position.Y>=m.Position.Y)
            {
                return true;
            }
            return false;
        }
    }
}
