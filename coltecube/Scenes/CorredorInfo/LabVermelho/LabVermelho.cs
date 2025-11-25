using coltecube.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coltecube.Scenes.CorredorInfo.LabVermelho;

public class LabVermelho : View
{
    private InteractiveObject adesivo;
    private InteractiveObject computador;
    private InteractiveObject impressora;

    public override void LoadContent(ContentManager content)
    {
        //Background
        _background = content.Load<Texture2D>("Backgrounds/LabVermelho/background");
         
        // adesivo
        adesivo = new InteractiveObject(content.Load<Texture2D>("Backgrounds/LabVermelho/adesivo"), 
            new Vector2(0, 0),
            _backgroundScale);
        adesivo.OnClick += () => {
            
        };
        _objects.Add(adesivo); 
        adesivo.name = "adesivo";
        adesivo.IsVisible = false;

        // computador
        computador = new InteractiveObject(content.Load<Texture2D>("Backgrounds/LabVermelho/computador"), 
            new Vector2(0, 0),
            _backgroundScale);
        computador.OnClick += () => {
            
        };
        _objects.Add(computador); 
        computador.name = "computador";
        computador.IsVisible = true;
        
        // impressora
        impressora = new InteractiveObject(content.Load<Texture2D>("Backgrounds/LabVermelho/impressora"), 
            new Vector2(0, 0),
            _backgroundScale);
        impressora.OnClick += () => {
            
        };
        _objects.Add(impressora); 
        impressora.name = "impressora";
        impressora.IsVisible = true;
    }
}