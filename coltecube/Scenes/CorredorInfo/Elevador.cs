using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;

namespace coltecube.Scenes.Hall;

public class Elevador : View
{
    // private InteractiveObject cadeado;

    public override void LoadContent(ContentManager content)
    {
        _background = content.Load<Texture2D>("Backgrounds/Escada-elevador/background");
		// _
           
        /*var cadeadoTexture = content.Load<Texture2D>("Objects/Cadeado");
        cadeado = new InteractiveObject(cadeadoTexture, new Vector2(50, 350), 1.2f);

        cadeado.OnClick += () => {
            if (Inventory.HasItem("ChaveQuadra"))
                Console.WriteLine("Entrando na quadra...");
            else
                Console.WriteLine("Está trancado!");
        };

        _objects.Add(cadeado); // Adiciona na lista de objetos da vista*/
    }
}