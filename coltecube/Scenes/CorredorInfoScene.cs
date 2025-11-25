using System.Collections.Generic;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;   
using coltecube.Objects;
using coltecube.Scenes.CorredorInfo;

namespace coltecube.Scenes;

public class CorredorInfoScene : GeralScene
{
    private FaceView _vistaInicial;

    // Construtor
    public CorredorInfoScene(FaceView vistaInicial = FaceView.Mural) 
    { 
        _vistaInicial = vistaInicial;
    }

    public override void LoadContent()
    {
        totalWalls = 4;
        indiceFaceView = 6; // O offset do Enum (Mural = 6)
        
        _viewTransition = new Transition();
        _viewTransition.LoadContent(Core.GraphicsDevice);
        _viewTransition.OnFadeOutComplete += SwapViewLogic; 

        // Carrega as Vistas
        _views = new Dictionary<FaceView, View>();
        _views[FaceView.Mural] = new Mural();
        _views[FaceView.Mural].LoadContent(this.Content);

        _views[FaceView.LabAmareloTI] = new LabAmareloTI();
        _views[FaceView.LabAmareloTI].LoadContent(this.Content);
        
        _views[FaceView.LabVerdeVermelho] = new LabVerdeVermelho();
        _views[FaceView.LabVerdeVermelho].LoadContent(this.Content);
        
        _views[FaceView.Elevador] = new Elevador();
        _views[FaceView.Elevador].LoadContent(this.Content);
        
        // Se a vista passada for válida para essa sala, usamos ela. Se não, usa 0.
        int indiceCalculado = (int)_vistaInicial - indiceFaceView;
        
        if (indiceCalculado >= 0 && indiceCalculado < totalWalls)
        {
            _currentViewKey = (FaceView)indiceCalculado;
        }
        else
        {
            _currentViewKey = 0; // Padrão se der erro
        }
        
        _nextViewKey = _currentViewKey;

        _activeView = _views[_currentViewKey + indiceFaceView];
        
        DefineArrows();
        SwapViewLogic();
    }
    
    protected void SwapViewLogic()
    {
        _currentViewKey = _nextViewKey;
        if (_views.ContainsKey(_currentViewKey + indiceFaceView))
        {
            _activeView = _views[_currentViewKey + indiceFaceView];
        }
        
        _arrowUp.IsVisible = false;
        _arrowDown.IsVisible = false;
        _arrowLeft.IsVisible = true;
        _arrowRight.IsVisible = true;
    }
}