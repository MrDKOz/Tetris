using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tetris.Classes;
using Tetris.Extensions;
using Tetris.Models;

namespace Tetris.Helpers
{
    public class MovementHelper
    {
        // Game grid dimensions
        public const int MaxX = 10;
        public const int MaxY = 20;

        // Directions the piece can be moved in
        public enum Direction
        {
            Left,
            Right,
            Down
        }

        /// <summary>
        /// Calculates the given movement, and returns the new points.
        /// </summary>
        /// <param name="direction">The direction to move in.</param>
        /// <param name="currentPlacement">The current placement of the piece.</param>
        /// <returns>The new placement points.</returns>
        public Placement CalculateMovement(Direction direction, Placement currentPlacement)
        {
            var newPlacement = new Placement();
            var offSetX = 0;
            var offSetY = 0;

            foreach (var point in currentPlacement.ActiveCells)
            {
                switch (direction)
                {
                    case Direction.Left:
                        offSetX = -1;
                        break;
                    case Direction.Right:
                        offSetX = 1;
                        break;
                    case Direction.Down:
                        offSetY = 1;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }

                newPlacement.ActiveCells.Add(new Point(point.X + offSetX, point.Y + offSetY));
            }

            return newPlacement;
        }

        /// <summary>
        /// Checks if the move meets the following requirements:
        /// 1. New placement is within the bounds of the game grid
        /// 2. New placement is not overlapping with another set piece
        /// </summary>
        /// <param name="piece">The piece we're moving.</param>
        /// <param name="newPlacement">The requested placement of the next piece.</param>
        /// <param name="gameGrid">The game grid.</param>
        /// <returns>True/False as to whether the given move is valid</returns>
        public bool CheckIfMoveIsValid(Placement newPlacement, TableLayoutPanel gameGrid)
        {
            var defaultBackgroundColour = new BackgroundTile().Default().BackColor;

            // Check we're within our bounds
            var moveIsValid = newPlacement.ActiveCells.None(p => p.X < 0 || p.X >= MaxX || p.Y > MaxY);

            if (!moveIsValid) return false; // We're out out of bounds, no need to check the rest

            // We're in bounds, let's check if we're colliding with another piece
            foreach (var focusedPanel in newPlacement.ActiveCells.Select(newPoint => GetGridPoint(newPoint.X, newPoint.Y, gameGrid)))
            {
                // Check if the focused panel is the default background colour
                // if it's not then we know we've hit another piece
                moveIsValid = focusedPanel.BackColor == defaultBackgroundColour;

                if (!moveIsValid)
                    break; // Move isn't valid, no need to continue
            }

            return moveIsValid;
        }

        /// <summary>
        /// Draws the given placement on the game grid in the given colour.
        /// </summary>
        /// <param name="focusPlacement">The placement to draw.</param>
        /// <param name="color">The color to draw the placement in.</param>
        /// <param name="gameGrid">The game grid.</param>
        private void ManageActivePieceVisibility(Placement focusPlacement, Color color, TableLayoutPanel gameGrid)
        {
            foreach (var panel in focusPlacement.ActiveCells.Select(point => GetGridPoint(point.X, point.Y, gameGrid)))
                panel.BackColor = color;
        }

        /// <summary>
        /// Gets the Panel using the given X and Y coordinates.
        /// </summary>
        /// <param name="x">X co-ordinate.</param>
        /// <param name="y">Y co-ordinate.</param>
        /// <param name="gameGrid">The grid in which the game takes place</param>
        /// <returns>The panel at the given co-ordinates.</returns>
        private Panel GetGridPoint(int x, int y, TableLayoutPanel gameGrid)
        {
            return (Panel)gameGrid.GetControlFromPosition(x, y);
        }
    }
}
