using ChessGame.Lib;
using ChessGame.Lib.Board;
using ChessGame.Lib.Figures;
using ChessGame.Lib.GameLogic;
using ChessGame.Lib.Helpers;
using ChessGame.Lib.Players;
using ChessGameTask.Lib.Loggers;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace ChessGameTask.Tests
{
    public class ChessGameSimulationTests
    {
        private static readonly string FILE_NAME = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\LogTests.txt";
        private readonly ChessGameSimulation _game;
        public ChessGameSimulationTests()
        {
            var chessBoardFactory = new DefaultChessBoardFactory();
            var chessBoard = chessBoardFactory.Create();
            var firstPlayer = new Player(Color.White, chessBoard.GetPieces(Color.White), "White player");
            var secondPlayer = new Player(Color.Black, chessBoard.GetPieces(Color.Black), "Black player");
            var logger = new FileLogger<MovementResult>(FILE_NAME);
            _game = new ChessGameSimulation(chessBoard, firstPlayer, secondPlayer, Color.White, logger);
        }

        [Fact]
        public void ChessGameSimulation_PlayerTakesRightPieceTest()
        {
            var whitePiecePawn = _game.GetPiece('a', 2);

            Assert.IsType<Pawn>(whitePiecePawn);
            Assert.True(whitePiecePawn.Color == Color.White && whitePiecePawn.CurrentPosition.FullName == "a2");
        }
        [Fact]
        public void ChessGameSimulation_PlayerTakesEnemyPieceTest()
        {
            var blackPieceKnight = _game.GetPiece('b', 8);

            Assert.IsType<EmptyPiece>(blackPieceKnight);
            Assert.True(blackPieceKnight.Color == Color.None);
        }
        [Fact]
        public void ChessGameSimulation_PlayerTakesEmptyPieceTest()
        {
            var wrongPiece = _game.GetPiece('a', 4);

            Assert.IsType<EmptyPiece>(wrongPiece);
            Assert.True(wrongPiece.Color == Color.None);
        }
        [Fact]
        public void ChessGameSimulation_FirstRightMoveCheckingTest()
        {
            var firstPlayerFirstPiece = _game.GetPiece('a', 2);

            var moveResult = _game.Move(firstPlayerFirstPiece, 'a', 4);
            Assert.True(moveResult == MovementResultType.MovedToEmptySquare);

            Assert.True(_game.CurrentPlayer.ChessPlayingColor == Color.Black);
        }
        [Fact]
        public void ChessGameSimulation_PawnsFirstMovesWithHittngTest()
        {
            var firstPlayerFirstPiece = _game.GetPiece('a', 2);
            _game.Move(firstPlayerFirstPiece, 'a', 4);

            var secondPlayerFirstPiece = _game.GetPiece('b', 7);
            _game.Move(secondPlayerFirstPiece, 'b', 5);

            var firstPlayerPieceToHit = _game.GetPiece('a', 4);
            _game.Move(firstPlayerPieceToHit, 'b', 5);

            Assert.Collection(_game.AnotherPlayer.HittedPieces,
                              piece => Assert.IsType<Pawn>(piece));
        }
        [Fact]
        public void ChessGameSimulation_CheckTest()
        {
            var firstPlayerPe2e4 = _game.GetPiece('e', 2);
            _game.Move(firstPlayerPe2e4, 'e', 4);

            var secondPlayerPd7d5 = _game.GetPiece('d', 7);
            _game.Move(secondPlayerPd7d5, 'd', 5);

            var firstPlayerKe1e2 = _game.GetPiece('e', 1);
            _game.Move(firstPlayerKe1e2, 'e', 2);

            var secondPlayerBc8g4 = _game.GetPiece('c', 8);
            var moveResult = _game.Move(secondPlayerBc8g4, 'g', 4);
            Assert.True(moveResult == MovementResultType.Check);
        }
        [Fact]
        public void ChessGameSimulation_FastCheckmateTest()
        {
            var firstPlayerPe2e4 = _game.GetPiece('e', 2);
            _game.Move(firstPlayerPe2e4, 'e', 4);

            var secondPlayerPd7d5 = _game.GetPiece('g', 7);
            _game.Move(secondPlayerPd7d5, 'g', 5);

            var firstPlayerKe1e2 = _game.GetPiece('d', 2);
            _game.Move(firstPlayerKe1e2, 'd', 4);

            var secondPlayerBc8g4 = _game.GetPiece('f', 7);
            _game.Move(secondPlayerBc8g4, 'f', 6);

            var firstPlayerQd1g5 = _game.GetPiece('d', 1);
            var move = _game.Move(firstPlayerQd1g5, 'h', 5);
            Assert.True(move == MovementResultType.Checkmate);
            Assert.True(_game.Winner.ChessPlayingColor == Color.White);
        }
        [Fact]
        public void ChessGameSimulation_PawnToQueenTest()
        {
            var firstPlayerPh2h4 = _game.GetPiece('h', 2);
            _game.Move(firstPlayerPh2h4, 'h', 4);

            var secondPlayerPg7g5 = _game.GetPiece('g', 7);
            _game.Move(secondPlayerPg7g5, 'g', 5);

            var firstPlayerPh2g5 = _game.GetPiece('h', 4);
            _game.Move(firstPlayerPh2g5, 'g', 5);

            var secondPlayerKg8h6 = _game.GetPiece('g', 8);
            _game.Move(secondPlayerKg8h6, 'h', 6);

            var firstPlayerPg5g6 = _game.GetPiece('g', 5);
            _game.Move(firstPlayerPg5g6, 'g', 6);

            var secondPlayerKh6g4 = _game.GetPiece('h', 6);
            _game.Move(secondPlayerKh6g4, 'g', 4);

            var firstPlayerPg6g7 = _game.GetPiece('g', 6);
            _game.Move(firstPlayerPg6g7, 'g', 7);

            var secondPlayerKg4f2 = _game.GetPiece('g', 4);
            _game.Move(secondPlayerKg4f2, 'f', 2);

            var firstPlayerPg7g8 = _game.GetPiece('g', 7);
            _game.Move(firstPlayerPg7g8, 'g', 8);

            Assert.IsType<Queen>(_game.CurrentChessBoard['g', 8].Piece);
            Assert.Collection(_game.AnotherPlayer.Pieces.Where(p => p is Queen),
                              firstQuenn => Assert.True(firstQuenn.CurrentPosition.FullName == "d1"),
                              secondQueen => Assert.True(secondQueen.CurrentPosition.FullName == "g8"));
        }

        [Fact]
        public void ChessGameSimulation_RealGameSimulationTest()
        {
            var firstPlayerPe2e4 = _game.GetPiece('e', 2);
            _game.Move(firstPlayerPe2e4, 'e', 4);

            var secondPlayerPc7c5 = _game.GetPiece('c', 7);
            _game.Move(secondPlayerPc7c5, 'c', 5);



            var firstPlayerKg1f3 = _game.GetPiece('g', 1);
            _game.Move(firstPlayerKg1f3, 'f', 3);

            var secondPlayerKd7d6 = _game.GetPiece('d', 7);
            _game.Move(secondPlayerKd7d6, 'd', 6);



            var firstPlayerPc2c3 = _game.GetPiece('c', 2);
            _game.Move(firstPlayerPc2c3, 'c', 3);

            var secondPlayerKg8g6 = _game.GetPiece('g', 8);
            _game.Move(secondPlayerKg8g6, 'f', 6);



            var firstPlayerBf1e2 = _game.GetPiece('f', 1);
            _game.Move(firstPlayerBf1e2, 'e', 2);

            var secondPlayerKf6e4 = _game.GetPiece('f', 6);
            _game.Move(secondPlayerKf6e4, 'e', 4);



            var firstPlayerQd1a4 = _game.GetPiece('d', 1);
            var checkmate = _game.Move(firstPlayerQd1a4, 'a', 4);

            Assert.True(checkmate == MovementResultType.Check);
        }
        [Fact]
        public void ChessGameSimulation_RealGameSimulationTest1()
        {
            _game.Move(_game.GetPiece('d', 2), 'd', 4);
            _game.Move(_game.GetPiece('d', 7), 'd', 5);
            _game.Move(_game.GetPiece('c', 1), 'f', 4);
            _game.Move(_game.GetPiece('g', 8), 'f', 6);
            _game.Move(_game.GetPiece('g', 1), 'f', 3);
            _game.Move(_game.GetPiece('c', 7), 'c', 5);
            _game.Move(_game.GetPiece('d', 4), 'c', 5);
            _game.Move(_game.GetPiece('e', 7), 'e', 6);
            _game.Move(_game.GetPiece('e', 2), 'e', 3);
            _game.Move(_game.GetPiece('f', 8), 'c', 5);
            _game.Move(_game.GetPiece('c', 2), 'c', 4);
            _game.Move(_game.GetPiece('e', 8), 'f', 8);
            _game.Move(_game.GetPiece('b', 1), 'c', 3);
            _game.Move(_game.GetPiece('b', 8), 'c', 6);
            _game.Move(_game.GetPiece('h', 2), 'h', 3);
            _game.Move(_game.GetPiece('d', 8), 'a', 5);
            _game.Move(_game.GetPiece('f', 3), 'd', 2);
            _game.Move(_game.GetPiece('c', 5), 'b', 4);
            _game.Move(_game.GetPiece('d', 1), 'c', 2);
            _game.Move(_game.GetPiece('d', 5), 'd', 4);
            _game.Move(_game.GetPiece('c', 3), 'b', 1);
            _game.Move(_game.GetPiece('f', 8), 'g', 8);
            _game.Move(_game.GetPiece('e', 3), 'e', 4);
            _game.Move(_game.GetPiece('g', 7), 'g', 5);
            _game.Move(_game.GetPiece('f', 4), 'h', 2);
            _game.Move(_game.GetPiece('d', 4), 'd', 3);
            _game.Move(_game.GetPiece('c', 2), 'c', 1);
            _game.Move(_game.GetPiece('f', 6), 'e', 4);
            _game.Move(_game.GetPiece('f', 2), 'f', 3);
            _game.Move(_game.GetPiece('e', 4), 'd', 2);
            _game.Move(_game.GetPiece('b', 1), 'd', 2);
            _game.Move(_game.GetPiece('c', 6), 'd', 4);
            _game.Move(_game.GetPiece('e', 1), 'd', 1);
            _game.Move(_game.GetPiece('d', 4), 'c', 2);
            _game.Move(_game.GetPiece('d', 2), 'b', 3);
            _game.Move(_game.GetPiece('a', 5), 'f', 5);
            _game.Move(_game.GetPiece('a', 1), 'b', 1);
            _game.Move(_game.GetPiece('c', 8), 'd', 7);
            _game.Move(_game.GetPiece('h', 3), 'h', 4);
            _game.Move(_game.GetPiece('d', 7), 'c', 6);
            _game.Move(_game.GetPiece('b', 3), 'd', 2);
            _game.Move(_game.GetPiece('g', 5), 'g', 4);
            _game.Move(_game.GetPiece('a', 2), 'a', 3);
            _game.Move(_game.GetPiece('g', 4), 'f', 3);
            _game.Move(_game.GetPiece('g', 2), 'g', 4);
            _game.Move(_game.GetPiece('f', 3), 'f', 2);
            _game.Move(_game.GetPiece('h', 4), 'h', 5);
            var check = _game.Move(_game.GetPiece('f', 5), 'f', 3);
            Assert.True(check == MovementResultType.Check);
            _game.Move(_game.GetPiece('d', 2), 'f', 3);
            var check1 = _game.Move(_game.GetPiece('c', 6), 'f', 3);
            Assert.True(check1 == MovementResultType.Check);
            _game.Move(_game.GetPiece('f', 1), 'e', 2);

            _game.SetStandoff();
            Assert.False(_game.CanContinue());
        }
    }
}
