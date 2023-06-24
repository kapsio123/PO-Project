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
        public virtual void move(Tuple<int, int> to, Board board){
            this.pos = to;
        }
    }

    public class King : Piece{
        bool has_moved;
        public King(bool isWhite, Tuple<int, int> pos)
        {
            this.pieceId = 6;
            this.isWhite = isWhite;
            this.pos = pos;
            this.has_moved = false;
        }
        public override void possible_moves(Board board)
        {
            bool[,] new_move_list = new bool[Board.width, Board.height];

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
        public override void move(Tuple<int, int> to, Board board){
            this.pos = to;
            this.has_moved = true;
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
            bool tr, tl, br, bl;
            tr = true; tl = true; br = true; bl = true;
            bool[,] new_move_list = new bool[Board.width, Board.height];
            for(int i = 1; i < Board.width; i++){
                if(br && this.pos.Item1 + i > 0 && this.pos.Item1 + i < Board.width && this.pos.Item2 + i > 0 && this.pos.Item2 + i < Board.height){
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
                if(bl && this.pos.Item1 - i > 0 && this.pos.Item1 - i < Board.width && this.pos.Item2 + i > 0 && this.pos.Item2 + i < Board.height){
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
                if(tr && this.pos.Item1 + i > 0 && this.pos.Item1 + i < Board.width && this.pos.Item2 - i > 0 && this.pos.Item2 - i < Board.height){
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
                if(tl && this.pos.Item1 - i > 0 && this.pos.Item1 - i < Board.width && this.pos.Item2 - i > 0 && this.pos.Item2 - i < Board.height){
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
            bool t, b, r, l;
            t = true; b = true; r = true; l = true;
            for(int i = 1; i < Board.width; i++){
                if(b && this.pos.Item2 + i > 0 && this.pos.Item2 + i < Board.height){
                    if(board.board[this.pos.Item1, this.pos.Item2 + i] != null){
                        if(board.board[this.pos.Item1, this.pos.Item2 + i].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1, this.pos.Item2 + i] = true;
                            b = false;
                        }
                        else{
                            b = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1, this.pos.Item2 + i] = true;
                    }
                }
                if(l && this.pos.Item1 - i > 0 && this.pos.Item1 + i < Board.width){
                    if(board.board[this.pos.Item1 - i, this.pos.Item2] != null){
                        if(board.board[this.pos.Item1 - i, this.pos.Item2].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 - i, this.pos.Item2] = true;
                            l = false;
                        }
                        else{
                            l = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1 - i, this.pos.Item2] = true;
                    }
                }
                if(r && this.pos.Item1 + i > 0 && this.pos.Item1 + i < Board.width){
                    if(board.board[this.pos.Item1 + i, this.pos.Item2] != null){
                        if(board.board[this.pos.Item1 + i, this.pos.Item2].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 + i, this.pos.Item2] = true;
                            r = false;
                        }
                        else{
                            r = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1 + i, this.pos.Item2] = true;
                    }
                }
                if(t && this.pos.Item2 - i > 0 && this.pos.Item2 - i < Board.height){
                    if(board.board[this.pos.Item1, this.pos.Item2 - i] != null){
                        if(board.board[this.pos.Item1, this.pos.Item2 - i].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1, this.pos.Item2 - i] = true;
                            t = false;
                        }
                        else{
                            t = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1, this.pos.Item2 - i] = true;
                    }
                }
                this.move_list = new_move_list;
            }
        }
    }

    public class Rook : Piece{
        bool has_moved;
        public Rook(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 4;
            this.isWhite = isWhite;
            this.pos = pos;
            this.has_moved = false;
        }

        public override void possible_moves(Board board)
        {
            bool t, b, r, l;
            t = true; b = true; r = true; l = true;
            bool[,] new_move_list = new bool[Board.width, Board.height];
            for(int i = 1; i < Board.width; i++){
                if(b && this.pos.Item2 + i > 0 && this.pos.Item2 + i < Board.height){
                    if(board.board[this.pos.Item1, this.pos.Item2 + i] != null){
                        if(board.board[this.pos.Item1, this.pos.Item2 + i].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1, this.pos.Item2 + i] = true;
                            b = false;
                        }
                        else{
                            b = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1, this.pos.Item2 + i] = true;
                    }
                }
                if(l && this.pos.Item1 - i >= 0 && this.pos.Item1 - i < Board.width){
                    if(board.board[this.pos.Item1 - i, this.pos.Item2] != null){
                        if(board.board[this.pos.Item1 - i, this.pos.Item2].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 - i, this.pos.Item2] = true;
                            l = false;
                        }
                        else{
                            l = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1 - i, this.pos.Item2] = true;
                    }
                }
                if(r && this.pos.Item1 + i >= 0 && this.pos.Item1 + i < Board.width){
                    if(board.board[this.pos.Item1 + i, this.pos.Item2] != null){
                        if(board.board[this.pos.Item1 + i, this.pos.Item2].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 + i, this.pos.Item2] = true;
                            r = false;
                        }
                        else{
                            r = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1 + i, this.pos.Item2] = true;
                    }
                }
                if(t && this.pos.Item2 - i >= 0 && this.pos.Item2 - i < Board.height){
                    if(board.board[this.pos.Item1, this.pos.Item2 - i] != null){
                        if(board.board[this.pos.Item1, this.pos.Item2 - i].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1, this.pos.Item2 - i] = true;
                            t = false;
                        }
                        else{
                            t = false;
                        }
                    }
                    else{
                        new_move_list[this.pos.Item1, this.pos.Item2 - i] = true;
                    }
                }
                this.move_list = new_move_list;
            }
        }
        public override void move(Tuple<int, int> to, Board board){
            this.pos = to;
            this.has_moved = true;
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
            bool tr, tl, br, bl;
            tr = true; tl = true; br = true; bl = true;
            bool[,] new_move_list = new bool[Board.width, Board.height];
            for(int i = 1; i < Board.width; i++){
                if(br && this.pos.Item1 + i >= 0 && this.pos.Item1 + i < Board.width && this.pos.Item2 + i >= 0 && this.pos.Item2 + i < Board.width){
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
                if(bl && this.pos.Item1 - i >= 0 && this.pos.Item1 - i < Board.width && this.pos.Item2 + i >= 0 && this.pos.Item2 + i < Board.width){
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
                if(tr && this.pos.Item1 + i >= 0 && this.pos.Item1 + i < Board.width && this.pos.Item2 - i >= 0 && this.pos.Item2 - i < Board.width){
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
                if(tl && this.pos.Item1 - i >= 0 && this.pos.Item1 - i < Board.width && this.pos.Item2 - i >= 0 && this.pos.Item2 - i < Board.width){
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

    public class Knight : Piece{
        public Knight(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 2;
            this.isWhite = isWhite;
            this.pos = pos;
        }

        public override void possible_moves(Board board)
        {
            bool[,] new_move_list = new bool[Board.width, Board.height];
            for(int i = -1; i <= 1; i += 2){
                if(this.pos.Item1 + i >= 0 && this.pos.Item1 + i < Board.width && this.pos.Item2 + 2 < Board.height){
                    if(board.board[this.pos.Item1 + i, this.pos.Item2 + 2] == null){
                        new_move_list[this.pos.Item1 + i, this.pos.Item2 + 2] = true;
                    }
                    else{
                        if(board.board[this.pos.Item1 + i, this.pos.Item2 + 2].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 + i, this.pos.Item2 + 2] = true;
                        }
                    }
                }
                if(this.pos.Item1 + i >= 0 && this.pos.Item1 + i < Board.width && this.pos.Item2 - 2 >= 0){
                    if(board.board[this.pos.Item1 + i, this.pos.Item2 - 2] == null){
                        new_move_list[this.pos.Item1 + i, this.pos.Item2 - 2] = true;
                    }
                    else{
                        if(board.board[this.pos.Item1 + i, this.pos.Item2 - 2].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 + i, this.pos.Item2 - 2] = true;
                        }
                    }
                }

                if(this.pos.Item1 + 2 < Board.width && this.pos.Item2 + i >= 0 && this.pos.Item2 + i < Board.height){
                    if(board.board[this.pos.Item1 + 2, this.pos.Item2 + i] == null){
                        new_move_list[this.pos.Item1 + 2, this.pos.Item2 + i] = true;
                    }
                    else{
                        if(board.board[this.pos.Item1 + 2, this.pos.Item2 + i].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 + 2, this.pos.Item2 + i] = true;
                        }
                    }
                }

                if(this.pos.Item1 - 2 >= 0 && this.pos.Item2 + i >= 0 && this.pos.Item2 + i < Board.height){
                    if(board.board[this.pos.Item1 - 2, this.pos.Item2 + i] == null){
                        new_move_list[this.pos.Item1 - 2, this.pos.Item2 + i] = true;
                    }
                    else{
                        if(board.board[this.pos.Item1 - 2, this.pos.Item2 + i].isWhite != this.isWhite){
                            new_move_list[this.pos.Item1 - 2, this.pos.Item2 + i] = true;
                        }
                    }
                }
            }
            this.move_list = new_move_list;
        }
    }

    public class Pawn : Piece{
        bool has_moved;
        int direction;
        protected bool en_passant;
        protected bool en_passant_l;
        protected bool en_passant_r;
        public Pawn(bool isWhite, Tuple<int, int> pos){
            this.pieceId = 1;
            this.isWhite = isWhite;
            this.pos = pos;
            this.has_moved = false;
            en_passant = false;
            en_passant_l = false;
            en_passant_r = false;
            if(this.isWhite) direction = -1;
            else direction = 1;
        }
        public override void possible_moves(Board board)
        {
            bool[,] new_move_list = new bool[Board.width, Board.height];

            if(!en_passant){
                en_passant_l = false;
                en_passant_r = false;
            }

            if(this.pos.Item2 + direction < Board.height || this.pos.Item2 + direction > 0){
                if(board.board[this.pos.Item1, this.pos.Item2 + direction] == null){
                    new_move_list[this.pos.Item1, this.pos.Item2 + direction] = true;
                    if(!has_moved && board.board[this.pos.Item1, this.pos.Item2 + direction * 2] == null) new_move_list[this.pos.Item1, this.pos.Item2 + direction * 2] = true;
                }
    
                if(this.pos.Item1 + 1 < Board.width){
                    if(board.board[this.pos.Item1 + 1, this.pos.Item2 + direction] != null) 
                    {
                        if(board.board[this.pos.Item1 + 1, this.pos.Item2 + direction].isWhite != this.isWhite) new_move_list[this.pos.Item1 + 1, this.pos.Item2 + direction] = true;
                    }
                    else if(en_passant_r){
                        new_move_list[this.pos.Item1 + 1, this.pos.Item2 + direction] = true;
                    }
                }

                if(this.pos.Item1 - 1 >= 0){
                    if(board.board[this.pos.Item1 - 1, this.pos.Item2 + direction] != null) 
                    {
                        if(board.board[this.pos.Item1 - 1, this.pos.Item2 + direction].isWhite != this.isWhite) new_move_list[this.pos.Item1 - 1, this.pos.Item2 + direction] = true;
                    }
                    else if(en_passant_l){
                        new_move_list[this.pos.Item1 - 1, this.pos.Item2 + direction] = true;
                    }
                }
            }
            en_passant = false;
            this.move_list = new_move_list;
        }
         public override void move(Tuple<int, int> to, Board board){
            if(!has_moved && to.Item2 == this.pos.Item2 + direction * 2){
                if(board.board[to.Item1 + 1, to.Item2] != null && board.board[to.Item1 + 1, to.Item2].isWhite != this.isWhite && board.board[to.Item1 + 1, to.Item2] is Pawn){
                    Pawn temp = (Pawn)board.board[to.Item1 + 1, to.Item2];
                    temp.en_passant_l = true;
                    temp.en_passant = true;
                    board.board[to.Item1 + 1, to.Item2] = temp;
                }
                if(board.board[to.Item1 - 1, to.Item2] != null && board.board[to.Item1 - 1, to.Item2].isWhite != this.isWhite && board.board[to.Item1 - 1, to.Item2] is Pawn){
                    Pawn temp = (Pawn)board.board[to.Item1 - 1, to.Item2];
                    temp.en_passant_r = true;
                    temp.en_passant = true;
                    board.board[to.Item1 - 1, to.Item2] = temp;
                }
            }
            this.has_moved = true;
            if(to.Item1 > this.pos.Item1 && en_passant_r) board.board[to.Item1, to.Item2 - direction] = null;
            if(to.Item1 < this.pos.Item1 && en_passant_l) board.board[to.Item1, to.Item2 - direction] = null;
    
            this.pos = to;
        }
    }
}