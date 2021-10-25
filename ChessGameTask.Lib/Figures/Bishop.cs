using ChessGame.Lib.Board;
using ChessGame.Lib.Helpers;
using ChessGameTask.Lib.Helpers;
using System.Collections.Generic;

namespace ChessGame.Lib.Figures
{
    /// <summary>
    /// Represents bishop piece in chess game
    /// </summary>
    public class Bishop : Piece
    {
        /// <summary>
        /// Copy constructor for a bishop that initializes new bishop instance with parameters equal to copying piece
        /// </summary>
        /// <param name="pieceToCopy">Copying piece</param>
        public Bishop(Piece pieceToCopy) : base(pieceToCopy) { }
        /// <summary>
        /// Initializes new bishop instance with specified start position and color
        /// </summary>
        public Bishop(Position startPosition, Color pieceColor) : base(startPosition, pieceColor) { }
        /// <summary>
        /// Calculates squares where bishop can go
        /// </summary>
        internal override List<Square> GetPossibleNewSquares(ChessBoard board) => 
                                   GetPositions(board, BishopPositionCheckDirection.LeftBottom)
                                  .AddRangeFluent(GetPositions(board, BishopPositionCheckDirection.LeftTop))
                                  .AddRangeFluent(GetPositions(board, BishopPositionCheckDirection.RightTop))
                                  .AddRangeFluent(GetPositions(board, BishopPositionCheckDirection.RightBottom));
        /// <summary>
        /// Gets squares where bishop can go depending on a checking direction
        /// </summary>
        private List<Square> GetPositions(ChessBoard board, BishopPositionCheckDirection direction)
        {
            var result = new List<Square>();
            var currentPosition = CurrentPosition;
            do
            {
                switch (direction)
                {
                    case BishopPositionCheckDirection.LeftTop:
                        {
                            --currentPosition.Horizontal;
                            ++currentPosition.Vertical;
                            break;
                        }
                    case BishopPositionCheckDirection.LeftBottom:
                        {
                            --currentPosition.Horizontal;
                            --currentPosition.Vertical;
                            break;
                        }
                    case BishopPositionCheckDirection.RightBottom:
                        {
                            ++currentPosition.Horizontal;
                            --currentPosition.Vertical;
                            break;
                        }
                    case BishopPositionCheckDirection.RightTop:
                        {
                            ++currentPosition.Horizontal;
                            ++currentPosition.Vertical;
                            break;
                        }
                    default: break;
                }
                if(Position.CanBeOnBoard(currentPosition) == false)
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
        /// Represents checking direction for squares where bishop can go
        /// </summary>
        private enum BishopPositionCheckDirection
        {
            LeftTop,
            LeftBottom,
            RightTop,
            RightBottom
        }
    }
}
