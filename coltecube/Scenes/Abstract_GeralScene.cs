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
	
    public enum FaceView { Escada, Quadra, Cantina, Escaninhos, BanhoDeSol, Teto // hall
	, LabAmarelo, LabVerde, Elevador, Mural // corredor info
	}
    public abstract class GeralScene : Scene
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

        public override void LoadContent()
        {
            // edit on the class
        }

        // Mudou de vista
        protected void StartViewChange(FaceView nextView)
        {
            if (_viewTransition.IsTransitioning) return; 

            _nextViewKey = nextView;
            _viewTransition.Start(); 
        }

        // Logica de ver as setas
        protected void SwapViewLogic()
        {
            _currentViewKey = _nextViewKey;
            _activeView = _views[_currentViewKey];

            if (_currentViewKey == FaceView.Teto)
            {
                // Se estiver no TETO: esconde setas Cima/Lado, mostra Baixo
                _arrowUp.IsVisible = false;
                _arrowDown.IsVisible = true;
                _arrowLeft.IsVisible = false;
                _arrowRight.IsVisible = false;
            }
            else
            {
                // Se estiver em uma PAREDE: mostra setas Cima/Lado, esconde Baixo
                _arrowUp.IsVisible = true;
                _arrowDown.IsVisible = false;
                _arrowLeft.IsVisible = true;
                _arrowRight.IsVisible = true;
            }
        }
        
        // Rotacao das vistas
        protected void RotateView(bool toRight)
        {
            if (_currentViewKey == FaceView.Teto) return;

            int currentIndex = (int)_currentViewKey;
            int totalWalls = 5; 

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

            _arrowLeft.Draw(Core.SpriteBatch);
            _arrowRight.Draw(Core.SpriteBatch);
			_arrowUp.Draw(Core.SpriteBatch);
			_arrowDown.Draw(Core.SpriteBatch);

            _viewTransition.Draw(Core.SpriteBatch, Core.GraphicsDevice.Viewport);

            base.Draw(gameTime);
        }
    }
}