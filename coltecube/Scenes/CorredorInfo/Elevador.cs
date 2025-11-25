using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;
using MonoGameLibrary;

namespace coltecube.Scenes.CorredorInfo;

public class Elevador : View
{
    private InteractiveObject elevadorAberto;
    private InteractiveObject elevadorFechado;
    private ClickableZone escada;

    public override void LoadContent(ContentManager content)
    {
        // Background
        _background = content.Load<Texture2D>("Backgrounds/Escada-elevador/background");
        
		// Elevadores
        elevadorAberto = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Escada-elevador/elevador(aberto)"), 
            new Vector2(0, 0), 
            _backgroundScale);
        elevadorAberto.OnClick += () => {
            Core.ChangeScene(new TerceiroAndarScene());
        };
        _objects.Add(elevadorAberto); 
        elevadorAberto.name = "elevadorAberto";
        elevadorAberto.IsVisible = false;
        
        
        elevadorFechado = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Escada-elevador/elevador"), 
            new Vector2(0, 0), 
            _backgroundScale);
        elevadorFechado.OnClick += () => {
            elevadorFechado.IsVisible = false;
            elevadorAberto.IsVisible = true;
        };
        _objects.Add(elevadorFechado); 
        elevadorFechado.name = "elevadorFechado";
        elevadorFechado.IsVisible = true;
        
        // Escada
        var escada = new ClickableZone(new Rectangle(150, 100, 500, 600));
        escada.name = "escada";

        escada.OnClick += () => {
            Core.ChangeScene(new HallScene());
        };

        _objects.Add(escada);
    }
}