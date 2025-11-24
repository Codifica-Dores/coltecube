using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;   
using coltecube.Objects;
using coltecube.Scenes.Hall;

namespace coltecube.Scenes;

public class CorredorInfoScene : GeralScene
{
    public override void LoadContent()
    {
        // Configura a Transição da Vista
		indiceFaceView = 6;
		// _nextViewKey = FaceView.Mural;
        _viewTransition = new Transition();
        _viewTransition.LoadContent(Core.GraphicsDevice);
        _viewTransition.OnFadeOutComplete += SwapViewLogic; 

        // Cria e Carrega todas as Vistas
		_views = new Dictionary<FaceView, View>();
        _views[FaceView.Mural] = new Mural();
        _views[FaceView.Mural].LoadContent(this.Content);

        _views[FaceView.LabAmarelo] = new LabAmarelo();
        _views[FaceView.LabAmarelo].LoadContent(this.Content);
        
        _views[FaceView.LabVerde] = new LabVerde();
        _views[FaceView.LabVerde].LoadContent(this.Content);
        
        _views[FaceView.Elevador] = new Elevador();
        _views[FaceView.Elevador].LoadContent(this.Content);
        
        // Define a Vista Inicial
        _currentViewKey = 0; // sempre 0
        _activeView = _views[_currentViewKey+indiceFaceView];

        DefineArrows();
    }
}
