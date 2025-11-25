using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;

namespace coltecube.Scenes.TerceiroAndar;

public class Sala313 : View
{
    private InteractiveObject papeis;

    public override void LoadContent(ContentManager content)
    {
        //Background
        _background = content.Load<Texture2D>("Backgrounds/313/comunismo");
         
        // Papeis
        papeis = new InteractiveObject(content.Load<Texture2D>("Backgrounds/313/papeis"), 
            new Vector2(0, 0),
            _backgroundScale);
        papeis.OnClick += () => {
            
        };
        _objects.Add(papeis); 
        papeis.name = "papeis";
    }
}
