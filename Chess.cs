using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Boards;
using Pieces;
using System;

namespace Szachy;

public class Chess : Game
{
    Texture2D TileLight;
    Texture2D TileDark;
    Texture2D QueenB;
    Texture2D KingB;
    Texture2D RookB;
    Texture2D BishopB;
    Texture2D KnightB;
    Texture2D PawnB;
    Texture2D QueenW;
    Texture2D KingW;
    Texture2D RookW;
    Texture2D BishopW;
    Texture2D KnightW;
    Texture2D PawnW;
    static int spriteSize = 80;
    Board board;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    bool selected;
    bool promotion;
    bool White_turn;
    Point selectedPiece_pos;

    public Chess()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = spriteSize * 8;
        _graphics.PreferredBackBufferHeight = spriteSize * 8;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        board = new Board();
        board.Initialize();
        selected = false;
        White_turn = true;
        promotion = false;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        TileLight = Content.Load<Texture2D>("square brown dark_png_shadow_128px");
        TileDark = Content.Load<Texture2D>("square brown light_png_shadow_128px");

        KingB = Content.Load<Texture2D>("b_king_png_shadow_128px");
        QueenB = Content.Load<Texture2D>("b_queen_png_shadow_128px");
        RookB = Content.Load<Texture2D>("b_rook_png_shadow_128px");
        BishopB = Content.Load<Texture2D>("b_bishop_png_shadow_128px");
        KnightB = Content.Load<Texture2D>("b_knight_png_shadow_128px");
        PawnB = Content.Load<Texture2D>("b_pawn_png_shadow_128px");

