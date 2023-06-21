using Pieces;
using System;

namespace Boards;
public class Board{
    public static int height = 8;
    public static int width = 8;
    public Piece[,] board {get; private set;}
    public Board(){
        this.board = new Piece[height, width];
    }
    public void Initialize(){
        board[0, 0] = new Rook(false, new Tuple<int, int>(0, 0));
        board[1, 0] = new Knight(false, new Tuple<int, int>(1, 0));
        board[2, 0] = new Bishop(false, new Tuple<int, int>(2, 0));
        board[3, 0] = new Queen(false, new Tuple<int, int>(3, 0));
        board[4, 0] = new King(false, new Tuple<int, int>(4, 0), this);
        board[5, 0] = new Bishop(false, new Tuple<int, int>(5, 0));
        board[6, 0] = new Knight(false, new Tuple<int, int>(6, 0));
        board[7, 0] = new Rook(false, new Tuple<int, int>(7, 0));
        for(int i = 0; i < width; i++) board[i, 1] = new Pawn(false, new Tuple<int, int>(i, 1));

        board[0, 7] = new Rook(true, new Tuple<int, int>(0, 7));
        board[1, 7] = new Knight(true, new Tuple<int, int>(1, 7));
        board[2, 7] = new Bishop(true, new Tuple<int, int>(2, 7));
        board[3, 7] = new Queen(true, new Tuple<int, int>(3, 7));
        board[4, 7] = new King(true, new Tuple<int, int>(4, 7), this);
        board[5, 7] = new Bishop(true, new Tuple<int, int>(5, 7));
        board[6, 7] = new Knight(true, new Tuple<int, int>(6, 7));
        board[7, 7] = new Rook(true, new Tuple<int, int>(7, 7));
        for(int i = 0; i < width; i++) board[i, 6] = new Pawn(true, new Tuple<int, int>(i, 6));
        System.Console.WriteLine(board[4, 7].move_list[5, 7]);
        for(int i = 0; i < width; i++){
            board[i, 0].possible_moves(this);
            board[i, 7].possible_moves(this);
        }
    }

    public bool move(Tuple<int, int> from, Tuple<int, int> to){
        if(!board[from.Item1, from.Item2].isLegal(to)) return false;

        System.Console.WriteLine(board[from.Item1, from.Item2].move_list);
        board[from.Item1, from.Item2].move(to, this);
        board[to.Item1, to.Item2] = board[from.Item1, from.Item2];
        board[from.Item1, from.Item2] = null;
        return true;
    }
}