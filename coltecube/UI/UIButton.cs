using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input; 
using coltecube.Objects;

namespace coltecube.UI;

public class UIButton : GameObject 
{
    // Texto
    public string Text { get; set; }
    private SpriteFont _font;
    
    // Evento
    public event Action OnClick;
    
    // Construtor
    public UIButton(Texture2D texture, Vector2 position, float scale, SpriteFont font, string text)
        : base(texture, position, scale)
    {
        _font = font;
        Text = text;
    }

    public override void Update(GameTime gameTime, MouseInfo mouse)
    {
        base.Update(gameTime, mouse);

        if (IsClicked(mouse)) OnClick?.Invoke();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        if (_font != null && !string.IsNullOrEmpty(Text))
        {
            Vector2 textSize = _font.MeasureString(Text);
            
            Rectangle buttonBounds = this.Bounds;

            Vector2 textPosition = new Vector2(
                (int)(buttonBounds.X + (buttonBounds.Width - textSize.X) / 2),
                (int)(buttonBounds.Y + (buttonBounds.Height - textSize.Y) / 2)
            );

            spriteBatch.DrawString(_font, Text, textPosition, Color.Black);
        }
    }
}
