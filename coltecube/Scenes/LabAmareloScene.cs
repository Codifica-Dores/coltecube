using System.Collections.Generic;
using coltecube.Scenes.CorredorInfo.LabAmarelo;
using coltecube.Scenes.CorredorInfo.LabVermelho;
using MonoGameLibrary;

namespace coltecube.Scenes;

public class LabAmareloScene : GeralScene
{
    public override void LoadContent()
    {
        totalWalls = 1;

        indiceFaceView = 12; 
        
        _viewTransition = new Transition();
        _viewTransition.LoadContent(Core.GraphicsDevice);
        _viewTransition.OnFadeOutComplete += SwapViewLogic; 

        // Cria e Carrega a única vista
        _views = new Dictionary<FaceView, View>();
        _views[FaceView.LabAmarelo] = new LabAmarelo();
        _views[FaceView.LabAmarelo].LoadContent(this.Content);
        
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
        _arrowRight.IsVisible = false;
        
        // A seta esquerda serve para SAIR da sala
        _arrowLeft.IsVisible = true;
    }
    
    protected override bool TryChangeScene(bool isRight)
    {
        if ((int)_currentViewKey == 0 && !isRight)
        {
            Core.ChangeScene(new CorredorInfoScene(FaceView.LabAmareloTI)); 
            return true; 
        }
        return false;
    }
}