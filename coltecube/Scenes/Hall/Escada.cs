using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;

namespace coltecube.Scenes.Hall;

    public class Escada : View
    {
		private InteractiveObject escada;
        private InteractiveObject ampulheta;

        public override void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("Backgrounds/Hall_Escada");
			escada = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Escada/escada"), new Vector2(300, 300), 1.0f);
			_objects.Add(escada);
			// _things.Add((content.Load<Texture2D>("Backgrounds/Escada/escada"), new Vector2(100, 100)));
			// _things.Add((content.Load<Texture2D>("Backgrounds/Escada/ampulheta"), new Vector2(100, 300)) );
            // _backgroundScale = 0.32f;
           
            /*var ampulhetaTexture = content.Load<Texture2D>("Objects/Ampulheta");
            ampulheta = new InteractiveObject(ampulhetaTexture, new Vector2(50, 350), 1.2f);
            
            ampulheta.OnClick += () => {
                if (Inventory.HasItem("ChaveQuadra"))
                    Console.WriteLine("Entrando na quadra...");
                else
                    Console.WriteLine("Está trancado!");
            };
            
            _objects.Add(ampulheta); // Adiciona na lista de objetos da vista*/
        }
    }
