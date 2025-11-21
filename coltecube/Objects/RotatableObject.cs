using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace coltecube.Objects;

// Objeto que pode ser rotaionado (ex: setas)
public class RotatableObject : InteractiveObject
{
    public float Rotation { get; set; }

    // Construtor
    public RotatableObject(Texture2D texture, Vector2 position, float scale = 1.0f)
        : base(texture, position, scale) 
    {
        Rotation = 0f;
    }

    public override void Update(GameTime gameTime, MonoGameLibrary.Input.MouseInfo mouse)
    {
		// Console.WriteLine("oxi "+this.name);
		clickInner(mouse);
		base.Update(gameTime, mouse);
        // // Calcula a posição real na tela da sprite (mesma lógica do Draw)
        // var viewport = MonoGameLibrary.Core.GraphicsDevice.Viewport;
        // var center = new Vector2(viewport.Width / 2f - coltecube.Game1.ESPACO_LATERAL_ITEMS/2, viewport.Height / 2f);

        // // Posição do centro do sprite na tela (consistente com Draw)
        // var drawPosition = new Vector2(
        //     Position.X + (Texture.Width * Scale) / 2f + center.X,
        //     Position.Y + (Texture.Height * Scale) / 2f + center.Y
        // );

        // // Origem (centro da textura) e top-left do retângulo de colisão
        // var origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        // var topLeft = drawPosition - origin * Scale;

        // var bounds = new Rectangle(
        //     (int)topLeft.X,
        //     (int)topLeft.Y,
        //     (int)(Texture.Width * Scale),
        //     (int)(Texture.Height * Scale)
        // );

        // if (IsVisible && bounds.Contains(mouse.Position) && mouse.WasButtonJustPressed(MonoGameLibrary.Input.MouseButton.Left))
        // {
        //     // Use helper to raise the event from base class
        //     RaiseClick();
        // }
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 center)
    {
        if (IsVisible && Texture != null)
        {
            // Origem é o centro da textura (para girar em torno do centro)
            Vector2 origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);

            // Calcula o centro da imagem na tela
            Vector2 drawPosition = new Vector2(
                Position.X + (Texture.Width * Scale) / 2f+center.X,
                Position.Y + (Texture.Height * Scale) / 2f+center.Y
            );

            spriteBatch.Draw(
                Texture,
                drawPosition, // Posição (centro do objeto na tela)
                null,         // sourceRect (textura inteira)
                Color.White,
                Rotation,     // Rotação
                origin,       // Origem da rotação (centro da textura)
                Scale,        // Escala proporcional
                SpriteEffects.None,
                0f
            );
        }
    }
}
