using System;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace coltecube.Objects;

// Objeto interativo (ex: dedo, panela, cadeado)
public class InteractiveObject : GameObject
{
    // Evento
    public event Action OnClick;

    // Construtor
    public InteractiveObject(Texture2D texture, Vector2 position, float scale = 1.0f)
        : base(texture, position, scale) 
    {
    }
    
    // Permite classes derivadas dispararem o evento de clique
    protected void RaiseClick() { OnClick?.Invoke(); }
    
    public override void Update(GameTime gameTime, MouseInfo mouse)
    {
		
        if (IsClicked(mouse))
        {
            OnClick?.Invoke();
			// Console.WriteLine("ooooooo");
        }else{
			// Console.WriteLine("pos: "+Bounds.X+" "+Bounds.Y+" tam: "+Bounds.Width+" "+Bounds.Height);
			// Console.WriteLine(this.name+" "+mouse.Position);
			// Console.WriteLine(Bounds.Contains(mouse.Position)+" "+mouse.WasButtonJustPressed(MouseButton.Left));
		}
        base.Update(gameTime, mouse);
    }

	public override void Draw(SpriteBatch spriteBatch){
		// this.Bounds = new Rectangle(
		// 	(int)(center.X),
		// 	(int)(center.Y),
		// 	(int)(300),
		// 	(int)(300)
		// );	
		this.DrawArea(spriteBatch);
		base.Draw(spriteBatch);
	}

	public void DrawArea(SpriteBatch spriteBatch){
		// Console.WriteLine("pos: "+Bounds.X+" "+Bounds.Y+" tam: "+Bounds.Width+" "+Bounds.Height);
		if (Game1.showLocals) spriteBatch.Draw(Game1.Pixelw, new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height), new Color(255, 0, 0, 80));
	}
}
