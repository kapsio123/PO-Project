namespace Boards;
public class Board{
    static int height = 8;
    static int width = 8;
    int[,] board {get;}
    public Board(){
        this.board = new int[height, width];
    }
}