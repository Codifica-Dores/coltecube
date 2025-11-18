using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input;
using coltecube.Objects;
using System.Collections.Generic;

namespace coltecube.Scenes;

// Representa uma única face/vista de um cubo (como uma parede).
public abstract class View
{
    protected Texture2D _background;
    protected float _backgroundScale = 0.32f;
    protected List<GameObject> _objects = new List<GameObject>();

    public virtual void LoadContent(ContentManager content) { }

    public virtual void UnloadContent() { }

    public virtual void Update(GameTime gameTime, MouseInfo mouse)
    {
        foreach (var obj in _objects) obj.Update(gameTime, mouse);
    }

    public virtual void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        // Desenha o Fundo (Centralizado) 
        Vector2 screenCenter = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 2f);
        Vector2 textureCenterOrigin = new Vector2(_background.Width / 2f, _background.Height / 2f);

        spriteBatch.Draw(
            _background,
            screenCenter,
            null, Color.White, 0f,
            textureCenterOrigin,
            _backgroundScale,
            SpriteEffects.None, 0f
        );

        // Desenha os Objetos
        foreach (var obj in _objects)
        {
            obj.Draw(spriteBatch);
        }
    }
}
