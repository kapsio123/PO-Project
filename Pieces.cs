using System;

namespace Pieces{
    public abstract class Piece{
        public int pieceId {get; init;}
        public bool isWhite {get; init;}
        public Tuple<int, int>  pos;
    }

    public class King : Piece{
        public King(bool isWhite, Tuple<int, int> pos)
        {
            this.pieceId = 6;
            this.isWhite = isWhite;
            this.pos = pos;
        }
    }

    public class Queen : Piece{
        public Queen(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 5;
            this.isWhite = isWhite;
            this.pos = pos;
        }
    }

    public class Rook : Piece{
        public Rook(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 4;
            this.isWhite = isWhite;
            this.pos = pos;
        }
    }

    public class Bishop : Piece{
        public Bishop(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 3;
            this.isWhite = isWhite;
            this.pos = pos;
        }
    }

    public class Knight : Piece{
        public Knight(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 2;
            this.isWhite = isWhite;
            this.pos = pos;
        }
    }

    public class Pawn : Piece{
        public Pawn(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 1;
            this.isWhite = isWhite;
            this.pos = pos;
        }
    }
}