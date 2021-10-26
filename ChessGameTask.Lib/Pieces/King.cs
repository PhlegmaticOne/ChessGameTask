using ChessGame.Lib.Board;
using ChessGame.Lib.Helpers;
using System.Collections.Generic;

namespace ChessGame.Lib.Figures
{
    /// <summary>
    /// Represents king piece in chess game
    /// </summary>
    public class King : Piece
    {
        /// <summary>
        /// Copy constructor for a king that initializes new king instance with parameters equal to copying piece
        /// </summary>
        /// <param name="pieceToCopy">Copying piece</param>
        public King(Piece pieceToCopy) : base(pieceToCopy) { }
        /// <summary>
        /// Initializes new king instance with specified start position and color
        /// </summary>
        public King(Position startPosition, Color pieceColor) : base(startPosition, pieceColor) { }
        /// <summary>
        /// Calculates squares where king can go
        /// </summary>
        internal override List<Square> GetPossibleNewSquares(ChessBoard board) => CheckAround(board);
        /// <summary>
        /// Returns list of squares around king where it can go
        /// </summary>
        private List<Square> CheckAround(ChessBoard board)
        {
            var result = new List<Square>();
            for (int i = -1; i <= 1; i++)
            {
                var hor = (char)(CurrentPosition.Horizontal + i);
                for (int j = -1; j <= 1; j++)
                {
                    var ver = CurrentPosition.Vertical + j;
                    if (Position.CanBeOnBoard(hor, ver) && board[hor, ver].Piece.Color != Color)
                    {
                        result.Add(board[hor, ver]);
                    }
                }
            }
            return result;
        }
    }
}