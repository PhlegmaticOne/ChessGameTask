using ChessGame.Lib.Board;
using ChessGame.Lib.Helpers;
using ChessGameTask.Lib.Helpers;
using System.Collections.Generic;

namespace ChessGame.Lib.Figures
{
    /// <summary>
    /// Represents rook piece in chess game
    /// </summary>
    public class Rook : Piece
    {
        /// <summary>
        /// Copy constructor for a rook that initializes new rook instance with parameters equal to copying piece
        /// </summary>
        /// <param name="pieceToCopy">Copying piece</param>
        public Rook(Piece pieceToCopy) : base(pieceToCopy) { }
        /// <summary>
        /// Initializes new rook instance with specified start position and color
        /// </summary>
        public Rook(Position startPosition, Color pieceColor) : base(startPosition, pieceColor) { }
        /// <summary>
        /// Calculates squares where rook can go
        /// </summary>
        internal override List<Square> GetPossibleNewSquares(ChessBoard board) =>
                                   GetPositions(board, RookPositionCheckDirection.Left)
                                  .AddRangeFluent(GetPositions(board, RookPositionCheckDirection.Right))
                                  .AddRangeFluent(GetPositions(board, RookPositionCheckDirection.Top))
                                  .AddRangeFluent(GetPositions(board, RookPositionCheckDirection.Bottom));
        /// <summary>
        /// Gets new squares where rook can go depending on a checking direction type
        /// </summary>
        private List<Square> GetPositions(ChessBoard board, RookPositionCheckDirection direction)
        {
            var result = new List<Square>();
            var currentPosition = CurrentPosition;
            do
            {
                switch (direction)
                {
                    case RookPositionCheckDirection.Left:
                        {
                            --currentPosition.Horizontal;
                            break;
                        }
                    case RookPositionCheckDirection.Right:
                        {
                            ++currentPosition.Horizontal;
                            break;
                        }
                    case RookPositionCheckDirection.Top:
                        {
                            ++currentPosition.Vertical;
                            break;
                        }
                    case RookPositionCheckDirection.Bottom:
                        {
                            --currentPosition.Vertical;
                            break;
                        }
                    default: break;
                }
                if (Position.CanBeOnBoard(currentPosition) == false)
                {
                    return result;
                }
                if (board.ContainsRealPiece(currentPosition))
                {
                    if (board[currentPosition.Horizontal, currentPosition.Vertical].Piece.Color != Color)
                    {
                        result.Add(board[currentPosition.Horizontal, currentPosition.Vertical]);
                    }
                    return result;
                }
                else
                {
                    result.Add(board[currentPosition.Horizontal, currentPosition.Vertical]);
                }
            } while (true);
        }
        /// <summary>
        /// Represents movement check direction for a rook
        /// </summary>
        private enum RookPositionCheckDirection
        {
            Left,
            Right,
            Top,
            Bottom
        }
    }
}