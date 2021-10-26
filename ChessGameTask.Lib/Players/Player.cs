using ChessGame.Lib.Figures;
using ChessGame.Lib.Helpers;
using System.Collections.Generic;

namespace ChessGame.Lib.Players
{
    /// <summary>
    /// Represent player i a chess game
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Initializes new player instance with specified chess color, pieces of that color and player's name
        /// </summary>
        public Player(Color chessPlayingColor, List<Piece> pieces, string name) => (ChessPlayingColor, Pieces, Name) =
                                                                                   (chessPlayingColor, pieces, name);
        /// <summary>
        /// Color in the chess game
        /// </summary>
        public Color ChessPlayingColor { get; set; }
        /// <summary>
        /// List of pieces that belong to a player
        /// </summary>
        public List<Piece> Pieces { get; set; }
        /// <summary>
        /// Player's name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// List of pieces that player has hitted
        /// </summary>
        public List<Piece> HittedPieces { get; set; } = new();
        /// <summary>
        /// Counter for moves that didn't lead to hit any piece
        /// </summary>
        public int EmptyMovesCount { get; set; }
    }
}
