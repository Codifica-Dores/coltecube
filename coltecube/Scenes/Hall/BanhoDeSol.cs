using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;

namespace coltecube.Scenes.Hall;

public class BanhoDeSol : View
{
    private InteractiveObject banheira;

    public override void LoadContent(ContentManager content)
    {
        // Background
        _background = content.Load<Texture2D>("Backgrounds/BanhoDeSol/background");

        // Banheira
        banheira = new InteractiveObject(content.Load<Texture2D>("Backgrounds/BanhoDeSol/sol_na_banheira"),
            new Vector2(0, 0),
            _backgroundScale);
        _objects.Add(banheira);
        banheira.name = "sol_na_banheira";
    }
}