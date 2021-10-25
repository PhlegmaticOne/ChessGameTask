using ChessGame.Lib.Board;
using ChessGame.Lib.Figures;
using ChessGame.Lib.GameLogic;
using ChessGame.Lib.Helpers;
using ChessGame.Lib.Players;
using ChessGameTask.Lib.Loggers;
using System.Linq;

namespace ChessGame.Lib
{
    /// <summary>
    /// Represents chess game beetween two players
    /// </summary>
    public class ChessGameSimulation
    {
        /// <summary>
        /// Maximum count of steps to set a standoff of current game
        /// </summary>
        private const int MAX_EMPTY_MOVES_TO_STANDOFF = 50;
        /// <summary>
        /// Current state of  game after last piece movement
        /// </summary>
        private MovementResultType _currentState;
        /// <summary>
        /// Current pre movement result of a last pre moving cheching
        /// </summary>
        private PreMovementResultType _currentPreMovementResult;
        /// <summary>
        /// Current chess board
        /// </summary>
        private readonly ChessBoard _chessBoard;
        /// <summary>
        /// Checker that checks state on a board
        /// </summary>
        private readonly GameStateCheker _gameStateCheker;
        /// <summary>
        /// Logger that logs movement results
        /// </summary>
        private readonly Logger<MovementResult> _logger;
        /// <summary>
        /// Initializes new chess game instance
        /// </summary>
        /// <param name="startBoard">Start chess board</param>
        /// <param name="firstPlayer">First player</param>
        /// <param name="secondPlayer">Second player</param>
        /// <param name="firstMoveColor">Color of a player that moves first</param>
        /// <param name="logger">Logger that logs movement results</param>
        public ChessGameSimulation(ChessBoard startBoard, Player firstPlayer, Player secondPlayer,
                                   Color firstMoveColor, Logger<MovementResult> logger)
        {
            _chessBoard = startBoard;
            CurrentChessBoard = startBoard.Clone();
            _logger = logger;
            _gameStateCheker = new GameStateCheker(_chessBoard);
            switch (firstMoveColor)
            {
                case Color.White:
                {
                    (CurrentPlayer, AnotherPlayer) = (firstPlayer.ChessPlayingColor == Color.White) ?
                                                       (firstPlayer, secondPlayer) : (secondPlayer, firstPlayer);
                    break;
                }
                case Color.Black:
                {
                    (CurrentPlayer, AnotherPlayer) = (firstPlayer.ChessPlayingColor == Color.Black) ?
                                                       (firstPlayer, secondPlayer) : (secondPlayer, firstPlayer);
                        break;
                }
            }
        }
        /// <summary>
        /// Winner of a current chess game
        /// </summary>
        public Player Winner { get; private set; }
        /// <summary>
        /// Current chess board
        /// </summary>
        public ChessBoard CurrentChessBoard { get; private set; }
        /// <summary>
        /// Current player that make movement
        /// </summary>
        public Player CurrentPlayer { get; private set; }
        /// <summary>
        /// Another player that was made previous movement
        /// </summary>
        public Player AnotherPlayer { get; private set; }
        /// <summary>
        /// Moves a piece to an specified square
        /// </summary>
        /// <returns>Movement result</returns>
        public MovementResultType Move(Piece piece, Square newSquare)
        {
            if (GameCanContinue() == false) return _currentState;

            if(_currentPreMovementResult == PreMovementResultType.KingWillBeHitted ||
               _currentPreMovementResult == PreMovementResultType.WrongMovement ||
               _currentState == MovementResultType.WrongMovement)
            {
                _currentState = MovementResultType.WrongMovement;
                return _currentState;
            }

            var movementResult = new Movement(piece, newSquare, _chessBoard).Move();
            if(movementResult.MovementResultType == MovementResultType.PawnTransformed)
            {
                CurrentPlayer.Pieces[CurrentPlayer.Pieces.FindIndex(i => i.CurrentPosition == movementResult.MovedPiece.CurrentPosition)] = movementResult.HittedPiece;
            }
            _currentState = _gameStateCheker.UpdateAfterMovement(movementResult).MovementResultType;
            if(_currentState == MovementResultType.Check)
            {
                if(_gameStateCheker.CanAnyPieceSaveKing(AnotherPlayer, movementResult.MovedPiece) == PreMovementResultType.KingWillBeHitted)
                {
                    _currentState = MovementResultType.Checkmate;
                    Winner = CurrentPlayer;
                }
            }
            switch (movementResult.MovementResultType)
            {
                case MovementResultType.MovedToEmptySquare:
                {
                    ++CurrentPlayer.EmptyMovesCount;
                    break;
                }
                case MovementResultType.HittedAnotherPiece:
                {
                    CurrentPlayer.HittedPieces.Add(movementResult.HittedPiece);
                    AnotherPlayer.Pieces.Remove(AnotherPlayer.Pieces.First(p => p.CurrentPosition == movementResult.HittedPiece.CurrentPosition));
                    CurrentPlayer.EmptyMovesCount = 0;
                    break;
                }
            }
            if (CurrentPlayer.EmptyMovesCount > MAX_EMPTY_MOVES_TO_STANDOFF)
            {
                _currentState = MovementResultType.Standoff;
            }
            movementResult.UpdateState(_currentState);
            _logger.Log(movementResult);
            SwapPlayers();
            CurrentChessBoard = _chessBoard.Clone();
            return _currentState;
        }
        /// <summary>
        /// Moves a piece to an specified square
        /// </summary>
        /// <returns>Movement result</returns>
        public MovementResultType Move(Piece piece, char horizontal, int vertical) => Move(piece, _chessBoard[horizontal, vertical]);
        /// <summary>
        /// Checks state of posiible move
        /// </summary>
        public PreMovementResultType PreMoveChecking(Piece piece, Square newSquare)
        {
            if (GameCanContinue() == false) return PreMovementResultType.GameEnded;
            _currentPreMovementResult =  (_currentState == MovementResultType.Check) ?
                                         _gameStateCheker.CanPieceSaveKing(piece, newSquare, AnotherPlayer) :
                                         _gameStateCheker.CheckForOpeningKing(piece, newSquare, AnotherPlayer);
            return _currentPreMovementResult;
        }
        /// <summary>
        /// Checks state of posiible move
        /// </summary>
        public PreMovementResultType PreMoveChecking(Piece piece, char horizontal, int vertical) => PreMoveChecking(piece, _chessBoard[horizontal, vertical]);
        /// <summary>
        /// Gets piece from the board that belong to a current player
        /// </summary>
        /// <returns>Real piece - piece belongs to a current player, empty piece - it is not</returns>
        public Piece GetPiece(char horizontal, int vertical)
        {
            var badResult = new EmptyPiece(Position.Unreachable, Color.None);
            if(Position.CanBeOnBoard(horizontal, vertical) == false)
            {
                return badResult;
            }
            var piece = _chessBoard[horizontal, vertical].Piece;
            if(CurrentPlayer.Pieces.Contains(piece) == false)
            {
                return badResult;
            }
            return piece;
        }
        /// <summary>
        /// Check if game is ended
        /// </summary>
        public bool GameCanContinue() => _currentState != MovementResultType.Checkmate && _currentState != MovementResultType.Standoff;
        /// <summary>
        /// Swaps current player with an another player
        /// </summary>
        private void SwapPlayers() => (CurrentPlayer, AnotherPlayer) = (AnotherPlayer, CurrentPlayer);
    }
}