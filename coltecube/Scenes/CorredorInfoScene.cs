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

    public class CorredorInfo : GeralScene
    {

        public override void LoadContent()
        {
            // Configura a Transição da Vista
            _viewTransition = new Transition();
            _viewTransition.LoadContent(Core.GraphicsDevice);
            _viewTransition.OnFadeOutComplete += SwapViewLogic; 

            // Cria e Carrega todas as Vistas
            _views[FaceView.Mural] = new Mural();
            _views[FaceView.Mural].LoadContent(this.Content);

            _views[FaceView.LabAmarelo] = new LabAmarelo();
            _views[FaceView.LabAmarelo].LoadContent(this.Content);
            
            _views[FaceView.LabVerde] = new LabVerde();
            _views[FaceView.LabVerde].LoadContent(this.Content);
            
            _views[FaceView.Elevador] = new Elevador();
            _views[FaceView.Elevador].LoadContent(this.Content);
            
            // _views[FaceView.BanhoDeSol] = new BanhoDeSol();
            // _views[FaceView.BanhoDeSol].LoadContent(this.Content);
            
            _views[FaceView.Teto] = new Teto();
            _views[FaceView.Teto].LoadContent(this.Content);
            
            // Define a Vista Inicial
            _currentViewKey = FaceView.Escada;
            _activeView = _views[_currentViewKey];

            //  Carrega as Setas 
            var arrowTex = Content.Load<Texture2D>("UI/ArrowDown");
            float arrowScale = 0.015f;
            
            // Esquerda
			int paddingX = 80;
			int paddingY = 80;
            _arrowLeft = new RotatableObject(arrowTex, new Vector2(0+paddingX,  _activeView._background.Height/2*_activeView._backgroundScale), arrowScale);
            _arrowLeft.Rotation = MathHelper.ToRadians(90f);
            _arrowLeft.OnClick += () => RotateView(false); 

            // Direita
            _arrowRight = new RotatableObject(arrowTex, new Vector2((_activeView._background.Width-Game1.ESPACO_LATERAL_ITEMS)*_activeView._backgroundScale,  _activeView._background.Height/2*_activeView._backgroundScale), arrowScale);
            _arrowRight.Rotation = MathHelper.ToRadians(-90f);
            _arrowRight.OnClick += () => RotateView(true); 

            // Cima
            _arrowUp = new RotatableObject(arrowTex, new Vector2((_activeView._background.Width/2)*_activeView._backgroundScale, paddingY), arrowScale);
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
        }
    }
}