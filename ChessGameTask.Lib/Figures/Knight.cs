using ChessGame.Lib.Board;
using ChessGame.Lib.Helpers;
using ChessGameTask.Lib.Helpers;
using System.Collections.Generic;

namespace ChessGame.Lib.Figures
{
    /// <summary>
    /// Represents knight piece in chess game
    /// </summary>
    public class Knight : Piece
    {
        /// <summary>
        /// Copy constructor for a knight that initializes new knight instance with parameters equal to copying piece
        /// </summary>
        /// <param name="pieceToCopy">Copying piece</param>
        public Knight(Piece pieceToCopy) : base(pieceToCopy) { }
        /// <summary>
        /// Initializes new knight instance with specified start position and color
        /// </summary>
        public Knight(Position startPosition, Color pieceColor) : base(startPosition, pieceColor) { }
        /// <summary>
        /// Calculates squares where knight can go
        /// </summary>
        internal override List<Square> GetPossibleNewSquares(ChessBoard board) =>
                                     GetKnightSquares(board, KnightPositionCheckDirection.Vertical)
                                     .AddRangeFluent(GetKnightSquares(board, KnightPositionCheckDirection.Horizontal));
        /// <summary>
        /// Gets squares where knight can go depending on a direction of "Г" square checking
        /// </summary>
        private List<Square> GetKnightSquares(ChessBoard board, KnightPositionCheckDirection direction)
        {
            var result = new List<Square>();
            char hor = default; int ver = default;
            for (int i = 2; i >= -2; i -= 4)
            {
                switch (direction)
                {
                    case KnightPositionCheckDirection.Vertical:
                    {
                        ver = CurrentPosition.Vertical + i;
                        break;
                    }
                    case KnightPositionCheckDirection.Horizontal:
                    {
                        hor = (char)(CurrentPosition.Horizontal + i);
                        break;
                    }
                }
                for (int j = -1; j <= 1; j += 2)
                {
                    switch (direction)
                    {
                        case KnightPositionCheckDirection.Vertical:
                        {
                            hor = (char)(CurrentPosition.Horizontal + j);
                            break;
                        }
                        case KnightPositionCheckDirection.Horizontal:
                        {
                            ver = CurrentPosition.Vertical + j;
                            break;
                        }
                    }
                    if (Position.CanBeOnBoard(hor, ver) && board[hor, ver].Piece.Color != Color)
                    {
                        result.Add(board[hor, ver]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Represents direction of "Г" square checking
        /// </summary>
        private enum KnightPositionCheckDirection
        {
            Vertical,
            Horizontal
        }
    }
}