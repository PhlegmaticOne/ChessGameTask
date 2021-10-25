using ChessGame.Lib.Board;
using ChessGame.Lib.Helpers;
using System;
using System.Collections.Generic;

namespace ChessGame.Lib.Figures
{
    /// <summary>
    /// Represents chess figure on the board
    /// </summary>
    public abstract class Piece : IClonable<Piece>
    {
        /// <summary>
        /// Initializes new chess figure from start figure position and figure color 
        /// </summary>
        /// <param name="startPosition">Start position of figure</param>
        /// <param name="color">Figure color</param>
        public Piece(Position startPosition, Color color) => (CurrentPosition, Color) = (startPosition, color);
        /// <summary>
        /// Returns new chess figure which is equal to copying figure
        /// </summary>
        /// <param name="pieceToCopy">Firgure to copy</param>
        public Piece(Piece pieceToCopy) => (CurrentPosition, Color) = (pieceToCopy.CurrentPosition, pieceToCopy.Color);
        /// <summary>
        /// Current figure position
        /// </summary>
        public Position CurrentPosition { get; set; }
        /// <summary>
        /// Color of figure
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Abstract method for calculating squares where specified pieces can go to depending on a state of chess board
        /// </summary>
        /// <param name="board">Chess board where piece stands</param>
        /// <returns>Collection of squares where piece can go</returns>
        internal abstract List<Square> GetPossibleNewSquares(ChessBoard board);
        /// <summary>
        /// Sets new position to a piece
        /// </summary>
        /// <param name="position">New specified position</param>
        public void SetPosition(Position position) => CurrentPosition = position;
        /// <summary>
        /// Clone this piece instance into a new piece which is equal to cloning piece
        /// </summary>
        /// <returns></returns>
        public Piece Clone() => Activator.CreateInstance(GetType(), CurrentPosition, Color) as Piece;
        /// <summary>
        /// Transforms instance of chess figure to string
        /// </summary>
        /// <returns>String representation of chess figure</returns>
        public override string ToString() => string.Format("{0}: {1}", GetType().Name, CurrentPosition.FullName);
        /// <summary>
        /// Checks two chess figures for equality
        /// </summary>
        /// <param name="obj">Other figure to check</param>
        /// <returns>true - figures are equal, false - they are not</returns>
        public override bool Equals(object obj) => obj is Piece piece && piece.CurrentPosition == CurrentPosition && piece.Color == Color;
        /// <summary>
        /// Calculates hashcode of chess figure
        /// </summary>
        /// <returns>Hashcode of figure based of it's proterties state</returns>
        public override int GetHashCode() => unchecked(CurrentPosition.GetHashCode() + Color.GetHashCode());
    }
}