using System.Drawing;

namespace Tetris.Classes
{
    public class Piece
    {
        public enum Names
        {
            I,
            J,
            L,
            O,
            S,
            T,
            Z
        }

        public Names Name { get; set; }
        public Color Color { get; set; }
        public int[,] Shape { get; set; }
        public int Rotations { get; set; }
    }
}
