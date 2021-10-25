using ChessGame.Lib.Board;
using System.Collections.Generic;

namespace ChessGameTask.Lib.Helpers
{
    /// <summary>
    /// Extension for list
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Adds range of square to list fluently
        /// </summary>
        /// <param name="newSquares">New squares to add</param>
        /// <param name="squaresToAdd">List in which it is necessary to add new values</param>
        /// <returns>List with added values</returns>
        public static List<Square> AddRangeFluent(this List<Square> newSquares, List<Square> squaresToAdd)
        {
            newSquares.AddRange(squaresToAdd);
            return newSquares;
        }
        /// <summary>
        /// Adds square to list fluently
        /// </summary>
        /// <param name="newSquares">New square to add</param>
        /// <param name="squareToAdd">List in which it is necessary to add new value</param>
        /// <returns>List with added value</returns>
        public static List<Square> AddFluent(this List<Square> newSquares, Square squareToAdd)
        {
            newSquares.Add(squareToAdd);
            return newSquares;
        }
    }
}
