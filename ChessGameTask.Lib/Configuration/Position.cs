namespace ChessGame.Lib.Helpers
{
    /// <summary>
    /// Represents chess figure position
    /// </summary>
    public struct Position
    {
        /// <summary>
        /// Initializes new FigurePositin from row and column
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="vertical"></param>
        public Position(char horizontal, int vertical)
        {
            Horizontal = char.ToLower(horizontal);
            Vertical = vertical;
        }
        /// <summary>
        /// Identify horizontal position of figure
        /// </summary>
        public char Horizontal { get; set; }
        /// <summary>
        /// Identify vertical position of figure
        /// </summary>
        public int Vertical { get; set; }
        /// <summary>
        /// Returns full position representation consisting of row and column
        /// </summary>
        public string FullName => string.Format("{0}{1}", Horizontal, Vertical);
        /// <summary>
        /// Represents a position that can't have any piece
        /// </summary>
        public static Position Unreachable => new(char.MinValue, int.MinValue);
        /// <summary>
        /// Checks that position representing by horizontal and vertical prameters can be on a chess board
        /// </summary>
        /// <param name="horizontal">Horizontal parameter</param>
        /// <param name="vertical">Vertical parameter</param>
        public static bool CanBeOnBoard(char horizontal, int vertical) => (vertical >= 1 && vertical <= 8) && (horizontal >= 'a' && horizontal <= 'h');
        /// <summary>
        /// Checks that position can be on a chess board
        /// </summary>
        /// <param name="position">Specified position</param>
        public static bool CanBeOnBoard(Position position) => CanBeOnBoard(position.Horizontal, position.Vertical);
        /// <summary>
        /// Overloads == operator for two chess figures
        /// </summary>
        /// <param name="left">First figure position to check it's equality to another</param>
        /// <param name="right">Second figure position to check it's equality to another</param>
        /// <returns></returns>
        public static bool operator ==(Position left, Position right) => left.Equals(right);
        /// <summary>
        /// Overloads != operator for two chess figures
        /// </summary>
        /// <param name="left">First figure position to check it's unequality to another</param>
        /// <param name="right">Second figure position to check it's unequality to another</param>
        /// <returns></returns>
        public static bool operator !=(Position left, Position right) => !(left == right);
        /// <summary>
        /// Creates string representation of an figure position
        /// </summary>
        /// <returns>String representation of an figure position</returns>
        public override string ToString() => FullName;
        /// <summary>
        /// Calculates hash code of an figure position
        /// </summary>
        /// <returns>Hash code of an figure position</returns>
        public override int GetHashCode() => Horizontal.GetHashCode() ^ Vertical.GetHashCode();
        /// <summary>
        /// Checks equality of two figures positions
        /// </summary>
        /// <param name="obj">Position to check</param>
        /// <returns>true - positions are equal, false - they are not</returns>
        public override bool Equals(object obj) => obj is Position position && FullName == position.FullName;
    }
}
