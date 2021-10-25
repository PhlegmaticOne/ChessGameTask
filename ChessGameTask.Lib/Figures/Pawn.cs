using ChessGame.Lib.Board;
using ChessGame.Lib.Helpers;
using System.Collections.Generic;

namespace ChessGame.Lib.Figures
{
    /// <summary>
    /// Represents pawn piece in chess game
    /// </summary>
    public class Pawn : Piece
    {
        /// <summary>
        /// First pawn position
        /// </summary>
        private Position _firstPosition;
        /// <summary>
        /// Copy constructor for a pawn that initializes new pawn instance with parameters equal to copying piece
        /// </summary>
        /// <param name="pieceToCopy">Copying piece</param>
        public Pawn(Piece pieceToCopy) : base(pieceToCopy) { }
        /// <summary>
        /// Initializes new pawn instance with specified start position and color
        /// </summary>
        public Pawn(Position startPosition, Color pieceColor) : base(startPosition, pieceColor) => _firstPosition = startPosition;
        /// <summary>
        /// Calculates squares where pawn can go
        /// </summary>
        internal override List<Square> GetPossibleNewSquares(ChessBoard board)
        {
            var result = new List<Square>();
            switch (Color)
            {
                case Color.White:
                {
                    if (CurrentPosition == _firstPosition)
                    {
                         TryAddEmptySquare(CurrentPosition.Horizontal, CurrentPosition.Vertical + 2, result, board);
                    }
                    TryAddEmptySquare(CurrentPosition.Horizontal, CurrentPosition.Vertical + 1, result, board);
                    TryAddEnemiesMoves((char)(CurrentPosition.Horizontal - 1), CurrentPosition.Vertical + 1, result, board);
                    TryAddEnemiesMoves((char)(CurrentPosition.Horizontal + 1), CurrentPosition.Vertical + 1, result, board);
                    break;
                }
                case Color.Black:
                {
                    if (CurrentPosition == _firstPosition)
                    {
                        TryAddEmptySquare(CurrentPosition.Horizontal, CurrentPosition.Vertical - 2, result, board);
                    }
                    TryAddEmptySquare(CurrentPosition.Horizontal, CurrentPosition.Vertical - 1, result, board);
                    TryAddEnemiesMoves((char)(CurrentPosition.Horizontal - 1), CurrentPosition.Vertical - 1, result, board);
                    TryAddEnemiesMoves((char)(CurrentPosition.Horizontal + 1), CurrentPosition.Vertical - 1, result, board);
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// Adds new square where pawn can go if it contains enemy piece
        /// </summary>
        private void TryAddEnemiesMoves(char horizontal, int vertical, List<Square> squares, ChessBoard board)
        {
            if (Position.CanBeOnBoard(horizontal, vertical) && board.ContainsRealPiece(horizontal, vertical)
                                                            && board[horizontal, vertical].Piece.Color != Color)
            {
                squares.Add(board[horizontal, vertical]);
            }
        }
        /// <summary>
        /// Adds new square where pawn can go if it contains empty piece
        /// </summary>
        private void TryAddEmptySquare(char horizontal, int vertical, List<Square> squares, ChessBoard board)
        {
            if (Position.CanBeOnBoard(horizontal, vertical) && board.ContainsEmptyPiece(horizontal, vertical))
            {
                squares.Add(board[horizontal, vertical]);
            }
        }
    }
}