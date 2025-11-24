using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input;
using System;
using coltecube.Scenes;

namespace coltecube.Objects;

public class GameObject
{
	public Texture2D Texture { get; set; }
	public Color[] PixelData { get; private set; } 
	
	private Vector2 _position;
	private float _scale;
	
	public Vector2 Position 
	{ 
		get { return _position; } 
		set 
		{ 
			_position = value; 
			UpdateBounds(); 
		} 
	}

	public float Scale 
	{ 
		get { return _scale; } 
		set 
		{ 
			_scale = value; 
			UpdateBounds(); 
		} 
	}

	public bool IsVisible { get; set; } = true;
	
	public Rectangle Bounds;
	
	public string name;

	// Método auxiliar para recalcular o retângulo
	private void UpdateBounds()
	{
		if (Texture != null)
		{
			Bounds = new Rectangle(
				(int)_position.X,
				(int)_position.Y,
				(int)(Texture.Width * _scale),
				(int)(Texture.Height * _scale)
			);
		}
	}

	// Construtor
	public GameObject(Texture2D texture, Vector2 position, float scale = 1.0f)
	{
		Texture = texture;
		_scale = scale; 
		_position = position; 
        
		name = "-Sem Nome-";
        
		PixelData = new Color[texture.Width * texture.Height];
		texture.GetData(PixelData);

		UpdateBounds(); 
	}
    
    //Mouse
    public bool IsMouseOver(MouseInfo mouse) 
    { 
	    // Verifica se o mouse está dentro do retângulo bruto
	    if (!IsVisible || !Bounds.Contains(new Point(mouse.Position.X, mouse.Position.Y))) return false;
	    
	    int localX = (int)((mouse.Position.X - Position.X) / Scale);
	    int localY = (int)((mouse.Position.Y - Position.Y) / Scale);

	    if (localX < 0 || localX >= Texture.Width || localY < 0 || localY >= Texture.Height) return false;

	    // Pega o pixel
	    Color pixelColor = PixelData[localY * Texture.Width + localX];

	    return pixelColor.A > 10; 
    }
    
    
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
	// public virtual void Draw(SpriteBatch spriteBatch)
    // {
    //     if (IsVisible && Texture != null)
    //     {
	// 		// Console.WriteLine("Desenhando objeto em " + Position + " ... " +Texture.Width + "-" +Texture.Height);
	// 		// Bounds.X+=screenCenter.X;
	// 		// Bounds.Y+=screenCenter.Y;
    //         spriteBatch.Draw(
    //             Texture,
    //             Position,     // Posição (top-left)
    //             null,         // sourceRect (textura inteira)
    //             Color.White,
    //             0f,           // Rotação (nenhuma)
    //             Vector2.Zero, // Origem (top-left)
    //             Scale,        // Escala proporcional!
    //             SpriteEffects.None,
    //             0f
    //         );
    //     }			
    // }	
}
