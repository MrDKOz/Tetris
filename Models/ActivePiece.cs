namespace Tetris.Models
{
    public class ActivePiece : Piece
    {
        public int Rotations { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
    }
}
