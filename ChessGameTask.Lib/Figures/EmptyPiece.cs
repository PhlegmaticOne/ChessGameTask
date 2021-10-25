using ChessGame.Lib.Board;
using ChessGame.Lib.Helpers;
using System.Collections.Generic;

namespace ChessGame.Lib.Figures
{
    /// <summary>
    /// Represents empty piece that can't do anything in the chess game
    /// </summary>
    public class EmptyPiece : Piece
    {
        /// <summary>
        /// Copy constructor for a empty piece that initializes new empty piece instance with parameters equal to copying piece
        /// </summary>
        /// <param name="pieceToCopy"></param>
        public EmptyPiece(Piece pieceToCopy) : base(pieceToCopy) { }
        /// <summary>
        /// Initializes new empty piece instance with specified start position and color
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="color"></param>
        public EmptyPiece(Position startPosition, Color color) : base(startPosition, color) { }
        /// <summary>
        /// Calculates squares where empty piece can go
        /// </summary>
        /// <returns>Empty list of squares</returns>
        internal override List<Square> GetPossibleNewSquares(ChessBoard board) => new();
    }
}
