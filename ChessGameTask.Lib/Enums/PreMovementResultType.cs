namespace ChessGame.Lib.GameLogic
{
    /// <summary>
    /// Represents result that piece movement can lead to
    /// </summary>
    internal enum PreMovementResultType
    {
        KingWillBeHitted,
        PieceResolvingCheckmateFinded,
        WrongMovement,
        CorrectMovement,
    }
}
