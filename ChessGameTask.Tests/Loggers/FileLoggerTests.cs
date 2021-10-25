using Xunit;
using ChessGameTask.Lib.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Lib.Board;
using ChessGame.Lib.GameLogic;
using System.IO;
using System.Text.RegularExpressions;

namespace ChessGameTask.Lib.Loggers.Tests
{
    public class FileLoggerTests
    {
        [Fact]
        public void LogTest()
        {
            //Arrange
            var chessBoard = new DefaultChessBoardFactory().Create();
            var filePath = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\LogTests.txt";
            var logger = new FileLogger<MovementResult>(filePath);
            var moves = new List<MovementResult>()
            {
                new Movement(chessBoard['a', 2].Piece, chessBoard['a', 4], chessBoard).Move(),
                new Movement(chessBoard['a', 1].Piece, chessBoard['a', 3], chessBoard).Move(),
                new Movement(chessBoard['b', 1].Piece, chessBoard['c', 3], chessBoard).Move(),
                new Movement(chessBoard['h', 2].Piece, chessBoard['h', 4], chessBoard).Move(),
            };

            //Act
            foreach (var move in moves)
            {
                logger.Log(move);
            }

            //Assert
            var allText = new List<string>();
            using (var stream = File.OpenText(filePath))
            {
                allText.AddRange(stream.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries));
            }
            Assert.Collection(allText,
                              first => Assert.True(first.Contains("Pawn") && first.Contains("a4") && first.Contains("MovedToEmptySquare")),
                              second => Assert.True(second.Contains("Rook") && second.Contains("a3") && second.Contains("MovedToEmptySquare")),
                              third => Assert.True(third.Contains("Knight") && third.Contains("c3") && third.Contains("MovedToEmptySquare")),
                              fourth => Assert.True(fourth.Contains("Pawn") && fourth.Contains("h4") && fourth.Contains("MovedToEmptySquare"))
                              );
        }
    }
}