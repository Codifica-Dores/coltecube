using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable enable

namespace TrabalhoFinal.Interactions;

public sealed class LockCombinationObject : IInteractiveObject
{
    private readonly Texture2D _baseTexture;
    private readonly Texture2D _arrowTexture;
    private readonly SpriteFont _font;
    private readonly Rectangle _arrowSourceRect;
    private readonly int[] _digits = new int[3];

    private Rectangle _bounds;
    private Rectangle[] _arrowUpRectangles = Array.Empty<Rectangle>();
    private Rectangle[] _arrowDownRectangles = Array.Empty<Rectangle>();
    private Vector2[] _digitPositions = Array.Empty<Vector2>();

    public LockCombinationObject(string id, Texture2D baseTexture, Texture2D arrowTexture, Rectangle arrowSourceRect, SpriteFont font)
    {
        Id = id;
        _baseTexture = baseTexture;
        _arrowTexture = arrowTexture;
        _arrowSourceRect = arrowSourceRect;
        _font = font;
    }

    public event Action<int[]>? CombinationChanged; // += _digits =>

    public string Id { get; }
    public bool IsActive { get; private set; }
    public Rectangle Bounds => _bounds;

    public void Show(Rectangle viewportBounds)
    {
        IsActive = true;
        var position = new Point(
            viewportBounds.Center.X - _baseTexture.Width / 2,
            viewportBounds.Center.Y - _baseTexture.Height / 2
        );
        _bounds = new Rectangle(position.X, position.Y, _baseTexture.Width, _baseTexture.Height);
        LayoutControls();
    }

    public void Hide()
    {
        IsActive = false;
    }

    public void Update(GameTime gameTime)
    {
        if (!IsActive) return;
    }

    public bool HandleClick(Point position, MouseButton button)
    {
        if (!IsActive || button != MouseButton.Left)
        {
            return false;
        }

        for (int i = 0; i < _arrowUpRectangles.Length; i++)
        {
            if (_arrowUpRectangles[i].Contains(position))
            {
                IncrementDigit(i);
                return true;
            }
            if (_arrowDownRectangles[i].Contains(position))
            {
                DecrementDigit(i);
                return true;
            }
        }

        return false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive)
        {
            return;
        }

        spriteBatch.Draw(_baseTexture, _bounds, Color.White);

        for (int i = 0; i < _digitPositions.Length; i++)
        {
            var value = _digits[i].ToString();
            var size = _font.MeasureString(value);
            var origin = size / 2f;
            spriteBatch.DrawString(_font, value, _digitPositions[i], Color.Black, 0f, origin, 1.2f, SpriteEffects.None, 0f);
        }

        DrawArrows(spriteBatch);
    }

    private void LayoutControls()
    {
        const int digitColumns = 3;
        _arrowUpRectangles = new Rectangle[digitColumns];
        _arrowDownRectangles = new Rectangle[digitColumns];
        _digitPositions = new Vector2[digitColumns];

        int columnWidth = _bounds.Width / (digitColumns + 1);
        int baseY = _bounds.Top + _bounds.Height / 2;
        int arrowWidth = _arrowSourceRect.Height;
        int arrowHeight = _arrowSourceRect.Width;
        int spacing = columnWidth;

        for (int i = 0; i < digitColumns; i++)
        {
            int centerX = _bounds.Left + spacing * (i + 1);
            _digitPositions[i] = new Vector2(centerX, baseY);

            _arrowUpRectangles[i] = new Rectangle(
                centerX - arrowHeight / 2,
                baseY - arrowHeight - 30,
                arrowHeight,
                arrowWidth
            );

            _arrowDownRectangles[i] = new Rectangle(
                centerX - arrowHeight / 2,
                baseY + 30,
                arrowHeight,
                arrowWidth
            );
        }
    }

    private void DrawArrows(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _arrowUpRectangles.Length; i++)
        {
            var upRect = _arrowUpRectangles[i];
            var downRect = _arrowDownRectangles[i];

            var upPosition = new Vector2(upRect.X + upRect.Width / 2f, upRect.Y + upRect.Height / 2f);
            var downPosition = new Vector2(downRect.X + downRect.Width / 2f, downRect.Y + downRect.Height / 2f);

            spriteBatch.Draw(
                _arrowTexture,
                upPosition,
                _arrowSourceRect,
                Color.White,
                -MathHelper.PiOver2,
                new Vector2(_arrowSourceRect.Width / 2f, _arrowSourceRect.Height / 2f),
                1f,
                SpriteEffects.None,
                0f);

            spriteBatch.Draw(
                _arrowTexture,
                downPosition,
                _arrowSourceRect,
                Color.White,
                MathHelper.PiOver2,
                new Vector2(_arrowSourceRect.Width / 2f, _arrowSourceRect.Height / 2f),
                1f,
                SpriteEffects.None,
                0f);
        }
    }

    private void IncrementDigit(int index)
    {
        _digits[index] = (_digits[index] + 1) % 10; // 0-9
        CombinationChanged?.Invoke((int[])_digits.Clone()); // Notify listeners of the change
    }

    private void DecrementDigit(int index)
    {
        _digits[index] = (_digits[index] + 9) % 10;
        CombinationChanged?.Invoke((int[])_digits.Clone());
    }
}
