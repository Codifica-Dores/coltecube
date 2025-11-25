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
    public class HallScene : GeralScene
    {

        public override void LoadContent()
        {
			totalWalls = 5;
            // Configura a Transição da Vista
            _viewTransition = new Transition();
            _viewTransition.LoadContent(Core.GraphicsDevice);
            _viewTransition.OnFadeOutComplete += SwapViewLogic; 

            // Cria e Carrega todas as Vistas
			var esc = new Escada();
            _views[FaceView.Escada] = new Escada();
            _views[FaceView.Escada].LoadContent(this.Content);

            _views[FaceView.Quadra] = new Quadra();
            _views[FaceView.Quadra].LoadContent(this.Content);
            
            _views[FaceView.Cantina] = new Cantina();
			_views[FaceView.Cantina].LoadContent(this.Content);
            
            _views[FaceView.Escaninhos] = new Escaninhos();
            _views[FaceView.Escaninhos].LoadContent(this.Content);
            
            _views[FaceView.BanhoDeSol] = new BanhoDeSol();
            _views[FaceView.BanhoDeSol].LoadContent(this.Content);
            
            _views[FaceView.Teto] = new Teto();
            _views[FaceView.Teto].LoadContent(this.Content);
            
            // Define a Vista Inicial
            _currentViewKey = 0; // sempre 0
            _activeView = _views[_currentViewKey+indiceFaceView];
            
            DefineArrows();
            SwapViewLogic();
        }
        
        // Logica de ver as setas
        protected void SwapViewLogic()
        {
            _currentViewKey = _nextViewKey;
            _activeView = _views[_currentViewKey+indiceFaceView];

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
    }
}