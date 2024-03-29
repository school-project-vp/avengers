﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avengers
{
    public class gauntletDoc
    {
        public List<gauntlet> gauntlets { get; set; }
        public bool ToDelete { get; set; }

        public gauntletDoc()
        {
            gauntlets = new List<gauntlet>();
        }

        public void AddGauntlet(int width, Point p)
        {
            gauntlet g = new gauntlet();
            g.Position = p;
            gauntlets.Add(g);
        }

        public void Draw(Graphics g)
        {
            foreach (gauntlet gauntlet in gauntlets)
            {
                gauntlet.Draw(g);
            }
        }

        public bool Hit(thor t)
        {

            for (int i = 0; i < gauntlets.Count; i++)
            {
                if (gauntlets[i].Hit(t))
                {
                    gauntlets[i].delete = true;
                    return true;
                }
            }
            return false;
        }

            public void MoveGauntlets(int limit)
        {
            for (int i = 0; i < gauntlets.Count; i++)
            {
                gauntlets.ElementAt(i).MoveDown(limit);
                if (gauntlets.ElementAt(i).delete)
                    gauntlets.Remove(gauntlets.ElementAt(i));
            }
        }

    }
}