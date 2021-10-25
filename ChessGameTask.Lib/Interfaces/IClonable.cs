namespace ChessGame.Lib.Board
{
    /// <summary>
    /// Represents interface for cloning any object
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    public interface IClonable<T>
    {
        /// <summary>
        /// Clones object
        /// </summary>
        /// <returns>New object which is equal to copying</returns>
        T Clone();
    }
}
