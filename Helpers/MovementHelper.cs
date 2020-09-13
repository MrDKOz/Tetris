using System;
using System.Drawing;
using System.Windows.Forms;
using Tetris.Classes;
using Tetris.Extensions;
using Tetris.Models;

namespace Tetris.Helpers
{
    public class MovementHelper
    {
        public int maxX = 10;
        public int maxY = 20;

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

        public bool CheckIfMoveIsValid(Placement newPlacement, TableLayoutPanel gameGrid, Placement currentPlacement)
        {
            var defaultBackgroundColour = new BackgroundTile().Default().BackColor;

            // Check we're within our bounds
            var moveIsValid = newPlacement.ActiveCells.None(p => p.X < 0 || p.X >= maxX || p.Y > maxY);

            if (moveIsValid)
            {
                foreach (var point in currentPlacement.ActiveCells)
                {
                    var panel = GetGridPoint(point.X, point.Y, gameGrid);
                    panel.BackColor = defaultBackgroundColour;
                }

                // We're within bounds, let's check for a collision
                foreach (var point in newPlacement.ActiveCells)
                {
                    var panel = GetGridPoint(point.X, point.Y, gameGrid);

                    moveIsValid = panel.BackColor == defaultBackgroundColour;

                    if (!moveIsValid)
                    {
                        foreach (var point1 in currentPlacement.ActiveCells)
                        {
                            var panel1 = GetGridPoint(point.X, point.Y, gameGrid);
                            panel.BackColor = Color.Aqua;
                        }

                        break;
                    }
                }
            }

            return moveIsValid;
        }

        /// <summary>
        /// Gets the Panel using the given X and Y coordinates.
        /// </summary>
        /// <param name="x">X co-ordinate.</param>
        /// <param name="y">Y co-ordinate.</param>
        /// <returns>The panel at the given co-ordinates.</returns>
        private Panel GetGridPoint(int x, int y, TableLayoutPanel gameGrid)
        {
            return (Panel)gameGrid.GetControlFromPosition(x, y);
        }
    }
}
