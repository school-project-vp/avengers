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
    public class Minion
    {
        public bool Alive { get; set; }
        public Point Position { get; set; }
        public Image ImageMinion { get; set; }
        

        public Minion(Point position) {
            this.ImageMinion = Resources.minion_1;
            this.Alive = true;
            this.Position = position;
        }
        public void Draw(Graphics g) {
            g.DrawImage(ImageMinion, Position);
        }
        public void Move(Nasoka nasoka) {
            if (nasoka == Nasoka.RIGHT) {
                Point newPosition = new Point(Position.X + 20, Position.Y);
                Position = newPosition;
            }
            else {
                Point newPosition = new Point(Position.X - 20, Position.Y);
                Position = newPosition;
            }
        }
    }
}
