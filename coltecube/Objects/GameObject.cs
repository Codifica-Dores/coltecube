using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input;
using System;

namespace coltecube.Objects;

public class GameObject
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public float Scale { get; set; }
    public bool IsVisible { get; set; } = true;
    
    // O retângulo (Bounds)
    public Rectangle Bounds => new Rectangle(
        (int)Position.X,
        (int)Position.Y,
        (int)(Texture.Width * Scale),
        (int)(Texture.Height * Scale)
    );

    // Construtor
    public GameObject(Texture2D texture, Vector2 position, float scale = 1.0f)
    {
        Texture = texture;
        Position = position;
        Scale = scale;
    }

    //Mouse
    public bool IsMouseOver(MouseInfo mouse) { return IsVisible && Bounds.Contains(mouse.Position); }
    public bool IsClicked(MouseInfo mouse) { return IsMouseOver(mouse) && mouse.WasButtonJustPressed(MouseButton.Left); }

    
    public virtual void Update(GameTime gameTime, MouseInfo mouse) { }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (IsVisible && Texture != null)
        {
			// Console.WriteLine("Desenhando objeto em " + Position + " ... " +Texture.Width + "-" +Texture.Height);
            spriteBatch.Draw(
                Texture,
                Position,     // Posição (top-left)
                null,         // sourceRect (textura inteira)
                Color.White,
                0f,           // Rotação (nenhuma)
                Vector2.Zero, // Origem (top-left)
                Scale,        // Escala proporcional!
                SpriteEffects.None,
                0f
            );
        }
			
    }
	public virtual void Draw(SpriteBatch spriteBatch, Vector2 screenCenter)
    {
        if (IsVisible && Texture != null)
        {
			// Console.WriteLine("Desenhando objeto em " + Position + " ... " +Texture.Width + "-" +Texture.Height);
            spriteBatch.Draw(
                Texture,
                Position + screenCenter,     // Posição (top-left)
                null,         // sourceRect (textura inteira)
                Color.White,
                0f,           // Rotação (nenhuma)
                Vector2.Zero, // Origem (top-left)
                Scale,        // Escala proporcional!
                SpriteEffects.None,
                0f
            );
        }
			
    }
}
