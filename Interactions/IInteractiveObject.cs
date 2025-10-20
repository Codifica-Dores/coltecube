/// <summary>
/// Interface para objetos interativos na tela.
/// Depois de já clicado e aberto
/// </summary>      

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrabalhoFinal.Interactions;

public enum MouseButton
{
    Left,
    Right,
    Middle
}

public interface IInteractiveObject
{
    string Id { get; } // identificador único do objeto
    bool IsActive { get; } // indica se o objeto está ativo
    Rectangle Bounds { get; } // em coordenadas da tela

    void Show(Rectangle viewportBounds);
    void Hide();
    void Update(GameTime gameTime);
    /// <summary>
    /// Handles a click. Returns true if the click was consumed.
    /// </summary>
    bool HandleClick(Point position, MouseButton button);
    void Draw(SpriteBatch spriteBatch);
}
