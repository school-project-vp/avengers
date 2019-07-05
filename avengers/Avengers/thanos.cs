using Avengers.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avengers
{
    public class thanos
    {
        public int Interval { get; set; }
        public bool Died { get; set; }
        public bool isHitted { get; set; }
        public Point Position = new Point(500, 80);
        public Image ImageThanosRight { get; set; }
        public Image ImageThanosLeft { get; set; }
        public Rectangle ImageBounds = new Rectangle(0, 0, 10, 10);
        int width;
        int height;
        public bool rotateImage { get; set; }
        public int health { get; set; }

        public thanos()
        {
            Interval = 20;
            Died = false;
            isHitted = false;
            ImageThanosRight = Resources.thanos;
            ImageThanosLeft = Resources.thanos2;
            ImageBounds.Width = ImageThanosRight.Width;
            width = ImageThanosRight.Width;
            health = ImageThanosRight.Height;
            rotateImage = false;
            health = 100;
        }
        public void Draw(Graphics g)
        {
            if (Died) return;
            if (!isHitted)
            {
                if (rotateImage == true)
                    g.DrawImage(ImageThanosLeft, Position.X, Position.Y);
                else
                    g.DrawImage(ImageThanosRight, Position.X, Position.Y);
            }

        }

        public void MoveRight(int limit)
        {
            Position.X = Position.X + Interval;
            if (Position.X > limit - ImageBounds.Width)
            {
                Position.X = limit - ImageBounds.Width;
                Position.Y += 10;
                rotateImage = true;
            }

        }

        public void MoveLeft()
        {
            Position.X -= Interval;
            if (Position.X < 0)
            {
                Position.X = 0;
                Position.Y += 5;
                rotateImage = false;
            }

        }

    }
}