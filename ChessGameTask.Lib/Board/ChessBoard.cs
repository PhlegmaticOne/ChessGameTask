using ChessGame.Lib.Figures;
using ChessGame.Lib.Helpers;
using ChessGameTask.Lib.Helpers;
using System;
using System.Collections.Generic;

namespace ChessGame.Lib.Board
{
    /// <summary>
    /// Represents instance of chess board with pieces on it
    /// </summary>
    public sealed class ChessBoard : IClonable<ChessBoard>
    {
        /// <summary>
        /// Represents squares on this chess board
        /// </summary>
        private readonly Square[,] _squares;
        /// <summary>
        /// Initializes new chess board instance with squares
        /// </summary>
        /// <param name="squares"></param>
        internal ChessBoard(Square[,] squares) => _squares = squares;
        /// <summary>
        /// Indexator to get square by its horizontal and vertical parameters
        /// </summary>
        /// <param name="horizontal">Horizontal parameter</param>
        /// <param name="vertical">Vertical parameter</param>
        public Square this[char horizontal, int vertical]
        {
            get
            {
                if(Position.CanBeOnBoard(horizontal, vertical))
                {
                    return _squares[vertical - 1, horizontal - 97];
                }
                throw new ArgumentOutOfRangeException("Row or column", string.Format("{0}{1}", horizontal, vertical), "Index out of board size");
            }
            set
            {
                if (Position.CanBeOnBoard(horizontal, vertical)) _squares[horizontal, vertical] = value;
            }
        }
        /// <summary>
        /// Swaps two pieces on board
        /// </summary>
        /// <param name="first">First piece to swap</param>
        /// <param name="second">Second piece to swap</param>
        public void SwapPieces(Piece first, Piece second)
        {
            var secondPosition = second.CurrentPosition;
            second.SetPosition(first.CurrentPosition);
            this[first.CurrentPosition.Horizontal, first.CurrentPosition.Vertical].Piece = second;
            first.SetPosition(secondPosition);
            this[secondPosition.Horizontal, secondPosition.Vertical].Piece = first;
        }
        /// <summary>
        /// Checks if square of this board contains piece that can participate in game
        /// </summary>
        /// <param name="square">Square to check piece on it</param>
        public bool ContainsRealPiece(Square square) => !ContainsEmptyPiece(square);
        /// <summary>
        /// Checks if square of this board is empty
        /// </summary>
        /// <param name="square">Square to check</param>
        public bool ContainsEmptyPiece(Square square) => square.Piece.GetType() == typeof(EmptyPiece);
        /// <summary>
        /// Checks if square of this board on this position contains piece that can participate in game
        /// </summary>
        /// <param name="position">Position of square on board</param>
        public bool ContainsRealPiece(Position position) => !ContainsEmptyPiece(position);
        /// <summary>
        /// Checks if square on this position of this board is empty
        /// </summary>
        /// <param name="position">Position of square on board</param>
        public bool ContainsEmptyPiece(Position position) => this[position.Horizontal, position.Vertical].Piece.GetType() == typeof(EmptyPiece);
        /// <summary>
        /// Checks if square of this board on this position representing by horizontal and vertical parameters contains piece that can participate in game
        /// </summary>
        /// <param name="horizontal">Horizontal parameter</param>
        /// <param name="vertical">Vertical parameter</param>
        public bool ContainsRealPiece(char horizontal, int vertical) => !ContainsEmptyPiece(new Position(horizontal, vertical));
        /// <summary>
        /// Checks if square on this position representing by horizontal and vertical parameters of this board is empty
        /// </summary>
        /// <param name="horizontal">Horizontal parameter</param>
        /// <param name="vertical">Vertical parameter</param>
        public bool ContainsEmptyPiece(char horizontal, int vertical) => ContainsEmptyPiece(new Position(horizontal, vertical));
        /// <summary>
        /// Retrieves all the pieces from the board which color is equal to input color
        /// </summary>
        /// <param name="color">Color that returning pieces must have</param>
        /// <returns>List of pieces of specified color</returns>
        public List<Piece> GetPieces(Color color) => _squares.GetPieces(square => square.Piece.Color == color);
        /// <summary>
        /// Calculates hash code of chess board
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i < _squares.GetLength(0); i++)
            {
                for (int j = 0; j < _squares.GetLength(1); j++)
                {
                    unchecked { hash += _squares[i, j].GetHashCode(); };
                }
            }
            return hash;
        }
        /// <summary>
        /// Determines whether the object is equal to this chess board
        /// </summary>
        /// <param name="obj">Object to check th equality</param>
        public override bool Equals(object obj)
        {
            if (obj is ChessBoard chessBoard) {
                for (int i = 0; i < _squares.GetLength(0); i++)
                {
                    for (int j = 0; j < _squares.GetLength(1); j++)
                    {
                        if (_squares[i, j] != chessBoard._squares[i, j])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Converts chess board to string
        /// </summary>
        /// <returns>String representation of chess board</returns>
        public override string ToString() => _squares.Length.ToString();
        /// <summary>
        /// Clones instance of chess board into a new chess board instance which is equal to current
        /// </summary>
        public ChessBoard Clone()
        {
            var carcass = new Square[8, 8];
            for (int i = 0; i < _squares.GetLength(0); i++)
            {
                for (int j = 0; j < _squares.GetLength(1); j++)
                {
                    carcass[i, j] = new Square(_squares[i, j].BoardPosition, _squares[i, j].Piece.Clone());
                }
            }
            return new ChessBoard(carcass);
        }
    }
}
