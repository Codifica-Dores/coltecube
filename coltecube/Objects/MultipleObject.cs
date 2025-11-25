using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace coltecube.Objects;

// Objeto que pode ser rotaionado (ex: setas)
public class MultipleObject : InteractiveObject
{
    public float Rotation { get; set; }
	protected List<InteractiveObject> objects;

    // Construtor
    public MultipleObject(List<InteractiveObject> objects, Texture2D texture, Vector2 position, float scale = 1.0f)
        : base(texture, position, scale) 
    {
        Rotation = 0f;
		this.objects = objects;
    }

    public override void Update(GameTime gameTime, MonoGameLibrary.Input.MouseInfo mouse)
    {
		// Console.WriteLine("oxi "+this.name);
		// clickInner(mouse);
		base.Update(gameTime, mouse);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        
		base.DrawArea(spriteBatch);
    }
}
