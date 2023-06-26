using Pieces;
using System;

namespace Boards;
public class Board{
    public static int height = 8;
    public static int width = 8;
    public King wKing {get; private set;}
    public King bKing {get; private set;}
    public Piece[,] board {get; private set;}
    public Piece[,] prev {get; private set;}
    public Board(){
        this.board = new Piece[height, width];
        this.prev =  new Piece[height, width];
    }
    public bool promotion {get; set;}
    public Pawn toPromote {get; set;}
    public void Initialize(){
        board[0, 0] = new Rook(false, new Tuple<int, int>(0, 0));
        board[1, 0] = new Knight(false, new Tuple<int, int>(1, 0));
        board[2, 0] = new Bishop(false, new Tuple<int, int>(2, 0));
        board[3, 0] = new Queen(false, new Tuple<int, int>(3, 0));
        board[4, 0] = new King(false, new Tuple<int, int>(4, 0));
        bKing = (King)board[4, 0];
        board[5, 0] = new Bishop(false, new Tuple<int, int>(5, 0));
        board[6, 0] = new Knight(false, new Tuple<int, int>(6, 0));
        board[7, 0] = new Rook(false, new Tuple<int, int>(7, 0));
        for(int i = 0; i < width; i++) board[i, 1] = new Pawn(false, new Tuple<int, int>(i, 1));

        board[0, 7] = new Rook(true, new Tuple<int, int>(0, 7));
        board[1, 7] = new Knight(true, new Tuple<int, int>(1, 7));
        board[2, 7] = new Bishop(true, new Tuple<int, int>(2, 7));
        board[3, 7] = new Queen(true, new Tuple<int, int>(3, 7));
        board[4, 7] = new King(true, new Tuple<int, int>(4, 7));
        wKing = (King)board[4,7];
        board[5, 7] = new Bishop(true, new Tuple<int, int>(5, 7));
        board[6, 7] = new Knight(true, new Tuple<int, int>(6, 7));
        board[7, 7] = new Rook(true, new Tuple<int, int>(7, 7));
        for(int i = 0; i < width; i++) board[i, 6] = new Pawn(true, new Tuple<int, int>(i, 6));

        promotion = false;
        copy(board, prev);

        this.update();
    }

    public bool move(Tuple<int, int> from, Tuple<int, int> to, bool White_turn){
        if(board[from.Item1, from.Item2] == null) return false;
        if(board[from.Item1, from.Item2].isWhite != White_turn) return false;
        if(!board[from.Item1, from.Item2].isLegal(to)) return false;

        copy(board, prev);
        King wKing_temp = new King((King)board[wKing.pos.Item1, wKing.pos.Item2]);
        King bKing_temp = new King((King)board[bKing.pos.Item1, bKing.pos.Item2]);

        board[from.Item1, from.Item2].move(to, this);

        board[to.Item1, to.Item2] = board[from.Item1, from.Item2];
        board[from.Item1, from.Item2] = null;

        this.update();

        if(wKing.inCheck() && White_turn){
            copy(prev, board);
            wKing = (King)board[wKing_temp.pos.Item1, wKing_temp.pos.Item2];
            bKing = (King)board[bKing_temp.pos.Item1, bKing_temp.pos.Item2];
            foreach(Piece p in wKing.attacked_by){
                if(p == null) continue;
                Console.WriteLine(p.pos);
            }
            return false;
        }
        else if(bKing.inCheck() && !White_turn){
            copy(prev, board);
            wKing = (King)board[wKing_temp.pos.Item1, wKing_temp.pos.Item2];
            bKing = (King)board[bKing_temp.pos.Item1, bKing_temp.pos.Item2];
            foreach(Piece p in bKing.attacked_by){
                if(p == null) continue;
                Console.WriteLine(p.pos);
            }
            return false;
        }
        return true;
    }
    void update(){
        wKing.number_of_attackers = 0;
        bKing.number_of_attackers = 0;
        foreach(Piece p in board){
            if(p == null) continue;
            p.possible_moves(this);
            if(p.move_list[wKing.pos.Item1, wKing.pos.Item2] == true){
                wKing.attacked_by[wKing.number_of_attackers] = p;
                wKing.number_of_attackers += 1;
            }
            if(p.move_list[bKing.pos.Item1, bKing.pos.Item2] == true){
                bKing.attacked_by[bKing.number_of_attackers] = p;
                bKing.number_of_attackers += 1;
            }
        }
    }
    public void castle_long(bool isWhite){
        if(isWhite){
            board[0, 7].move(new Tuple<int, int>(3, 7), this);
            board[3, 7] = board[0, 7];
            board[0, 7] = null;
        }
        else{
            board[0, 0].move(new Tuple<int, int>(3, 0), this);
            board[3, 0] = board[0, 0];
            board[0, 0] = null;
        }
    }

    public void castle_short(bool isWhite){
        if(isWhite){
            board[7, 7].move(new Tuple<int, int>(5, 7), this);
            board[5, 7] = board[7, 7];
            board[7, 7] = null;
        }
        else{
            board[7, 0].move(new Tuple<int, int>(5, 0), this);
            board[5, 0] = board[7, 0];
            board[7, 0] = null;
        }
    }
    void copy(Piece[,] from, Piece[,] to){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                if(from[i, j] is King){
                    to[i, j] = new King((King)from[i, j]);
                }
                else if(from[i, j] is Queen){
                    to[i, j] = new Queen((Queen)from[i, j]);
                }
                else if(from[i, j] is Rook){
                    to[i, j] = new Rook((Rook)from[i, j]);
                }
                else if(from[i, j] is Bishop){
                    to[i, j] = new Bishop((Bishop)from[i, j]);
                }
                else if(from[i, j] is Knight){
                    to[i, j] = new Knight((Knight)from[i, j]);
                }
                else if(from[i, j] is Pawn){
                    to[i, j] = new Pawn((Pawn)from[i, j]);
                }
                else to[i, j] = null;
            }
        }
    }

    public bool is_mate(bool isWhite){
        Piece[,] curr = new Piece[width, height];
        King wKing_temp = new King((King)board[wKing.pos.Item1, wKing.pos.Item2]);
        King bKing_temp = new King((King)board[bKing.pos.Item1, bKing.pos.Item2]);
        copy(board, curr);
        foreach(Piece p in board){
            if(p == null) continue;
            if(p.isWhite != isWhite) continue;
            for(int i = 0; i < width; i++){
                for(int j = 0; j < height; j++){
                    if(p.move_list[i, j]){
                        if(this.move(new Tuple<int, int>(p.pos.Item1, p.pos.Item2), new Tuple<int, int>(i, j), isWhite)){
                            copy(curr, board);
                            wKing = (King)board[wKing_temp.pos.Item1, wKing_temp.pos.Item2];
                            bKing = (King)board[bKing_temp.pos.Item1, bKing_temp.pos.Item2];
                            return false;
                        }
                    }
                }
            }

        }
        return true;
    }
}