namespace ChessGame.Lib.GameLogic
{
    /// <summary>
    /// Represents piece movement result
    /// </summary>
    public enum MovementResultType
    {
        MovedToEmptySquare,
        HittedAnotherPiece,
        PawnTransformed,
        Check,
        Checkmate,
        Standoff,
        WrongMovement
    }
}
