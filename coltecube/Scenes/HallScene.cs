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
    // public enum FaceView { Escada, Quadra, Cantina, Escaninhos, BanhoDeSol, Teto }

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
            _views[FaceView.Escada] = esc;
            _views[FaceView.Escada].LoadContent(this.Content);

            esc.escada.OnClick += () =>{
                Core.ChangeScene(new CorredorInfoScene());
            };

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
        }
    }
}