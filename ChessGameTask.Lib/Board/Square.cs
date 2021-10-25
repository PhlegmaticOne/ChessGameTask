using ChessGame.Lib.Figures;
using ChessGame.Lib.Helpers;
using System;

namespace ChessGame.Lib.Board
{
    /// <summary>
    /// Represents square that can contain piece on a chess board
    /// </summary>
    public sealed class Square : IEquatable<Square>
    {
        /// <summary>
        /// Initializes new square instance with specified position on a chess board and piece
        /// </summary>
        /// <param name="boardPosition">Position on a chess board</param>
        /// <param name="piece">Piece</param>
        public Square(Position boardPosition, Piece piece) => (BoardPosition, Piece) = (boardPosition, piece);
        /// <summary>
        /// Position on a chess board
        /// </summary>
        public Position BoardPosition { get; set; }
        /// <summary>
        /// Piece on this square
        /// </summary>
        public Piece Piece { get; set; }
        /// <summary>
        /// Equality operator that checks two squares on their equality to each other
        /// </summary>
        /// <param name="first">First square to check</param>
        /// <param name="second">Second square to check</param>
        public static bool operator ==(Square first, Square second) => first.Equals(second);
        /// <summary>
        /// Unequality operator that checks two squares on their unequality to each other 
        /// </summary>
        /// <param name="first">First square to check</param>
        /// <param name="second">Second square to check</param>
        public static bool operator !=(Square first, Square second) => !first.Equals(second);
        /// <summary>
        /// Checks specified square on its equality to this square instance 
        /// </summary>
        /// <param name="other">Specified square</param>
        public bool Equals(Square other) => BoardPosition == other?.BoardPosition;
        /// <summary>
        /// Converts instance of square to its string representation based on piece on it
        /// </summary>
        public override string ToString() => string.Format("{0}", Piece);
        /// <summary>
        /// Calculates hash code of current square instance 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => unchecked(Piece.GetHashCode() + BoardPosition.GetHashCode());
        /// <summary>
        /// Checks any object on its equality to this square instance 
        /// </summary>
        /// <param name="obj">Any object</param>
        public override bool Equals(object obj) => Equals(obj as Square);
    }
}