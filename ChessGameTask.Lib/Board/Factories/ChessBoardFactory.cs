namespace ChessGame.Lib.Board
{
    /// <summary>
    /// Base class for creating different chess board variations
    /// </summary>
    public abstract class ChessBoardFactory
    {
        /// <summary>
        /// Creates instance of chess board
        /// </summary>
        /// <returns>Chess board with pieces</returns>
        public abstract ChessBoard Create();
    }
}
