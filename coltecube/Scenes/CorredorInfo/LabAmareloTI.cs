using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;
using MonoGameLibrary;

namespace coltecube.Scenes.CorredorInfo;

public class LabAmareloTI : View
{
    private InteractiveObject portaLabAmareloAberta;
    private InteractiveObject portaLabAmareloFechada;
    private InteractiveObject portaTIAberta;
    private InteractiveObject portaTIFechada;

    public override void LoadContent(ContentManager content)
    {
        // Background
        _background = content.Load<Texture2D>("Backgrounds/Lab-amarelo-TI/background");
        
		// Portas
        // Portas Amarelo
        portaLabAmareloAberta = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Lab-amarelo-TI/porta_aberta"),
            new Vector2(0, 0),
            _backgroundScale);
        portaLabAmareloAberta.OnClick += () =>
        {
            Core.ChangeScene(new LabAmareloScene());
        };
        _objects.Add(portaLabAmareloAberta);
        portaLabAmareloAberta.name = "porta_lab_amarelo_aberta";
        portaLabAmareloAberta.IsVisible = false;
        
        portaLabAmareloFechada = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Lab-amarelo-TI/porta"),
            new Vector2(0, 0),
            _backgroundScale);
        portaLabAmareloFechada.OnClick += () =>
        {
            portaLabAmareloFechada.IsVisible = false;
            portaLabAmareloAberta.IsVisible = true;
        };
        _objects.Add(portaLabAmareloFechada);
        portaLabAmareloFechada.name = "porta_lab_amarelo_fechada";
        
        // PortasTI
        portaTIAberta = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Lab-amarelo-TI/portaTI(aberta)"),
            new Vector2(0, 0),
            _backgroundScale);
        portaTIAberta.OnClick += () =>
        {
            //Core.ChangeScene(new TIScene());
        };
        _objects.Add(portaTIAberta);
        portaTIAberta.name = "porta_ti_aberta";
        portaTIAberta.IsVisible = false;
        
        portaTIFechada = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Lab-amarelo-TI/portaTI"),
            new Vector2(0, 0),
            _backgroundScale);
        portaTIFechada.OnClick += () =>
        {
            portaTIFechada.IsVisible = false;
            portaTIAberta.IsVisible = true;
        };
        _objects.Add(portaTIFechada);
        portaTIFechada.name = "porta_ti_fechada";
        
    }
}