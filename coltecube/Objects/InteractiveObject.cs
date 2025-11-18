using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input;

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
    
    public override void Update(GameTime gameTime, MouseInfo mouse)
    {
        base.Update(gameTime, mouse);
        if (IsClicked(mouse))
        {
            OnClick?.Invoke();
        }
    }
}
