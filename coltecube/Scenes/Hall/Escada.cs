using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects;
using coltecube.Systems; 
using System;

namespace coltecube.Scenes.Hall;

    public class Escada : View
    {
		public InteractiveObject escada;
        public InteractiveObject ampulheta;

        public override void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("Backgrounds/Hall_Escada");
			escada = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Escada/escada"), new Vector2(
                0,0               
            ), _backgroundScale);

			_objects.Add(escada);
			escada.name = "escada";

			//
			escada.Bounds = new Rectangle(
				(int)(_background.Width/2*_backgroundScale),
				(int)(_background.Height/2*_backgroundScale),
				(int)(300),
				(int)(300)
			);
		}
    }
