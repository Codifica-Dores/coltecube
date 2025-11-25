using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;
using MonoGameLibrary;

namespace coltecube.Scenes.TerceiroAndar;

public class ElevadorSala : View
{
    private InteractiveObject elevadorFechado;
    private InteractiveObject elevadorAberto;
    private InteractiveObject portaFechada;
    private InteractiveObject portaAberta;
    private InteractiveObject calendario2025;
    private InteractiveObject calendario1999;
    
    public override void LoadContent(ContentManager content)
    {
        // Background
        _background = content.Load<Texture2D>("Backgrounds/Elevador-sala/backgound");

        // Portas
        portaAberta = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Elevador-sala/porta_aberta"),
            new Vector2(0, 0),
            _backgroundScale);
        portaAberta.OnClick += () =>
        {
            Core.ChangeScene(new Sala313Scene());
        };
        _objects.Add(portaAberta);
        portaAberta.name = "porta_aberta";
        portaAberta.IsVisible = false;
        
        portaFechada = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Elevador-sala/porta"),
            new Vector2(0, 0),
            _backgroundScale);
        portaFechada.OnClick += () =>
        {
            portaFechada.IsVisible = false;
            portaAberta.IsVisible = true;
        };
        _objects.Add(portaFechada);
        portaFechada.name = "porta_fechada";
        
        //Elevadores
        elevadorAberto = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Elevador-sala/elevador(aberto)"),
            new Vector2(0, 0),
            _backgroundScale);
        elevadorAberto.OnClick += () =>
        {
            Core.ChangeScene(new CorredorInfoScene(FaceView.Elevador));
        };
        _objects.Add(elevadorAberto);
        elevadorAberto.name = "elevador_aberto";
        elevadorAberto.IsVisible = false;
        
        elevadorFechado = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Elevador-sala/elevador"),
            new Vector2(0, 0),
            _backgroundScale);
        _objects.Add(elevadorFechado);
        elevadorFechado.OnClick += () =>
        {
            elevadorFechado.IsVisible = false;
            elevadorAberto.IsVisible = true;
        };
        elevadorFechado.name = "elevador_fechado";
        
        
        //Calendarios
        calendario2025 = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Elevador-sala/Calendario (2025)"),
            new Vector2(0, 0),
            _backgroundScale);
        _objects.Add(calendario2025);
        calendario2025.name = "calendario_2025";
        
        calendario1999 = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Elevador-sala/Calendario (1999)"),
            new Vector2(0, 0),
            _backgroundScale);
        _objects.Add(calendario1999);
        calendario1999.name = "calendario_1999";
        calendario1999.IsVisible = false;
    }
}
