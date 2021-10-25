using ChessGame.Lib.Board;
using ChessGame.Lib.Figures;
using ChessGame.Lib.Helpers;

namespace ChessGame.Lib.GameLogic
{
    /// <summary>
    /// Represents piece movement instance
    /// </summary>
    public class Movement
    {
        /// <summary>
        /// Piece that moved at another place
        /// </summary>
        private readonly Piece _pieceToMove;
        /// <summary>
        /// New piece position of the board
        /// </summary>
        private readonly Square _newSquare;
        /// <summary>
        /// Current chess board
        /// </summary>
        private readonly ChessBoard _chessBoard;
        /// <summary>
        /// Initializes new instance of movement with specified moving piece, new piece position, current chess board
        /// </summary>
        public Movement(Piece pieceToMove, Square newSquare, ChessBoard chessBoard) => (_pieceToMove, _newSquare, _chessBoard) =
                                                                                       (pieceToMove, newSquare, chessBoard);
        /// <summary>
        /// Makes figure movement
        /// </summary>
        /// <returns>Movement result that contains information about movement that was made</returns>
        public MovementResult Move()
        {
            var hittedPiece = _newSquare.Piece;
            var clonedHittedPiece = hittedPiece.Clone();
            var previousPosition = _pieceToMove.CurrentPosition;
            var result = (_chessBoard.ContainsRealPiece(_newSquare)) ? MovementResultType.HittedAnotherPiece : MovementResultType.MovedToEmptySquare;
            _chessBoard.SwapPieces(_pieceToMove, new EmptyPiece(hittedPiece));
            if (_pieceToMove is Pawn pawn)
            {
                switch (pawn.Color)
                {
                    case Color.White:
                    {
                        if(PawnReachedLastLine(8))
                        {
                            var queen = new Queen(pawn);
                            clonedHittedPiece = queen;
                            _chessBoard[_pieceToMove.CurrentPosition.Horizontal, _pieceToMove.CurrentPosition.Vertical].Piece = queen;
                            result = MovementResultType.PawnTransformed;
                        }
                        break;
                    }
                    case Color.Black:
                    {
                        if (PawnReachedLastLine(1))
                        {
                            var queen = new Queen(pawn);
                            clonedHittedPiece = queen;
                            _chessBoard[_pieceToMove.CurrentPosition.Horizontal, _pieceToMove.CurrentPosition.Vertical].Piece = queen;
                            result = MovementResultType.PawnTransformed;
                        }
                        break;
                    }
                }
            }
            return new MovementResult(clonedHittedPiece, _pieceToMove, previousPosition, result);
        }
        /// <summary>
        /// Checks if pawn reached necessary line
        /// </summary>
        private bool PawnReachedLastLine(int vertical) => _pieceToMove.CurrentPosition.Vertical == vertical;
    }
}
