using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;
using MonoGameLibrary;

namespace coltecube.Scenes.Hall;

public class Elevador : View
{
    private InteractiveObject elevadorAberto;

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
    }
}