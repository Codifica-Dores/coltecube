using coltecube.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coltecube.Scenes.CorredorInfo.LabVerde;

public class LabVerde : View
{
    private InteractiveObject computador;
    private InteractiveObject controle;

    public override void LoadContent(ContentManager content)
    {
        // Background
        _background = content.Load<Texture2D>("Backgrounds/LabVerde/background");
        
        // computador
        computador = new InteractiveObject(content.Load<Texture2D>("Backgrounds/LabVerde/computator"), 
            new Vector2(0, 0),
            _backgroundScale);
        computador.OnClick += () => {
            
        };
        _objects.Add(computador); 
        computador.name = "computador";
        computador.IsVisible = true;
        
        // controle
        controle = new InteractiveObject(content.Load<Texture2D>("Backgrounds/LabVerde/controle"), 
            new Vector2(0, 0),
            _backgroundScale);
        controle.OnClick += () => {
            
        };
        _objects.Add(controle); 
        controle.name = "controle";
        controle.IsVisible = true;
    }
}