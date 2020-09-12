using System.Drawing;
using System.Windows.Forms;
using Tetris.Classes;

namespace Tetris
{
    public partial class Form1 : Form
    {
        private readonly PieceManager _pieceManager;

        public Form1()
        {
            InitializeComponent();

            _pieceManager = new PieceManager();
            CreateGrid();
            DrawShape(_pieceManager.FetchNewPiece());
        }

        /// <summary>
        /// Draw the given shape onto the grid.
        /// </summary>
        private void DrawShape(Piece activePiece)
        {
            for (var row = 0; row < activePiece.Shape.GetLength(0); row++)
            for (var col = 0; col < activePiece.Shape.GetLength(1); col++)
            {
                if (activePiece.Shape[row, col] != 1) continue;

                var panel = (Panel) tblGrid.GetControlFromPosition(col, row);
                panel.BackColor = activePiece.Color;
            }
        }

        /// <summary>
        /// Creates the grid on which the game will take place.
        /// </summary>
        private void CreateGrid()
        {
            for (var x = 0; x <= 10; x++)
            for (var y = 0; y <= 20; y++)
                tblGrid.Controls.Add(new Panel
                {
                    BackColor = Color.White,
                    Margin = Padding.Empty
                }, x, y);
        }
    }
}