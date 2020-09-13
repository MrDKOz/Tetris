using System.Collections.Generic;
using System.Drawing;

namespace Tetris.Models
{
    public class Placement
    {
        public Placement()
        {
            ActiveCells = new List<Point>();
        }

        public List<Point> ActiveCells { get; set; }
    }
}
