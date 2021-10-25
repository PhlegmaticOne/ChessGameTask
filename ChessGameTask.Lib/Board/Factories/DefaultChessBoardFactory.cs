using ChessGame.Lib.Figures;
using ChessGame.Lib.Helpers;

namespace ChessGame.Lib.Board
{
    /// <summary>
    /// Represents default fatory for creating standart chess board with standart start positions of every piece on it
    /// </summary>
    public class DefaultChessBoardFactory : ChessBoardFactory
    {
        /// <summary>
        /// Creates instance of a standert chessboard
        /// </summary>
        /// <returns>Chess board with standart positions of every piece on it</returns>
        public override ChessBoard Create() => new(new Square[,]
        {
            {
                new Square(new Position('a', 1), new Rook(new Position('a', 1), Color.White)),
                new Square(new Position('b', 1), new Knight(new Position('b', 1), Color.White)),
                new Square(new Position('c', 1), new Bishop(new Position('c', 1), Color.White)),
                new Square(new Position('d', 1), new Queen(new Position('d', 1), Color.White)),
                new Square(new Position('e', 1), new King(new Position('e', 1), Color.White)),
                new Square(new Position('f', 1), new Bishop(new Position('f', 1), Color.White)),
                new Square(new Position('g', 1), new Knight(new Position('g', 1), Color.White)),
                new Square(new Position('h', 1), new Rook(new Position('h', 1), Color.White))
            },

            {
                new Square(new Position('a', 2), new Pawn(new Position('a', 2), Color.White)),
                new Square(new Position('b', 2), new Pawn(new Position('b', 2), Color.White)),
                new Square(new Position('c', 2), new Pawn(new Position('c', 2), Color.White)),
                new Square(new Position('d', 2), new Pawn(new Position('d', 2), Color.White)),
                new Square(new Position('e', 2), new Pawn(new Position('e', 2), Color.White)),
                new Square(new Position('f', 2), new Pawn(new Position('f', 2), Color.White)),
                new Square(new Position('g', 2), new Pawn(new Position('g', 2), Color.White)),
                new Square(new Position('h', 2), new Pawn(new Position('h', 2), Color.White)),
            },
            {
                new Square(new Position('a', 3), new EmptyPiece(new Position('a', 3), Color.None)),
                new Square(new Position('b', 3), new EmptyPiece(new Position('b', 3), Color.None)),
                new Square(new Position('c', 3), new EmptyPiece(new Position('c', 3), Color.None)),
                new Square(new Position('d', 3), new EmptyPiece(new Position('d', 3), Color.None)),
                new Square(new Position('e', 3), new EmptyPiece(new Position('e', 3), Color.None)),
                new Square(new Position('f', 3), new EmptyPiece(new Position('f', 3), Color.None)),
                new Square(new Position('g', 3), new EmptyPiece(new Position('g', 3), Color.None)),
                new Square(new Position('h', 3), new EmptyPiece(new Position('h', 3), Color.None)),
            },
            {
                new Square(new Position('a', 4), new EmptyPiece(new Position('a', 4), Color.None)),
                new Square(new Position('b', 4), new EmptyPiece(new Position('b', 4), Color.None)),
                new Square(new Position('c', 4), new EmptyPiece(new Position('c', 4), Color.None)),
                new Square(new Position('d', 4), new EmptyPiece(new Position('d', 4), Color.None)),
                new Square(new Position('e', 4), new EmptyPiece(new Position('e', 4), Color.None)),
                new Square(new Position('f', 4), new EmptyPiece(new Position('f', 4), Color.None)),
                new Square(new Position('g', 4), new EmptyPiece(new Position('g', 4), Color.None)),
                new Square(new Position('h', 4), new EmptyPiece(new Position('h', 4), Color.None)),
            },
            {
                new Square(new Position('a', 5), new EmptyPiece(new Position('a', 5), Color.None)),
                new Square(new Position('b', 5), new EmptyPiece(new Position('b', 5), Color.None)),
                new Square(new Position('c', 5), new EmptyPiece(new Position('c', 5), Color.None)),
                new Square(new Position('d', 5), new EmptyPiece(new Position('d', 5), Color.None)),
                new Square(new Position('e', 5), new EmptyPiece(new Position('e', 5), Color.None)),
                new Square(new Position('f', 5), new EmptyPiece(new Position('f', 5), Color.None)),
                new Square(new Position('g', 5), new EmptyPiece(new Position('g', 5), Color.None)),
                new Square(new Position('h', 5), new EmptyPiece(new Position('h', 5), Color.None)),
            },
            {
                new Square(new Position('a', 6), new EmptyPiece(new Position('a', 6), Color.None)),
                new Square(new Position('b', 6), new EmptyPiece(new Position('b', 6), Color.None)),
                new Square(new Position('c', 6), new EmptyPiece(new Position('c', 6), Color.None)),
                new Square(new Position('d', 6), new EmptyPiece(new Position('d', 6), Color.None)),
                new Square(new Position('e', 6), new EmptyPiece(new Position('e', 6), Color.None)),
                new Square(new Position('f', 6), new EmptyPiece(new Position('f', 6), Color.None)),
                new Square(new Position('g', 6), new EmptyPiece(new Position('g', 6), Color.None)),
                new Square(new Position('h', 6), new EmptyPiece(new Position('h', 6), Color.None)),
            },
            {
                new Square(new Position('a', 7), new Pawn(new Position('a', 7), Color.Black)),
                new Square(new Position('b', 7), new Pawn(new Position('b', 7), Color.Black)),
                new Square(new Position('c', 7), new Pawn(new Position('c', 7), Color.Black)),
                new Square(new Position('d', 7), new Pawn(new Position('d', 7), Color.Black)),
                new Square(new Position('e', 7), new Pawn(new Position('e', 7), Color.Black)),
                new Square(new Position('f', 7), new Pawn(new Position('f', 7), Color.Black)),
                new Square(new Position('g', 7), new Pawn(new Position('g', 7), Color.Black)),
                new Square(new Position('h', 7), new Pawn(new Position('h', 7), Color.Black)),
            },
            {
                new Square(new Position('a', 8), new Rook(new Position('a', 8), Color.Black)),
                new Square(new Position('b', 8), new Knight(new Position('b', 8), Color.Black)),
                new Square(new Position('c', 8), new Bishop(new Position('c', 8), Color.Black)),
                new Square(new Position('d', 8), new Queen(new Position('d', 8), Color.Black)),
                new Square(new Position('e', 8), new King(new Position('e', 8), Color.Black)),
                new Square(new Position('f', 8), new Bishop(new Position('f', 8), Color.Black)),
                new Square(new Position('g', 8), new Knight(new Position('g', 8), Color.Black)),
                new Square(new Position('h', 8), new Rook(new Position('h', 8), Color.Black))
            }
        });
    }
}
