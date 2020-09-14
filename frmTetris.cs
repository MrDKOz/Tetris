using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using Tetris.Classes;
using Tetris.Extensions;
using Tetris.Helpers;
using Tetris.Models;

namespace Tetris
{
    public partial class Tetris : Form
    {
        private readonly PieceManager _pieceManager;
        private readonly MovementHelper _movementHelper = new MovementHelper();
        private Piece _piece;
        private int maxX = 10;
        private int maxY = 20;

        private int spawnPointX = 3;

        public Tetris()
        {
            InitializeComponent();

            _pieceManager = new PieceManager();

            CreateGrid();
            SpawnNewPiece();
        }

        private void SpawnNewPiece()
        {
            _piece = _pieceManager.FetchNewPiece();
            _piece.CurrentPlacement = new Placement();

            PaintPiece(_piece, spawnPointX);
        }

        /// <summary>
        /// Draw the given shape onto the grid.
        /// </summary>
        /// <param name="pieceToPaint">The piece to draw onto the grid.</param>
        /// <param name="offSetX">How many columns should paint be offset by?</param>
        /// <param name="offSetY">How many rows should paint be offset by?</param>
        private void PaintPiece(Piece pieceToPaint, int offSetX = 0, int offSetY = 0)
        {
            for (var row = 0; row < pieceToPaint.Shape.GetLength(0); row++)
                for (var col = 0; col < pieceToPaint.Shape.GetLength(1); col++)
                {
                    if (pieceToPaint.Shape[row, col] != 1) continue;

                    var panel = GetGridPoint(col + offSetX, row + offSetY);
                    panel.BackColor = pieceToPaint.Color;

                    _piece.CurrentPlacement.ActiveCells.Add(new Point(col + offSetX, row + offSetY));
                }
        }

        /// <summary>
        /// Moves the active piece in the given direction.
        /// </summary>
        private void MovePiece(MovementHelper.Direction direction)
        {
            var defaultPanel = new BackgroundTile().Default().BackColor;
            var newPlacement = _movementHelper.CalculateMovement(direction, _piece.CurrentPlacement);
            
            // Clear the current piece from the game grid
            // so we don't interfere with collision detection
            DrawPlacement(_piece.CurrentPlacement, defaultPanel);

            // Ensure next placement is valid
            if (_movementHelper.CheckIfMoveIsValid(newPlacement, tlpGameGrid))
            {
                // Paint piece in new location
                DrawPlacement(newPlacement, _piece.Color);

                // Piece moved successfully, so update the piece
                _piece.CurrentPlacement = newPlacement;
            }
            else
            {
                DrawPlacement(_piece.CurrentPlacement, _piece.Color);
                SpawnNewPiece();
            }
        }

        /// <summary>
        /// Draws the given placement on the game grid in the given colour.
        /// </summary>
        /// <param name="focusPlacement">The placement to draw.</param>
        /// <param name="color">The color to draw the placement in.</param>
        private void DrawPlacement(Placement focusPlacement, Color color)
        {
            foreach (var panel in focusPlacement.ActiveCells.Select(point => GetGridPoint(point.X, point.Y)))
                panel.BackColor = color;
        }

        /// <summary>
        /// Creates the grid on which the game will take place.
        /// </summary>
        private void CreateGrid()
        {
            var panel = new BackgroundTile();

            for (var x = 0; x <= 10; x++)
                for (var y = 0; y <= 20; y++)
                    tlpGameGrid.Controls.Add(panel.Default(), x, y);
        }

        /// <summary>
        /// Gets the Panel using the given X and Y coordinates.
        /// </summary>
        /// <param name="x">X co-ordinate.</param>
        /// <param name="y">Y co-ordinate.</param>
        /// <returns>The panel at the given co-ordinates.</returns>
        private Panel GetGridPoint(int x, int y)
        {
            return (Panel)tlpGameGrid.GetControlFromPosition(x, y);
        }

        /// <summary>
        /// User has moved the active piece.
        /// </summary>
        private void Tetris_KeyDown(object sender, KeyEventArgs e)
        {
            MovementHelper.Direction direction;

            switch (e.KeyCode)
            {
                case Keys.A:
                    direction = MovementHelper.Direction.Left;
                    break;
                case Keys.D:
                    direction = MovementHelper.Direction.Right;
                    break;
                default:
                    direction = MovementHelper.Direction.Down;
                    break;
            }

            MovePiece(direction);
        }
    }
}