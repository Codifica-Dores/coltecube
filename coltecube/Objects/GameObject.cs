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
	
	public Vector2 _position;
	private float _scale;
	
	public Vector2 Position 
	{ 
		get { return _position; } 
		set { _position = value; UpdateBounds(); } 
	}

	public float Scale 
	{ 
		get { return _scale; } 
		set { _scale = value; UpdateBounds(); } 
	}

	public bool IsVisible { get; set; } = true;
	public Rectangle Bounds;
	public string name;

	public GameObject(Texture2D texture, Vector2 position, float scale = 1.0f)
	{
		Texture = texture;
		_scale = scale; 
		_position = position; 
		name = "-Sem Nome-";
        
        // [MODIFICAÇÃO] Só tenta ler pixels se tiver textura
		if (Texture != null)
		{
		    PixelData = new Color[texture.Width * texture.Height];
		    texture.GetData(PixelData);
		}

		UpdateBounds(); 
	}

	private void UpdateBounds()
	{
        // [MODIFICAÇÃO] Só calcula tamanho automático se tiver textura
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
    
    public virtual bool IsMouseOver(MouseInfo mouse) 
    { 
	    if (!IsVisible || !Bounds.Contains(new Point(mouse.Position.X, mouse.Position.Y))) return false;
	    
        // [MODIFICAÇÃO] Se não tem textura (zona invisível), mas está no Bounds, colidiu!
        if (Texture == null) return true;

        // Se tem textura, faz a checagem detalhada
	    int localX = (int)((mouse.Position.X - Position.X) / Scale);
	    int localY = (int)((mouse.Position.Y - Position.Y) / Scale);

	    if (localX < 0 || localX >= Texture.Width || localY < 0 || localY >= Texture.Height) return false;

	    Color pixelColor = PixelData[localY * Texture.Width + localX];
	    return pixelColor.A > 10; 
    }
    
    public bool IsClicked(MouseInfo mouse) { return IsMouseOver(mouse) && mouse.WasButtonJustPressed(MouseButton.Left); }

    public virtual void Update(GameTime gameTime, MouseInfo mouse) { }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (IsVisible && Texture != null)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }			
    }
}