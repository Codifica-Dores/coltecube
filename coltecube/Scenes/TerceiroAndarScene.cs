using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;   
using coltecube.Objects;
using coltecube.Scenes.TerceiroAndar;

namespace coltecube.Scenes;

public class TerceiroAndarScene : GeralScene
{
    public override void LoadContent()
    {
        // Configura a Transição da Vista
        indiceFaceView = 10;
        // _nextViewKey = FaceView.Mural;
        _viewTransition = new Transition();
        _viewTransition.LoadContent(Core.GraphicsDevice);
        _viewTransition.OnFadeOutComplete += SwapViewLogic; 

        // Cria e Carrega todas as Vistas
        _views = new Dictionary<FaceView, View>();
        _views[FaceView.ElevadorSala] = new ElevadorSala();
        _views[FaceView.ElevadorSala].LoadContent(this.Content);
        
        // Define a Vista Inicial
        _currentViewKey = 0; // sempre 0
        _activeView = _views[_currentViewKey+indiceFaceView];

        DefineArrows();
        SwapViewLogic();
    }
    
    protected void SwapViewLogic()
    {
        _currentViewKey = _nextViewKey;
        _activeView = _views[_currentViewKey+indiceFaceView];
        
        _arrowUp.IsVisible = false;
        _arrowDown.IsVisible = false;
        _arrowLeft.IsVisible = false;
        _arrowRight.IsVisible = false;
    }
}


