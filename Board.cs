namespace Boards;
using Pieces;
public class Board{
    static int height = 8;
    static int width = 8;
    public Piece[,] board {get;}
    public Board(){
        this.board = new Piece[height, width];
        this.board[0, 0] = new Pawn(true, new System.Tuple<int, int>(0, 0));
        this.board[2, 2] = new Bishop(false, new System.Tuple<int, int>(2, 2));
    }
    public void Initialize(){
        if(board[0, 0] == null) System.Console.Write("123");
    }
}