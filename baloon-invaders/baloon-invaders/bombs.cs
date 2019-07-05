using baloon_invaders.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baloon_invaders
{
    public class bomb
    {
        public Image ImageBomb { get; set; }
        public bool IsDone { get; set; }
        public Point Position { get; set; }
        public bomb(Point p)
        {
            this.ImageBomb = Resources.bomb;
            this.Position = p;
            this.IsDone = false;
        }
        public void Move(int height)
        {
            Point newPosition = new Point(Position.X, Position.Y + 15);
            if (newPosition.Y > height)
            {
                IsDone = true;
            }
            Position = newPosition;
        }
        public void setDone()
        {
            this.IsDone = true;
        }
        public bool Check(Point thorPosition)
        {
            if (Position.Y+50 >= thorPosition.Y && Position.Y<=thorPosition.Y +90 && Position.X >= thorPosition.X && Position.X <= thorPosition.X + 90)
            {
                IsDone = true;
                return true;
            }
            return false;
        }
        public void Draw(Graphics g)
        {
            if (IsDone == false)
            {
                Bitmap objBitmap = new Bitmap(Resources.bomb, new Size(50, 50));
                g.DrawImage(objBitmap, Position);
            }
        }
    }
}