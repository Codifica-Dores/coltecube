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
    public enum HallView { Escada, Quadra, Cantina, Escaninhos, BanhoDeSol, Teto }

    public class HallScene : Scene
    {
        // Variáveis de Gerenciamento 
        private Dictionary<HallView, View> _views = new Dictionary<HallView, View>();
        private View _activeView;
        private HallView _currentViewKey;
        private HallView _nextViewKey; 
        private HallView _lastWallView;

        // Transição Local 
        private Transition _viewTransition; 

        // Setas 
        private RotatableObject _arrowLeft;
        private RotatableObject _arrowRight;
        private RotatableObject _arrowUp;
        private RotatableObject _arrowDown;

        public override void LoadContent()
        {
            // Configura a Transição da Vista
            _viewTransition = new Transition();
            _viewTransition.LoadContent(Core.GraphicsDevice);
            _viewTransition.OnFadeOutComplete += SwapViewLogic; 

            // Cria e Carrega todas as Vistas
            _views[HallView.Escada] = new Escada();
            _views[HallView.Escada].LoadContent(this.Content);

            _views[HallView.Quadra] = new Quadra();
            _views[HallView.Quadra].LoadContent(this.Content);
            
            _views[HallView.Cantina] = new Cantina();
            _views[HallView.Cantina].LoadContent(this.Content);
            
            _views[HallView.Escaninhos] = new Escaninhos();
            _views[HallView.Escaninhos].LoadContent(this.Content);
            
            _views[HallView.BanhoDeSol] = new BanhoDeSol();
            _views[HallView.BanhoDeSol].LoadContent(this.Content);
            
            _views[HallView.Teto] = new Teto();
            _views[HallView.Teto].LoadContent(this.Content);
            
            // Define a Vista Inicial
            _currentViewKey = HallView.Escada;
            _activeView = _views[_currentViewKey];

            //  Carrega as Setas 
            var arrowTex = Content.Load<Texture2D>("UI/ArrowDown");
            float arrowScale = 0.015f;
            
            // Esquerda
            _arrowLeft = new RotatableObject(arrowTex, new Vector2(400, 300), arrowScale);
            _arrowLeft.Rotation = MathHelper.ToRadians(90f);
            _arrowLeft.OnClick += () => RotateView(false); 

            // Direita
            _arrowRight = new RotatableObject(arrowTex, new Vector2(500, 300), arrowScale);
            _arrowRight.Rotation = MathHelper.ToRadians(-90f);
            _arrowRight.OnClick += () => RotateView(true); 

            // Cima
            _arrowUp = new RotatableObject(arrowTex, new Vector2(500, 200), arrowScale);
            _arrowUp.Rotation = MathHelper.ToRadians(180f);
            _arrowUp.OnClick += () => 
            {
                _lastWallView = _currentViewKey;
                StartViewChange(HallView.Teto);
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
        }

        // Mudou de vista
        private void StartViewChange(HallView nextView)
        {
            if (_viewTransition.IsTransitioning) return; 

            _nextViewKey = nextView;
            _viewTransition.Start(); 
        }

        // Logica de ver as setas
        private void SwapViewLogic()
        {
            _currentViewKey = _nextViewKey;
            _activeView = _views[_currentViewKey];

            if (_currentViewKey == HallView.Teto)
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
        private void RotateView(bool toRight)
        {
            if (_currentViewKey == HallView.Teto) return;

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
            StartViewChange((HallView)currentIndex);
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

            _activeView.Update(gameTime, Core.Input.Mouse);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _activeView.Draw(Core.SpriteBatch, Core.GraphicsDevice);

            _arrowLeft.Draw(Core.SpriteBatch);
            _arrowRight.Draw(Core.SpriteBatch);

            _viewTransition.Draw(Core.SpriteBatch, Core.GraphicsDevice.Viewport);

            base.Draw(gameTime);
        }
    }
}