using Xunit;
using System.Linq;
using ChessGame.Lib.Figures;
using ChessGame.Lib.Helpers;

namespace ChessGame.Lib.Board.Tests
{
    public class ChessBoardTests
    {
        private readonly ChessBoard _chessBoard;
        public ChessBoardTests()
        {
            var chessBoardFactory = new DefaultChessBoardFactory();
            _chessBoard = chessBoardFactory.Create();
        }

        [Fact()]
        public void SwapPiecesTest()
        {
            _chessBoard.SwapPieces(_chessBoard['a', 1].Piece, _chessBoard['h', 7].Piece);

            Assert.IsType<Rook>(_chessBoard['h', 7].Piece);
            Assert.True(_chessBoard['h', 7].Piece.Color == Color.White);
            Assert.Equal("h7", _chessBoard['h', 7].Piece.CurrentPosition.FullName);

            Assert.IsType<Pawn>(_chessBoard['a', 1].Piece);
            Assert.True(_chessBoard['a', 1].Piece.Color == Color.Black);
            Assert.Equal("a1", _chessBoard['a', 1].Piece.CurrentPosition.FullName);

            //Swap back
            _chessBoard.SwapPieces(_chessBoard['a', 1].Piece, _chessBoard['h', 7].Piece);

            Assert.IsType<Pawn>(_chessBoard['h', 7].Piece);
            Assert.True(_chessBoard['h', 7].Piece.Color == Color.Black);
            Assert.Equal("h7", _chessBoard['h', 7].Piece.CurrentPosition.FullName);

            Assert.IsType<Rook>(_chessBoard['a', 1].Piece);
            Assert.True(_chessBoard['a', 1].Piece.Color == Color.White);
            Assert.Equal("a1", _chessBoard['a', 1].Piece.CurrentPosition.FullName);
        }

        [Fact()]
        public void ContainsRealPieceTest()
        {
            Assert.True(_chessBoard.ContainsRealPiece(new Position('a', 1)));
            Assert.True(_chessBoard.ContainsRealPiece(_chessBoard['h', 7]));
            Assert.True(_chessBoard.ContainsRealPiece('b', 2));
        }

        [Fact()]
        public void ContainsEmptyPieceTest()
        {
            Assert.True(_chessBoard.ContainsEmptyPiece(new Position('a', 3)));
            Assert.True(_chessBoard.ContainsEmptyPiece(_chessBoard['d', 6]));
            Assert.True(_chessBoard.ContainsEmptyPiece('h', 4));
        }

        [Fact()]
        public void GetWhitePiecesTest()
        {
            var whitePieces = _chessBoard.GetPieces(Color.White);
            Assert.True(whitePieces.Count == 16);
            Assert.True(whitePieces.Where(p => p is Pawn).Count() == 8);
            Assert.True(whitePieces.Where(p => p is Bishop).Count() == 2);
            Assert.True(whitePieces.Where(p => p is Knight).Count() == 2);
            Assert.True(whitePieces.Where(p => p is Rook).Count() == 2);
            Assert.True(whitePieces.Where(p => p is Queen).Count() == 1);
            Assert.True(whitePieces.Where(p => p is King).Count() == 1);
            Assert.True(!whitePieces.Where(p => p is EmptyPiece).Any());
        }
        [Fact()]
        public void GetBlackPiecesTest()
        {
            var blackPieces = _chessBoard.GetPieces(Color.Black);
            Assert.True(blackPieces.Count == 16);
            Assert.True(blackPieces.Where(p => p is Pawn).Count() == 8);
            Assert.True(blackPieces.Where(p => p is Bishop).Count() == 2);
            Assert.True(blackPieces.Where(p => p is Knight).Count() == 2);
            Assert.True(blackPieces.Where(p => p is Rook).Count() == 2);
            Assert.True(blackPieces.Where(p => p is Queen).Count() == 1);
            Assert.True(blackPieces.Where(p => p is King).Count() == 1);
            Assert.True(!blackPieces.Where(p => p is EmptyPiece).Any());
        }
        [Fact]
        public void CloneTest()
        {
            var clone = _chessBoard.Clone();
            Assert.True(clone.Equals(_chessBoard));
            Assert.True(_chessBoard.Equals(clone));
        }
    }
}