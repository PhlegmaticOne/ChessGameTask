using Xunit;
using ChessGame.Lib.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Lib.Helpers;

namespace ChessGame.Lib.Figures.Tests
{
    public class PieceTests
    {
        [Fact()]
        public void SetPositionTest()
        {
            var piece = new Pawn(new Position('a', 1), Color.White);
            piece.SetPosition(new Position('a', 2));
            Assert.True(piece.CurrentPosition.FullName == "a2");
        }

        [Fact()]
        public void ToStringTest()
        {
            var piece = new Pawn(new Position('a', 1), Color.White);
            Assert.True(piece.ToString() == "Pawn: a1");
        }

        [Fact()]
        public void EqualsTest()
        {
            var piece1 = new Pawn(new Position('a', 2), Color.White);
            var piece2 = new Pawn(new Position('a', 2), Color.White);
            Assert.True(piece1.Equals(piece2));
            Assert.True(piece2.Equals(piece1));
        }

        [Fact()]
        public void CloneTest()
        {
            var pawn = new Pawn(new Position('a', 2), Color.White);
            var newPawn = pawn.Clone();
            Assert.IsType<Pawn>(newPawn);
            Assert.True(newPawn.Equals(pawn));
            Assert.True(pawn.Equals(newPawn));
        }
    }
}