using ChessGame.Lib.Board;
using ChessGame.Lib.Figures;
using System;
using System.Collections.Generic;

namespace ChessGameTask.Lib.Helpers
{
    /// <summary>
    /// Two dimensional array of squares extesnsions
    /// </summary>
    public static class MatriceOfSquaresExtensions
    {
        /// <summary>
        /// Retrieves squares from the list depending on a specified predicate
        /// </summary>
        /// <param name="squares">List of squares from that squares will be retrieved</param>
        /// <param name="predicate">Specified prediate for getting squares</param>
        public static List<Piece> GetPieces(this Square[,] squares, Func<Square, bool> predicate)
        {
            var result = new List<Piece>();
            for (int i = 0; i < squares.GetLength(0); i++)
            {
                for (int j = 0; j < squares.GetLength(1); j++)
                {
                    if(predicate.Invoke(squares[i, j]))
                    {
                        result.Add(squares[i, j].Piece);
                    }
                }
            }
            return result;
        }
    }
}
