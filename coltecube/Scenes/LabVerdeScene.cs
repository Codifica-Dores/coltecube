using System.Collections.Generic;
using coltecube.Scenes.CorredorInfo.LabVerde;
using MonoGameLibrary;

namespace coltecube.Scenes;

public class LabVerdeScene : GeralScene
{
    public override void LoadContent()
    {
        totalWalls = 1;

        indiceFaceView = 14; 
        
        _viewTransition = new Transition();
        _viewTransition.LoadContent(Core.GraphicsDevice);
        _viewTransition.OnFadeOutComplete += SwapViewLogic; 

        // Cria e Carrega a única vista
        _views = new Dictionary<FaceView, View>();
        _views[FaceView.LabVerde] = new LabVerde();
        _views[FaceView.LabVerde].LoadContent(this.Content);
        
        // Define a Vista Inicial 
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
            Core.ChangeScene(new CorredorInfoScene(FaceView.LabVerdeVermelho)); 
            return true; 
        }
        return false;
    }
}