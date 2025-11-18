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

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsVisible && Texture != null)
        {
            // Origem é o centro da textura (para girar em torno do centro)
            Vector2 origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);

            // Calcula o centro da imagem na tela
            Vector2 drawPosition = new Vector2(
                Position.X + (Texture.Width * Scale) / 2f,
                Position.Y + (Texture.Height * Scale) / 2f
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
