using ChessGame.Lib.Board;
using ChessGame.Lib.Figures;
using ChessGame.Lib.Players;
using System.Linq;

namespace ChessGame.Lib.GameLogic
{
    /// <summary>
    /// Represents instance that checks current state of a chess board
    /// </summary>
    internal class GameStateCheker
    {
        /// <summary>
        /// Current chess board with pieces
        /// </summary>
        private readonly ChessBoard _chessBoard;
        /// <summary>
        /// Initializes new checker instance with a specialized chess board
        /// </summary>
        internal GameStateCheker(ChessBoard chessBoard) => _chessBoard = chessBoard;
        /// <summary>
        /// Check if moved piece make a check to aby enemy piece
        /// </summary>
        /// <returns>Updated movement result</returns>
        internal MovementResult UpdateAfterMovement(MovementResult movementResult)
        {
            if (MovedFigureCanHitKing(movementResult.MovedPiece))
            {
                movementResult.UpdateState(MovementResultType.Check);
            }
            return movementResult;
        }
        /// <summary>
        /// Checks if moving piece opens king with a similar to moving piece color
        /// </summary>
        /// <param name="piece">Moving piece</param>
        /// <param name="newSquare">Square when piece want to go</param>
        /// <param name="anotherPlayer">Another player</param>
        internal PreMovementResultType CheckForOpeningKing(Piece piece, Square newSquare, Player anotherPlayer)
        {
            var possibleMoves = piece.GetPossibleNewSquares(_chessBoard);
            if (possibleMoves.Contains(newSquare) == false)
            {
                return PreMovementResultType.WrongMovement;
            }
            var pieceOnNewPosition = newSquare.Piece;
            bool isBelongsToPlayer = pieceOnNewPosition is not EmptyPiece;
            if(isBelongsToPlayer) anotherPlayer.Pieces.Remove(pieceOnNewPosition);
            _chessBoard.SwapPieces(piece, pieceOnNewPosition);
            var result = anotherPlayer.Pieces.Any(p => p.GetPossibleNewSquares(_chessBoard).Any(p => p.Piece is King)) ?
                                              PreMovementResultType.KingWillBeHitted : PreMovementResultType.CorrectMovement;
            _chessBoard.SwapPieces(pieceOnNewPosition, piece);
            if(isBelongsToPlayer) anotherPlayer.Pieces.Add(pieceOnNewPosition);
            return result;
        }
        /// <summary>
        /// Checks if piece of current player saves king with color similar to moving piece color
        /// </summary>
        /// <param name="piece">Moving piece</param>
        /// <param name="newSquare">Square when piece want to go</param>
        /// <param name="anotherPlayer">Another player</param>
        internal PreMovementResultType CanPieceSaveKing(Piece piece, Square newSquare, Player anotherPlayer)
        {
            var possibleMoves = piece.GetPossibleNewSquares(_chessBoard);
            if (possibleMoves.Contains(newSquare) == false)
            {
                return PreMovementResultType.WrongMovement;
            }
            var pieceOnNewPosition = newSquare.Piece;

            bool isBelongsToPlayer = pieceOnNewPosition is not EmptyPiece;
            if (isBelongsToPlayer) anotherPlayer.Pieces.Remove(pieceOnNewPosition);
            _chessBoard.SwapPieces(piece, pieceOnNewPosition);
            var result = anotherPlayer.Pieces.All(p => p.GetPossibleNewSquares(_chessBoard).All(p => p.Piece is not King)) ?
                         PreMovementResultType.PieceResolvingCheckmateFinded : PreMovementResultType.KingWillBeHitted;
            _chessBoard.SwapPieces(pieceOnNewPosition, piece);
            if (isBelongsToPlayer) anotherPlayer.Pieces.Add(pieceOnNewPosition);
            return result;
        }
        /// <summary>
        /// Checks if any piece of an enemy can save it's king
        /// </summary>
        /// <param name="anotherPlayer">Another player</param>
        /// <param name="pieceThatMadeCheck">Piece that was made a check</param>
        internal PreMovementResultType CanAnyPieceSaveKing(Player anotherPlayer, Piece pieceThatMadeCheck)
        {
            foreach (var piece in anotherPlayer.Pieces)
            {
                var pieceBeginPosition = piece.CurrentPosition;
                foreach (var move in piece.GetPossibleNewSquares(_chessBoard))
                {
                    var pieceOnNewPosition = _chessBoard[move.BoardPosition.Horizontal, move.BoardPosition.Vertical].Piece;
                    _chessBoard.SwapPieces(piece, pieceOnNewPosition);
                    if (move.BoardPosition == pieceThatMadeCheck.CurrentPosition ||
                        pieceThatMadeCheck.GetPossibleNewSquares(_chessBoard).All(p => p.Piece is not King))
                    {
                        _chessBoard.SwapPieces(piece, _chessBoard[pieceBeginPosition.Horizontal, pieceBeginPosition.Vertical].Piece);
                        return PreMovementResultType.PieceResolvingCheckmateFinded;
                    }
                    _chessBoard.SwapPieces(piece, pieceOnNewPosition);
                }
            }
            return PreMovementResultType.KingWillBeHitted;
        } 
        /// <summary>
        /// Checks if moved piece makes a check to an enemy
        /// </summary>
        /// <param name="moved">Moved piece</param>
        private bool MovedFigureCanHitKing(Piece moved) => moved.GetPossibleNewSquares(_chessBoard).Any(p => p.Piece is King);
    }
}