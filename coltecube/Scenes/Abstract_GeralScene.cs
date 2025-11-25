using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;   
using coltecube.Objects;
using coltecube.Scenes.Hall;

namespace coltecube.Scenes
{
    // Enum para as vistas
    public enum FaceView
    {
        Escada,
        Quadra,
        Cantina,
        Escaninhos,
        BanhoDeSol,
        Teto, // hall 0 > 6 (6)
        Mural,
        LabAmareloTI,
        Elevador,
        LabVerdeVermelho, // corredor info 6 > 5 (11)
        ElevadorSala, // terceiro andar > 10
        Sala313, // sala 313 > 11
        LabAmarelo, // 12
        LabVermelho, //13
        LabVerde //14
    }

    public class GeralScene : Scene
    {
        // Variáveis de Gerenciamento 
        protected Dictionary<FaceView, View> _views = new Dictionary<FaceView, View>();
        protected View _activeView;
        protected FaceView _currentViewKey;
        protected FaceView _nextViewKey; 
        protected FaceView _lastWallView;

        // Transição Local 
        protected Transition _viewTransition; 

        // Setas 
        protected RotatableObject _arrowLeft;
        protected RotatableObject _arrowRight;
        protected RotatableObject _arrowUp;
        protected RotatableObject _arrowDown;
		protected int totalWalls = 4; // número de paredes (sem teto)
		protected int indiceFaceView = 0;

        public override void LoadContent()
        {
            // edit on the class
        }

		public void DefineArrows(){
			//  Carrega as Setas 
            var arrowTex = Content.Load<Texture2D>("UI/ArrowDown");
            float arrowScale = 0.015f;
			// Esquerda
			int paddingX = 80;
			int paddingY = 80;
			// relativo ao centro do background
            _arrowLeft = new RotatableObject(arrowTex, new Vector2(paddingX,_activeView._background.Height/2*_activeView._backgroundScale), arrowScale);
            _arrowLeft.Rotation = MathHelper.ToRadians(90f);
            _arrowLeft.OnClick += () => RotateView(false); 
			_arrowLeft.name = "left";

            // Direita
            _arrowRight = new RotatableObject(arrowTex, new Vector2(-paddingX+_activeView._background.Width*_activeView._backgroundScale, _activeView._background.Height*_activeView._backgroundScale/2), arrowScale);
            _arrowRight.Rotation = MathHelper.ToRadians(-90f);
            _arrowRight.OnClick += () => RotateView(true); 

            // Cima
            _arrowUp = new RotatableObject(arrowTex, new Vector2(_activeView._background.Width/2*_activeView._backgroundScale,paddingY), arrowScale);
            _arrowUp.Rotation = MathHelper.ToRadians(180f);
            _arrowUp.OnClick += () => 
            {
                _lastWallView = _currentViewKey;
                StartViewChange(FaceView.Teto);
            };

            // Baixo
            _arrowDown = new RotatableObject(arrowTex, new Vector2(500, 400), arrowScale);
            _arrowDown.Rotation = MathHelper.ToRadians(0f); // Rotação original
            _arrowDown.OnClick += () => 
            {
                StartViewChange(_lastWallView); 
            };

            _arrowDown.IsVisible = false;
            _arrowUp.IsVisible = true;
            _arrowLeft.IsVisible = true;
            _arrowRight.IsVisible = true;
			Console.WriteLine("arrow");
		}

        // Mudou de vista
        protected void StartViewChange(FaceView nextView)
        {
			Console.WriteLine("chn");
            if (_viewTransition.IsTransitioning) return; 

            _nextViewKey = nextView;
            _viewTransition.Start();
			
        }
        
        protected virtual bool TryChangeScene(bool isRight)
        {
            return false; // Por padrão, nenhuma cena muda, apenas roda a vista
        }
        
        // Rotacao das vistas
        protected void RotateView(bool toRight)
        {
            if (TryChangeScene(toRight)) return;
            
            if (_currentViewKey == FaceView.Teto) return;
			Console.WriteLine("rotating");

            int currentIndex = (int)_currentViewKey;

			Console.WriteLine("index: " + currentIndex);
			Console.WriteLine("current index: " + _currentViewKey);
            if (toRight)
            {
                currentIndex++;
                if (currentIndex >= totalWalls) currentIndex = 0;
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0) currentIndex = totalWalls - 1;
            }
			Console.WriteLine("new index: " + currentIndex);
            StartViewChange((FaceView)currentIndex);
        }

        public override void Update(GameTime gameTime)
        {
            // Transição
            _viewTransition.Update(gameTime);

            if (_viewTransition.IsTransitioning)
            {
                base.Update(gameTime);
                return;
            }

            _arrowLeft.Update(gameTime, Core.Input.Mouse);
            _arrowRight.Update(gameTime, Core.Input.Mouse);
			_arrowUp.Update(gameTime, Core.Input.Mouse);
			_arrowDown.Update(gameTime, Core.Input.Mouse);
			
            _activeView.Update(gameTime, Core.Input.Mouse);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _activeView.Draw(Core.SpriteBatch, Core.GraphicsDevice);

			// var center = new Vector2(Core.GraphicsDevice.Viewport.Width / 2f-Game1.ESPACO_LATERAL_ITEMS/2, Core.GraphicsDevice.Viewport.Height / 2f);
            _arrowLeft.Draw(Core.SpriteBatch);
            _arrowRight.Draw(Core.SpriteBatch);
			_arrowUp.Draw(Core.SpriteBatch);
			_arrowDown.Draw(Core.SpriteBatch);
            _viewTransition.Draw(Core.SpriteBatch, Core.GraphicsDevice.Viewport);

			Game1.inventory.Draw(Core.SpriteBatch);

            base.Draw(gameTime);
        }

		private void Items()
		{
			
		}
    }
}