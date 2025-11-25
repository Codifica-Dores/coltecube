using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;          
using MonoGameLibrary.Scenes;
using coltecube.Scenes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System.Collections.Generic;
using System;

namespace coltecube;

public class Game1 : Core
{
    public const int NATIVE_HEIGHT = 960;
	public const int ESPACO_LATERAL_ITEMS = 200;
    public const int NATIVE_WIDTH = NATIVE_HEIGHT * 4/3+ESPACO_LATERAL_ITEMS; // 4:3 Aspect Ratio minus side space

	public static Texture2D Pixelw;
	public static bool showLocals = false;
	public static Inventory inventory = null;

    // Construtor
    public Game1()
        : base("Coltecube Escape", NATIVE_WIDTH, NATIVE_HEIGHT, false)
    {
        Window.AllowUserResizing = true;

        Core.ExitOnEscape = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

		Pixelw = new Texture2D(GraphicsDevice, 1, 1);
    	Pixelw.SetData(new[] { Color.White });

        Core.ChangeScene(new MainMenuScene()); // mudar aqui > MainMenuScene()
		
		Game1.inventory = new Inventory(Content.Load<Texture2D>("Backgrounds/quadradinho"),
		new Vector2((Game1.NATIVE_WIDTH+Game1.ESPACO_LATERAL_ITEMS)/2,Game1.NATIVE_HEIGHT/2),
		0.05f);
		Game1.inventory.Charge();
    }

    protected override void Update(GameTime gameTime)
    {
        // Tela cheia
        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.F11))
        {
            Core.Graphics.ToggleFullScreen();
        }

		// mostra as paradas pra clicar
		if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Space))
        {
            Game1.showLocals = (Game1.showLocals == false);
        }

		// posição
		// MouseInfo mouse = MonoGameLibrary.Input.MouseInfo;
		// if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Enter))
        // {
		// 	Console.WriteLine(mouse.Position.X + ", " + mouse.Position.Y);
        // }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}
