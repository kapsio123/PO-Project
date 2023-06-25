using Pieces;
using System;

namespace Boards;
public class Board{
    public static int height = 8;
    public static int width = 8;
    public Tuple<int, int> wKing_pos {get; private set;}
    public Tuple<int, int> bKing_pos {get; private set;}
    public Piece[,] board {get; private set;}
    public Board(){
        this.board = new Piece[height, width];
    }
    public bool promotion {get; set;}
    public Pawn toPromote {get; set;}
    public void Initialize(){
        board[0, 0] = new Rook(false, new Tuple<int, int>(0, 0));
        board[1, 0] = new Knight(false, new Tuple<int, int>(1, 0));
        board[2, 0] = new Bishop(false, new Tuple<int, int>(2, 0));
        board[3, 0] = new Queen(false, new Tuple<int, int>(3, 0));
        board[4, 0] = new King(false, new Tuple<int, int>(4, 0));
        bKing_pos = new Tuple<int, int>(4, 0);
        board[5, 0] = new Bishop(false, new Tuple<int, int>(5, 0));
        board[6, 0] = new Knight(false, new Tuple<int, int>(6, 0));
        board[7, 0] = new Rook(false, new Tuple<int, int>(7, 0));
        for(int i = 0; i < width; i++) board[i, 1] = new Pawn(false, new Tuple<int, int>(i, 1));

        board[0, 7] = new Rook(true, new Tuple<int, int>(0, 7));
        board[1, 7] = new Knight(true, new Tuple<int, int>(1, 7));
        board[2, 7] = new Bishop(true, new Tuple<int, int>(2, 7));
        board[3, 7] = new Queen(true, new Tuple<int, int>(3, 7));
        board[4, 7] = new King(true, new Tuple<int, int>(4, 7));
        wKing_pos = new Tuple<int, int>(4, 7);
        board[5, 7] = new Bishop(true, new Tuple<int, int>(5, 7));
        board[6, 7] = new Knight(true, new Tuple<int, int>(6, 7));
        board[7, 7] = new Rook(true, new Tuple<int, int>(7, 7));
        for(int i = 0; i < width; i++) board[i, 6] = new Pawn(true, new Tuple<int, int>(i, 6));

        promotion = false;

        this.update();
    }

    public bool move(Tuple<int, int> from, Tuple<int, int> to, bool White_turn){
        if(board[from.Item1, from.Item2].isWhite != White_turn) return false;
        if(!board[from.Item1, from.Item2].isLegal(to)) return false;

        board[from.Item1, from.Item2].move(to, this);

        if(from == wKing_pos){
            wKing_pos = to;
        }
        else if(from == bKing_pos){
            bKing_pos = to;
        }

        board[to.Item1, to.Item2] = board[from.Item1, from.Item2];
        board[from.Item1, from.Item2] = null;
        this.update();
        return true;
    }
    void update(){
        foreach(Piece p in board){
            if(p == null) continue;
            p.possible_moves(this);
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
}