        KingW = Content.Load<Texture2D>("w_king_png_shadow_128px");
        QueenW = Content.Load<Texture2D>("w_queen_png_shadow_128px");
        RookW = Content.Load<Texture2D>("w_rook_png_shadow_128px");
        BishopW = Content.Load<Texture2D>("w_bishop_png_shadow_128px");
        KnightW = Content.Load<Texture2D>("w_knight_png_shadow_128px");
        PawnW = Content.Load<Texture2D>("w_pawn_png_shadow_128px");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (Keyboard.GetState().IsKeyDown(Keys.R))
            this.Initialize();
        // TODO: Add your update logic here
        MouseState mouse = Mouse.GetState();
        Point current_pos = mouse.Position;
        int posX = current_pos.X / spriteSize;
        int posY = current_pos.Y / spriteSize;
        if(!promotion){
            if(mouse.LeftButton == ButtonState.Pressed && current_pos != selectedPiece_pos && posX >= 0 && posX < Board.width && posY >= 0 && posY < Board.height){
                if(selected){
                    if(board.move(new System.Tuple<int, int>(selectedPiece_pos.X / spriteSize, selectedPiece_pos.Y / spriteSize),
                            new System.Tuple<int, int>(current_pos.X / spriteSize, current_pos.Y / spriteSize), White_turn)) White_turn = !White_turn;
                    selected  = false;
                    selectedPiece_pos = new Point(42, 42);
                    promotion = board.promotion;
                    board.promotion = false;
                    if(board.is_mate(White_turn) && (board.wKing.inCheck() || board.bKing.inCheck())){
                        System.Console.Write("Win: ");
                        if(!White_turn) System.Console.WriteLine("White");
                        else System.Console.WriteLine("Black");
                        //Exit();
                    } 
                }
                else{
                    if(board.board[current_pos.X / spriteSize, current_pos.Y / spriteSize] != null && board.board[current_pos.X / spriteSize, current_pos.Y / spriteSize].isWhite == White_turn){
                        selected = true;
                        selectedPiece_pos = current_pos;
                    }
                }
            }
        }
        else if(mouse.LeftButton == ButtonState.Pressed && posX >= 0 && posX < Board.width && posY >= 0 && posY < Board.height){
            if(posY == Board.height / 2 - 1){
                if(posX == Board.width / 2 - 2){
                    board.board[board.toPromote.pos.Item1, board.toPromote.pos.Item2] = new Knight(board.toPromote.isWhite, board.toPromote.pos);
                    promotion = false;
                }
                else if(posX == Board.width / 2 - 1){
                    board.board[board.toPromote.pos.Item1, board.toPromote.pos.Item2] = new Bishop(board.toPromote.isWhite, board.toPromote.pos);
                    promotion = false;
                }
                else if(posX == Board.width / 2){
                    board.board[board.toPromote.pos.Item1, board.toPromote.pos.Item2] = new Rook(board.toPromote.isWhite, board.toPromote.pos);
                    promotion = false;
                }
                else if(posX == Board.width / 2 + 1){
                    board.board[board.toPromote.pos.Item1, board.toPromote.pos.Item2] = new Queen(board.toPromote.isWhite, board.toPromote.pos);
                    promotion = false;
                }
            }
        }
        /*System.Console.WriteLine(Mouse.GetState());
        System.Console.WriteLine(selected);
        System.Console.WriteLine(selectedPiece_pos.Y / spriteSize);
        System.Console.WriteLine(System.DateTime.Now.TimeOfDay);*/
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        for(int i = 0; i < 8; i++){
            for(int j = 0; j < 8; j++){
                if((j + i) % 2 == 0){
                    _spriteBatch.Draw(TileLight, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                }
                else{
                    _spriteBatch.Draw(TileDark, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                }
                if(j == this.selectedPiece_pos.X / spriteSize && i == this.selectedPiece_pos.Y / spriteSize && selected){
                    _spriteBatch.Draw(TileDark, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.Cyan);
                }
                Piece temp = board.board[j, i];
                if(temp != null){
                    switch (temp.pieceId){
                        case 1:
                        if (temp.isWhite){
                            _spriteBatch.Draw(PawnW, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        else{
                            _spriteBatch.Draw(PawnB, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        break;

                        case 2:
                        if (temp.isWhite){
                            _spriteBatch.Draw(KnightW, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        else{
                            _spriteBatch.Draw(KnightB, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        break;

                        case 3:
                        if (temp.isWhite){
                            _spriteBatch.Draw(BishopW, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        else{
                            _spriteBatch.Draw(BishopB, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        break;

                        case 4:
                        if (temp.isWhite){
                            _spriteBatch.Draw(RookW, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        else{
                            _spriteBatch.Draw(RookB, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        break;

                        case 5:
                        if (temp.isWhite){
                            _spriteBatch.Draw(QueenW, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        else{
                            _spriteBatch.Draw(QueenB, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        break;

                        case 6:
                        if (temp.isWhite){
                            _spriteBatch.Draw(KingW, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        else{
                            _spriteBatch.Draw(KingB, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                        }
                        break;
                    }
                }
            }
        }
        if(promotion){
            _spriteBatch.Draw(TileDark, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - spriteSize * 2, _graphics.PreferredBackBufferHeight / 2 - spriteSize, spriteSize, spriteSize), Color.Black);
            _spriteBatch.Draw(TileDark, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - spriteSize, _graphics.PreferredBackBufferHeight / 2 - spriteSize, spriteSize, spriteSize), Color.Black);
            _spriteBatch.Draw(TileDark, new Rectangle(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 - spriteSize, spriteSize, spriteSize), Color.Black);
            _spriteBatch.Draw(TileDark, new Rectangle(_graphics.PreferredBackBufferWidth / 2 + spriteSize, _graphics.PreferredBackBufferHeight / 2 - spriteSize, spriteSize, spriteSize), Color.Black);

            _spriteBatch.Draw(KnightW, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - spriteSize * 2, _graphics.PreferredBackBufferHeight / 2 - spriteSize, spriteSize, spriteSize), Color.White);
            _spriteBatch.Draw(BishopW, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - spriteSize, _graphics.PreferredBackBufferHeight / 2 - spriteSize, spriteSize, spriteSize), Color.White);
            _spriteBatch.Draw(RookW, new Rectangle(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 - spriteSize, spriteSize, spriteSize), Color.White);
            _spriteBatch.Draw(QueenW, new Rectangle(_graphics.PreferredBackBufferWidth / 2 + spriteSize, _graphics.PreferredBackBufferHeight / 2 - spriteSize, spriteSize, spriteSize), Color.White);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
