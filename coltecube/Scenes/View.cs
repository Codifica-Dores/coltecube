using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input;
using coltecube.Objects;
using System;
using System.Collections.Generic;

namespace coltecube.Scenes;

// Representa uma única face/vista de um cubo (como uma parede).
public abstract class View
{
    public Texture2D _background; // public pra usar em todas as scenes
	public List<(Texture2D texture, Vector2 position)> _things = new List<(Texture2D texture, Vector2 position)>();
    public float _backgroundScale = (Game1.NATIVE_HEIGHT)/1536f;  // public pra usar em todas as scenes
    protected List<GameObject> _objects = new List<GameObject>();

    public virtual void LoadContent(ContentManager content) { }

    public virtual void UnloadContent() { }

    public virtual void Update(GameTime gameTime, MouseInfo mouse)
    {
        if (_objects.Count == 0) return;
        for (int i = 0; i < _objects.Count; i++)
        {
            // Capturar uma referência estável
            var obj = _objects[i];
            obj.Update(gameTime, mouse);
        }
    }

	public static Vector2 center;

    public virtual void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        // Desenha o Fundo (Centralizado) 
        Vector2 screenCenter = new Vector2(graphicsDevice.Viewport.Width / 2f-Game1.ESPACO_LATERAL_ITEMS/2, graphicsDevice.Viewport.Height / 2f);
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
		var center = new Vector2(graphicsDevice.Viewport.Width/ 2f-Game1.ESPACO_LATERAL_ITEMS/2, graphicsDevice.Viewport.Height / 2f);
        foreach (var obj in _objects)
        {
            obj.Draw(spriteBatch);
        }
		View.center = center;
    }
}
