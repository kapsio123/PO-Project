using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Boards;

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

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = spriteSize * 8;
        _graphics.PreferredBackBufferHeight = spriteSize * 8;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        board = new Board();
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

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

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        for(int i = 0; i < 8; i++){
            for(int j = 0; j < 8; j++){
                if ((j + i) % 2 == 0){
                    _spriteBatch.Draw(TileLight, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                }
                else{
                    _spriteBatch.Draw(TileDark, new Rectangle(spriteSize * j, spriteSize * i, spriteSize, spriteSize), Color.White);
                }
            }
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
