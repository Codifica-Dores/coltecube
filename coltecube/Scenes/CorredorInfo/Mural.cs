using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;

namespace coltecube.Scenes.CorredorInfo;

public class Mural : View
{
    private InteractiveObject mural;

    public override void LoadContent(ContentManager content)
    {
        // Background
        _background = content.Load<Texture2D>("Backgrounds/Mural/background");
           
        // Mural
        mural = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Mural/mural"), 
            new Vector2(0, 0), 
            _backgroundScale);
        _objects.Add(mural); 
    }
}