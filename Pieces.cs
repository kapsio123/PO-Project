namespace Pieces{
    public abstract class Piece{
        protected int pieceId {get; init;}
    }

    public class King : Piece{
        public King(){
            this.pieceId = 6;
        }
    }

    public class Queen : Piece{
        public Queen(){
            this.pieceId = 5;
        }
    }

    public class Rook : Piece{
        public Rook(){
            this.pieceId = 4;
        }
    }

    public class Bishop : Piece{
        public Bishop(){
            this.pieceId = 3;
        }
    }

    public class Knight : Piece{
        public Knight(){
            this.pieceId = 2;
        }
    }

    public class Pawn : Piece{
        public Pawn(){
            this.pieceId = 1;
        }
    }
}