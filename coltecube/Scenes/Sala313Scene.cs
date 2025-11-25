using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;   
using coltecube.Objects;
using coltecube.Scenes.TerceiroAndar;

namespace coltecube.Scenes;
 
public class Sala313Scene : GeralScene
{
    public override void LoadContent()
    {
        totalWalls = 1;

        indiceFaceView = 11; // Offset para chegar no Enum Sala313
        
        _viewTransition = new Transition();
        _viewTransition.LoadContent(Core.GraphicsDevice);
        _viewTransition.OnFadeOutComplete += SwapViewLogic; 

        // Cria e Carrega a única vista
        _views = new Dictionary<FaceView, View>();
        _views[FaceView.Sala313] = new Sala313();
        _views[FaceView.Sala313].LoadContent(this.Content);
        
        // Define a Vista Inicial (0 + 11 = 11)
        _currentViewKey = 0; 
        _activeView = _views[_currentViewKey + indiceFaceView]; 

        DefineArrows();
        SwapViewLogic();
    }
    
    protected void SwapViewLogic()
    {
        _activeView = _views[(FaceView)((int)_nextViewKey + indiceFaceView)];
        
        _currentViewKey = _nextViewKey;

        // Configura setas
        _arrowUp.IsVisible = false;
        _arrowDown.IsVisible = false;
        _arrowLeft.IsVisible = false;
        
        // A seta direita serve para SAIR da sala
        _arrowRight.IsVisible = true;
    }
    
    protected override bool TryChangeScene(bool isRight)
    {
        if ((int)_currentViewKey == 0 && isRight)
        {
            Core.ChangeScene(new TerceiroAndarScene()); 
            return true; 
        }
        return false;
    }
}