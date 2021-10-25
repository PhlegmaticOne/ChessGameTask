using ChessGame.Lib.Figures;
using ChessGame.Lib.Helpers;

namespace ChessGame.Lib.GameLogic
{
    /// <summary>
    /// Represents result of a piece movements
    /// </summary>
    public class MovementResult
    {
        /// <summary>
        /// Initializes new instance of a movement result
        /// </summary>
        /// <param name="hittedPiece">Piece that was hitted by a moved piece</param>
        /// <param name="movedPiece">Piece tat was moved</param>
        /// <param name="previousPosition">Previous position of a moved piece</param>
        /// <param name="movementResultType">Result of a piece movement</param>
        public MovementResult(Piece hittedPiece, Piece movedPiece, Position previousPosition, MovementResultType movementResultType)
            => (MovedPiece, HittedPiece, MovementResultType, PreviousPosition) =
               (movedPiece, hittedPiece, movementResultType, previousPosition);
        /// <summary>
        /// Updates movement result type of this instance
        /// </summary>
        /// <param name="movementResultType"></param>
        public void UpdateState(MovementResultType movementResultType) => MovementResultType = movementResultType;
        /// <summary>
        /// Piece tat was moved
        /// </summary>
        public Piece MovedPiece { get; private set; }
        /// <summary>
        /// Piece that was hitted by a moved piece
        /// </summary>
        public Piece HittedPiece {  get; set; }
        /// <summary>
        /// Result of a piece movement
        /// </summary>
        public MovementResultType MovementResultType { get; private set; }
        /// <summary>
        /// Previous position of a moved piece
        /// </summary>
        public Position PreviousPosition { get; private set; }
        /// <summary>
        /// Converts instance of a movement result to string depends on a movement result type
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result =  string.Format("{0} {1} {2} moved to {3}. Result is {4}.",
                                   MovedPiece.Color, MovedPiece.GetType().Name, PreviousPosition,
                                   MovedPiece.CurrentPosition, MovementResultType);
            if (HittedPiece is not EmptyPiece)
            {
                result += string.Format(" Hitted piece: {0}", HittedPiece.ToString());
            }
            if(MovementResultType == MovementResultType.PawnTransformed)
            {
                result += " Tranformed to Queen" + MovedPiece.CurrentPosition.FullName;
            }
            return result;
        }
    }
}
