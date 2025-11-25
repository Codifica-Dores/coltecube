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
		public InteractiveObject adesivo_teste; // teste

        public override void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("Backgrounds/Escada/background");
			this.loadObjects(content);
		}
		private void loadObjects(ContentManager content){
			escada = new InteractiveObject(content.Load<Texture2D>("Backgrounds/Escada/escada"), new Vector2(
                0,0               
            ), _backgroundScale);

			_objects.Add(escada);
			escada.name = "escada";

			escada.Bounds = new Rectangle(
				(int)(_background.Width/2*_backgroundScale),
				(int)(_background.Height/2*_backgroundScale),
				(int)(300),
				(int)(300)
			);
			// adesivo
			adesivo_teste = new InteractiveObject(content.Load<Texture2D>("Backgrounds/adesivo"), new Vector2(
				0,0               
			), 0.1f);
			_objects.Add(adesivo_teste);
			adesivo_teste.name = "adesivo_teste";
			int vx = (int)(30/adesivo_teste.Scale), vy = (int)(30/adesivo_teste.Scale);
			int dx = (int)(15/adesivo_teste.Scale), dy = (int)(15/adesivo_teste.Scale);
			adesivo_teste.Bounds = new Rectangle(
				(int)(adesivo_teste.Position.X+_background.Width/2*adesivo_teste.Scale-(dx)*adesivo_teste.Scale),
				(int)(adesivo_teste.Position.Y+_background.Height/2*adesivo_teste.Scale-(dy)*adesivo_teste.Scale),
				(int)(vx*adesivo_teste.Scale),
				(int)(vy*adesivo_teste.Scale)
			);
			adesivo_teste.OnClick += () => {
				Console.WriteLine("Adesivo clicado!");
				if(!Game1.inventory.HasItem(adesivo_teste)){
					Game1.inventory.AddItem(adesivo_teste);
					// _objects.Remove(adesivo_teste);
					Console.WriteLine("Adesivo adicionado ao inventário!");
				}else{
					Console.WriteLine("Você já pegou esse adesivo!");
				}
			};
		}
		
    }
