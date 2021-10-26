using ChessGame.Lib.Board;
using ChessGame.Lib.Helpers;
using ChessGameTask.Lib.Helpers;
using System.Collections.Generic;

namespace ChessGame.Lib.Figures
{
    /// <summary>
    /// Represents queen piece in chess game
    /// </summary>
    public class Queen : Piece
    {
        /// <summary>
        /// Copy constructor for a queen that initializes new queen instance with parameters equal to copying piece
        /// </summary>
        /// <param name="pieceToCopy">Copying piece</param>
        public Queen(Piece pieceToCopy) : base(pieceToCopy) { }
        /// <summary>
        /// Initializes new queen instance with specified start position and color
        /// </summary>
        public Queen(Position startPosition, Color pieceColor) : base(startPosition, pieceColor) { }
        /// <summary>
        /// Calculates squares where queen can go
        /// </summary>
        internal override List<Square> GetPossibleNewSquares(ChessBoard board) =>
                                      GetPositions(board, QueenPositionCheckDirection.Left)
                                     .AddRangeFluent(GetPositions(board, QueenPositionCheckDirection.Right))
                                     .AddRangeFluent(GetPositions(board, QueenPositionCheckDirection.Top))
                                     .AddRangeFluent(GetPositions(board, QueenPositionCheckDirection.Bottom))
                                     .AddRangeFluent(GetPositions(board, QueenPositionCheckDirection.LeftTop))
                                     .AddRangeFluent(GetPositions(board, QueenPositionCheckDirection.LeftBottom))
                                     .AddRangeFluent(GetPositions(board, QueenPositionCheckDirection.RightBottom))
                                     .AddRangeFluent(GetPositions(board, QueenPositionCheckDirection.RightTop));
        /// <summary>
        /// Gets new squares to a list where quenn can go depending on a checking direction
        /// </summary>
        private List<Square> GetPositions(ChessBoard board, QueenPositionCheckDirection direction)
        {
            var result = new List<Square>();
            var currentPosition = CurrentPosition;
            do
            {
                switch (direction)
                {
                    case QueenPositionCheckDirection.LeftTop:
                    {
                        --currentPosition.Horizontal;
                        ++currentPosition.Vertical;
                        break;
                    }
                    case QueenPositionCheckDirection.LeftBottom:
                    {
                        --currentPosition.Horizontal;
                        --currentPosition.Vertical;
                        break;
                    }
                    case QueenPositionCheckDirection.RightBottom:
                    {
                        ++currentPosition.Horizontal;
                        --currentPosition.Vertical;
                        break;
                    }
                    case QueenPositionCheckDirection.RightTop:
                    {
                        ++currentPosition.Horizontal;
                        ++currentPosition.Vertical;
                        break;
                    }
                    case QueenPositionCheckDirection.Left:
                    {
                        --currentPosition.Horizontal;
                        break;
                    }
                    case QueenPositionCheckDirection.Right:
                    {
                        ++currentPosition.Horizontal;
                        break;
                    }
                    case QueenPositionCheckDirection.Top:
                    {
                        ++currentPosition.Vertical;
                        break;
                    }
                    case QueenPositionCheckDirection.Bottom:
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
        /// Represents queen movement checking direction
        /// </summary>
        private enum QueenPositionCheckDirection
        {
            Left,
            Right,
            Top,
            Bottom,
            LeftTop,
            LeftBottom,
            RightTop,
            RightBottom
        }
    }
}