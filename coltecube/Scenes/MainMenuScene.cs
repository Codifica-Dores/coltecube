using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;          
using MonoGameLibrary.Scenes;   
using coltecube.UI;          
using System.Collections.Generic;

namespace coltecube.Scenes
{
    public class MainMenuScene : Scene
    {
        private Texture2D _background;
        private SpriteFont _menuFont;
        private UIButton _playButton;
        
        public override void LoadContent()
        {
            // Files
            _background = Content.Load<Texture2D>("Sprites/Backgrounds/entrada_coltec");
            var buttonTexture = Content.Load<Texture2D>("UI/Button");
            _menuFont = Content.Load<SpriteFont>("Fonts/File");

            // Botão
            float buttonScale = 0.25f;
            int buttonWidth = (int)(buttonTexture.Width * buttonScale);
            int buttonHeight = (int)(buttonTexture.Height * buttonScale);

            Vector2 buttonPosition = new Vector2(
                (Core.Graphics.PreferredBackBufferWidth - buttonWidth) / 2,
                (Core.Graphics.PreferredBackBufferHeight - buttonHeight) / 2
            );

            _playButton = new UIButton(
                buttonTexture,
                buttonPosition, // Vector2
                buttonScale,    // float
                _menuFont,
                "JOGAR"
            );

            _playButton.OnClick += () => 
            {
                Core.ChangeScene(new HallScene());
            };
			
        }

        public override void Update(GameTime gameTime)
        {
            _playButton.Update(gameTime, Core.Input.Mouse);

            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            int screenWidth = Core.GraphicsDevice.Viewport.Width;
            int screenHeight = Core.GraphicsDevice.Viewport.Height;
            
            int x = (screenWidth - _background.Width) / 2;
            int y = (screenHeight - _background.Height) / 2;

            Vector2 backgroundPosition = new Vector2(x, y);

            Core.SpriteBatch.Draw(_background, backgroundPosition, Color.White);

            _playButton.Draw(Core.SpriteBatch);

            base.Draw(gameTime);
        }
    }
}