using Xunit;
using System.Collections.Generic;

namespace ChessGame.Lib.Helpers.Tests
{
    public class PositionTests
    {
        [Theory]
        [MemberData(nameof(GetPositionCollection))]
        public void CanBeOnBoardWithPositionTest(Position position, bool expected)
        {
            Assert.Equal(expected, Position.CanBeOnBoard(position));
        }

        [Theory]
        [MemberData(nameof(GetPositionWithToStringCollection))]
        public void ToStringTest(Position position, string expected)
        {
            Assert.Equal(expected, position.ToString());
        }

        [Theory]
        [MemberData(nameof(GetPositionForEqualsTestingCollection))]
        public void EqualsTest(Position first, Position second, bool expected)
        {
            Assert.Equal(expected, first.Equals(second));
        }

        public static IEnumerable<object[]> GetPositionCollection()
        {
            yield return new object[] { new Position('a', 1), true };
            yield return new object[] { Position.Unreachable, false };
            yield return new object[] { new Position('h', 8), true };
            yield return new object[] { new Position('a', 9), false };
            yield return new object[] { new Position('a', 0), false };
            yield return new object[] { new Position('p', 5), false };
            yield return new object[] { new Position('c', -12), false };
        }

        public static IEnumerable<object[]> GetPositionWithToStringCollection()
        {
            yield return new object[] { new Position('a', 1), "a1" };
            yield return new object[] { Position.Unreachable, $"{char.MinValue}{int.MinValue}" };
            yield return new object[] { new Position('h', 8), "h8" };
            yield return new object[] { new Position('a', 9), "a9" };
            yield return new object[] { new Position('a', 0), "a0" };
            yield return new object[] { new Position('p', 5), "p5" };
            yield return new object[] { new Position('c', -12), "c-12" };
        }
        public static IEnumerable<object[]> GetPositionForEqualsTestingCollection()
        {
            yield return new object[] { new Position('a', 1), new Position('a', 1), true };
            yield return new object[] { Position.Unreachable, Position.Unreachable, true };
            yield return new object[] { new Position('h', 8), new Position('h', 9), false };
            yield return new object[] { new Position('a', 9), new Position('b', 9), false };
        }
    }
}