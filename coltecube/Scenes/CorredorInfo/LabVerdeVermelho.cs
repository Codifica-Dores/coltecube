using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;
using MonoGameLibrary;

namespace coltecube.Scenes.CorredorInfo;

public class LabVerdeVermelho : View
{
    private InteractiveObject portaLabVerdeAberta;
    private InteractiveObject portaLabVerdeFechada;
    private InteractiveObject portaLabVermelhoAberta;
    private InteractiveObject portaLabVermelhoFechada;
    private InteractiveObject peDeCabra;
    private InteractiveObject escaninhoAberto;
    private InteractiveObject escaninhoFechado;

    public override void LoadContent(ContentManager content)
    {
        // Background
        _background = content.Load<Texture2D>("Backgrounds/Labs-vermelho-verde/backgound");
        
        // Portas
        // Portas Verde
        portaLabVerdeAberta = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Labs-vermelho-verde/porta_lab_verde_aberta"),
            new Vector2(0, 0),
            _backgroundScale);
        portaLabVerdeAberta.OnClick += () =>
        {
            Core.ChangeScene(new LabVerdeScene());
        };
        _objects.Add(portaLabVerdeAberta);
        portaLabVerdeAberta.name = "porta_lab_verde_aberta";
        portaLabVerdeAberta.IsVisible = false;
        
        portaLabVerdeFechada = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Labs-vermelho-verde/porta_lab_verde"),
            new Vector2(0, 0),
            _backgroundScale);
        portaLabVerdeFechada.OnClick += () =>
        {
            portaLabVerdeFechada.IsVisible = false;
            portaLabVerdeAberta.IsVisible = true;
        };
        _objects.Add(portaLabVerdeFechada);
        portaLabVerdeFechada.name = "porta_lab_verde_fechada";
        
        // PortasLabVermelho
        portaLabVermelhoAberta = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Labs-vermelho-verde/porta_lab_vermelho_aberta"),
            new Vector2(0, 0),
            _backgroundScale);
        portaLabVermelhoAberta.OnClick += () =>
        {
            Core.ChangeScene(new LabVermelhoScene());
        };
        _objects.Add(portaLabVermelhoAberta);
        portaLabVermelhoAberta.name = "porta_lab_vermelho_aberta";
        portaLabVermelhoAberta.IsVisible = false;
        
        portaLabVermelhoFechada = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Labs-vermelho-verde/porta_lab_vermelho"),
            new Vector2(0, 0),
            _backgroundScale);
        portaLabVermelhoFechada.OnClick += () =>
        {
            portaLabVermelhoFechada.IsVisible = false;
            portaLabVermelhoAberta.IsVisible = true;
        };
        _objects.Add(portaLabVermelhoFechada);
        portaLabVermelhoFechada.name = "porta_lab_vermelho_fechada";
        
        //Escaninho Aberto
        escaninhoAberto = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Labs-vermelho-verde/escaninho_aberto"),
            new Vector2(0, 0),
            _backgroundScale);
        escaninhoAberto.OnClick += () =>
        {
        
        };
        _objects.Add(escaninhoAberto);
        escaninhoAberto.name = "escaninho_aberto";
        escaninhoAberto.IsVisible = false;
        
        //Pe de cabra
        peDeCabra = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Labs-vermelho-verde/pe_de_cabra"),
            new Vector2(0, 0),
            _backgroundScale);
        peDeCabra.OnClick += () =>
        {
        
        };
        _objects.Add(peDeCabra);
        peDeCabra.name = "pe_de_cabra";
        peDeCabra.IsVisible = true;
        
        //Escaninho Fechado
        escaninhoFechado = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Labs-vermelho-verde/escaninho"),
            new Vector2(0, 0),
            _backgroundScale);
        escaninhoFechado.OnClick += () =>
        {
            escaninhoAberto.IsVisible = true;
            escaninhoFechado.IsVisible = false;
        };
        _objects.Add(escaninhoFechado);
        escaninhoFechado.name = "escaninho_fechado";
        escaninhoFechado.IsVisible = true;
    }
    
    
}