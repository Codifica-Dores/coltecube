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
            new Vector2(Game1.NATIVE_WIDTH/2-10, Game1.NATIVE_HEIGHT/2-10),
            0.07f);
        // adesivo.Position.X = ;
        // adesivo.Position.Y = ;
        _objects.Add(adesivo); 
        adesivo.name = "adesivo";
        adesivo.OnClick += () => {
            if (!Game1.inventory.HasItem(adesivo))
            {
                // Core.ShowMessage("Já peguei o adesivo.");
                adesivo.Scale = 0.1f;
                Game1.inventory.AddItem(adesivo);
                _objects.Remove(adesivo);
                for (int i = _objects.Count - 1; i >= 0; i--)
                {
                    if (_objects[i].name == adesivo.name)
                    {
                        _objects.RemoveAt(i);
                    }
                }
            }
            else
            {
                // Core.ShowMessage("Você pegou o adesivo.");
            }
        };
        adesivo.IsVisible = true;

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