using Xunit;
using ChessGame.Lib.Helpers;
using ChessGame.Lib.Figures;

namespace ChessGame.Lib.Board.Tests
{
    public class SquareTests
    {
        [Fact()]
        public void EqualsTest()
        {
            var firstSquare = new Square(new Position('a', 1), new Pawn(new Position('a', 1), Color.White));
            var secondSquare = new Square(new Position('a', 1), new Pawn(new Position('a', 1), Color.White));
            Assert.True(firstSquare.Equals(secondSquare));
            Assert.True(firstSquare == secondSquare);
        }

        [Fact()]
        public void NotEqualsTest()
        {
            var firstSquare = new Square(new Position('b', 3), new Pawn(new Position('b', 3), Color.White));
            var secondSquare = new Square(new Position('a', 1), new Pawn(new Position('a', 1), Color.White));
            Assert.False(firstSquare.Equals(secondSquare));
            Assert.False(firstSquare == secondSquare);
            Assert.False(firstSquare.Equals(new object()));
        }

        [Fact()]
        public void ToStringTest()
        {
            var square = new Square(new Position('h', 8), new Pawn(new Position('h', 8), Color.White));
            Assert.Equal("Pawn: h8", square.ToString());
        }
    }
}