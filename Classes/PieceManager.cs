using System;
using System.Collections.Generic;
using System.Drawing;
using Tetris.Models;

namespace Tetris.Classes
{
    public class PieceManager
    {
        private readonly Random _random;
        private readonly List<Piece> _pieces = new List<Piece>(5);

        public PieceManager()
        {
            BuildPieces();
            _random = new Random();
        }

        /// <summary>
        /// Populates a list of available pieces, and their data.
        /// </summary>
        private void BuildPieces()
        {
            foreach (Piece.Names tetromino in Enum.GetValues(typeof(Piece.Names)))
            {
                var piece = new Piece
                {
                    Name = tetromino
                };

                switch (tetromino)
                {
                    case Piece.Names.I:
                        piece.Color = Color.Aqua;
                        piece.Shape = new[,]
                        {
                            { 0, 0, 0, 0 },
                            { 1, 1, 1, 1 },
                            { 0, 0, 0, 0 },
                            { 0, 0, 0, 0 },
                        };
                        break;
                    case Piece.Names.J:
                        piece.Color = Color.Blue;
                        piece.Shape = new[,]
                        {
                            { 1, 0, 0 },
                            { 1, 1, 1 },
                            { 0, 0, 0 },
                            { 0, 0, 0 },
                        };
                        break;
                    case Piece.Names.L:
                        piece.Color = Color.Orange;
                        piece.Shape = new[,]
                        {
                            { 0, 0, 1 },
                            { 1, 1, 1 },
                            { 0, 0, 0 },
                            { 0, 0, 0 },
                        };
                        break;
                    case Piece.Names.O:
                        piece.Color = Color.Yellow;
                        piece.Shape = new[,]
                        {
                            { 0, 1, 1, 0 },
                            { 0, 1, 1, 0 },
                            { 0, 0, 0, 0 },
                            { 0, 0, 0, 0 },
                        };
                        break;
                    case Piece.Names.S:
                        piece.Color = Color.LawnGreen;
                        piece.Shape = new[,]
                        {
                            { 0, 1, 1 },
                            { 1, 1, 0 },
                            { 0, 0, 0 },
                            { 0, 0, 0 },
                        };
                        break;
                    case Piece.Names.T:
                        piece.Color = Color.Purple;
                        piece.Shape = new[,]
                        {
                            { 0, 1, 0 },
                            { 1, 1, 1 },
                            { 0, 0, 0 },
                            { 0, 0, 0 },
                        };
                        break;
                    case Piece.Names.Z:
                        piece.Color = Color.DarkRed;
                        piece.Shape = new[,]
                        {
                            { 1, 1, 0 },
                            { 0, 1, 1 },
                            { 0, 0, 0 },
                            { 0, 0, 0 },
                        };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _pieces.Add(piece);
            }
        }

        /// <summary>
        /// Fetches a random Piece from the available pieces.
        /// </summary>
        /// <returns>A randomly chosen Piece.</returns>
        public Piece FetchNewPiece()
        {
            return _pieces[_random.Next(_pieces.Count)];
        }
    }
}
