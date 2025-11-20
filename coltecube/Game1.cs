using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;          
using MonoGameLibrary.Scenes;
using coltecube.Scenes;

namespace coltecube;

public class Game1 : Core
{
    public const int NATIVE_HEIGHT = 960;
	public const int ESPACO_LATERAL_ITEMS = 200;
    public const int NATIVE_WIDTH = NATIVE_HEIGHT * 4/3+ESPACO_LATERAL_ITEMS; // 4:3 Aspect Ratio minus side space

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

        Core.ChangeScene(new MainMenuScene()); // mudar aqui > MainMenuScene()
    }

    protected override void Update(GameTime gameTime)
    {
        // Tela cheia
        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.F11))
        {
            Core.Graphics.ToggleFullScreen();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}
