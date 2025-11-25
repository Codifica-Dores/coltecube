using coltecube.Objects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coltecube.Scenes.CorredorInfo.LabAmarelo;

public class LabAmarelo : View
{
    private InteractiveObject arcondicionadoLigado;
    private InteractiveObject arcondicionadoDesligado;

    public override void LoadContent(ContentManager content)
    {
        _background = content.Load<Texture2D>("Backgrounds/LabAmarelo/background");
    }
}