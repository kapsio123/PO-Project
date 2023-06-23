using System;
using Boards;

namespace Pieces{
    public abstract class Piece{
        public int pieceId {get; init;}
        public bool isWhite {get; init;}
        public Tuple<int, int>  pos;
        public bool[,] move_list = new bool[Board.width, Board.height];
        public abstract void possible_moves(Board board);
        public bool isLegal(Tuple<int, int> to){
            return move_list[to.Item1, to.Item2];
        }
        public void move(Tuple<int, int> to, Board board){
            this.pos = to;
        }
    }

    public class King : Piece{
        public King(bool isWhite, Tuple<int, int> pos, Board board)
        {
            this.pieceId = 6;
            this.isWhite = isWhite;
            this.pos = pos;
            this.possible_moves(board);
        }
        public override void possible_moves(Board board)
        {
            bool[,] new_move_list = new bool[Board.width, Board.height];
            foreach(bool b in new_move_list){
                Console.WriteLine(b);
            }

            for(int i = -1; i < 2; i++){
                for(int j = -1; j < 2; j++){
                    if(i == 0 && j == 0) continue;
                    if(this.pos.Item1 + j < 0 || this.pos.Item1 + j > Board.width - 1) continue;
                    if(this.pos.Item2 + i < 0 || this.pos.Item2 + i > Board.height - 1) continue;
                    if(board.board[this.pos.Item1 + j, this.pos.Item2 + i] != null && board.board[this.pos.Item1 + j, this.pos.Item2 + i].isWhite == this.isWhite) continue;

                    new_move_list[this.pos.Item1 + j, this.pos.Item2 + i] = true;
                }
            }
            this.move_list = new_move_list;
        }
    }

    public class Queen : Piece{
        public Queen(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 5;
            this.isWhite = isWhite;
            this.pos = pos;
        }

        public override void possible_moves(Board board)
        {
            throw new NotImplementedException();
        }
    }

    public class Rook : Piece{
        public Rook(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 4;
            this.isWhite = isWhite;
            this.pos = pos;
        }

        public override void possible_moves(Board board)
        {
            bool tr, tl, br, bl;
            tr = true; tl = true; br = true; bl = true;
            bool[,] new_move_list = new bool[Board.width, Board.height];
            for(int i = 1; i < Board.width; i++){
                if(br){
                    if(board.board[this.pos.Item1 + i, this.pos.Item2 + i] != null){
                        if(board.board[this.pos.Item1 + i, this.pos.Item2 + i].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 + i, this.pos.Item2 + i] = true;
                            br = false;
                        }
                        else{
                            br = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1 + i, this.pos.Item2 + i] = true;
                    }
                }
                if(bl){
                    if(board.board[this.pos.Item1 - i, this.pos.Item2 + i] != null){
                        if(board.board[this.pos.Item1 - i, this.pos.Item2 + i].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 - i, this.pos.Item2 + i] = true;
                            bl = false;
                        }
                        else{
                            bl = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1 - i, this.pos.Item2 + i] = true;
                    }
                }
                if(tr){
                    if(board.board[this.pos.Item1 + i, this.pos.Item2 - i] != null){
                        if(board.board[this.pos.Item1 + i, this.pos.Item2 - i].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 + i, this.pos.Item2 - i] = true;
                            tr = false;
                        }
                        else{
                            tr = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1 + i, this.pos.Item2 - i] = true;
                    }
                }
                if(tl){
                    if(board.board[this.pos.Item1 - i, this.pos.Item2 - i] != null){
                        if(board.board[this.pos.Item1 - i, this.pos.Item2 - i].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 - i, this.pos.Item2 - i] = true;
                            tl = false;
                        }
                        else{
                            tl = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1 - i, this.pos.Item2 - i] = true;
                    }
                }
                this.move_list = new_move_list;
            }
        }
    }

    public class Bishop : Piece{
        public Bishop(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 3;
            this.isWhite = isWhite;
            this.pos = pos;
        }

        public override void possible_moves(Board board)
        {
            throw new NotImplementedException();
        }
    }

    public class Knight : Piece{
        public Knight(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 2;
            this.isWhite = isWhite;
            this.pos = pos;
        }

        public override void possible_moves(Board board)
        {
            bool[,] new_move_list = new bool[Board.width, Board.height];

            
        }
    }

    public class Pawn : Piece{
        public Pawn(bool isWhite, Tuple<int, int> pos, Board board){
            this.pieceId = 1;
            this.isWhite = isWhite;
            this.pos = pos;
            this.possible_moves(board);
        }

        public override void possible_moves(Board board)
        {
            bool[,] new_move_list = new bool[Board.width, Board.height];
            int direction;
            if(this.isWhite) direction = -1;
            else direction = 1;

            if(this.pos.Item2 + direction < Board.height || this.pos.Item2 + direction > 0){
                if(board.board[this.pos.Item1, this.pos.Item2 + direction] == null) new_move_list[this.pos.Item1, this.pos.Item2 + direction] = true;
    
                if(this.pos.Item1 + 1 < Board.width && board.board[this.pos.Item1 + 1, this.pos.Item2 + direction] != null){
                    if(board.board[this.pos.Item1 + 1, this.pos.Item2 + direction].isWhite != this.isWhite) new_move_list[this.pos.Item1 + 1, this.pos.Item2 + direction] = true;
                }

                if(this.pos.Item1 - 1 > 0 && board.board[this.pos.Item1 - 1, this.pos.Item2 + direction] != null){
                    if(board.board[this.pos.Item1 - 1, this.pos.Item2 + direction].isWhite != this.isWhite) new_move_list[this.pos.Item1 - 1, this.pos.Item2 + direction] = true;
                }
            }
            this.move_list = new_move_list;
        }
    }
}