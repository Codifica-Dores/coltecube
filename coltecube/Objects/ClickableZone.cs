using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Input;

namespace coltecube.Objects;

public class ClickableZone : InteractiveObject
{
    // Construtor
    public ClickableZone(Rectangle area) 
        : base(null, new Vector2(area.X, area.Y)) 
    {
        this.Bounds = area;
        this.name = "Zona Invisivel";
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        
            // Desenha um retângulo AZUL semi-transparente
            // Parâmetros de Cor: R=0, G=0, B=255, Alpha=80
            spriteBatch.Draw(Game1.Pixelw, Bounds, new Color(0, 0, 0, 90));
        
    }
}