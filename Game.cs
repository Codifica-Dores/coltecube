using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using TrabalhoFinal.Interactions;
using TrabalhoFinal.Scene;

#nullable enable

namespace TrabalhoFinal;

public class Engine : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch = null!;
    private float width { get; }
    private float height { get; }

    //
    private readonly Dictionary<string, CubeDefinition> _cubes = new(); // (cubo0:CubeDefinition, cubo1:CubeDefinition)
    private readonly Dictionary<string, Texture2D> _objectTextures = new(); // (objeto0:Imagem, objeto1:Imagem)
    private readonly Dictionary<string, IInteractiveObject> _interactiveObjects = new();
    private IInteractiveObject? _activeObject;
    private Texture2D _arrowTexture;
    private readonly Rectangle _arrowSourceRect = new Rectangle(5, 5, 17, 20);
    private string _currentCubeId = string.Empty;
    private int _currentFaceIndex;
    private int lastFace;
    private SpriteFont _uiFont = null!;
    private Rectangle areaSetaDireita, areaSetaEsquerda, areaSetaCima, areaSetaBaixo;
    private Rectangle _viewportBounds;
    bool showAreas = false;
    bool inShadowTransitionUp = true;
    private int preFace = 0;
    private string preCube = string.Empty;

    private CubeDefinition CurrentCube => _cubes[_currentCubeId];
    private CubeFaceDefinition CurrentFace => CurrentCube.GetFace(_currentFaceIndex);
    private float shadowTransition = 0f;
	// branch Menu
	private Dictionary<string, string[]> menuTextures = new Dictionary<string, string[]>();
	private bool inMenu = false; // mudar pra true
	private bool inConfiguracoes = false ; // mudar pra false
	private readonly Dictionary<string, CubeDefinition> menus = new();
	//
    
    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        width = 1920f;
        height = 1080f;
        _graphics.PreferredBackBufferWidth = (int)width;   // tamXura
        _graphics.PreferredBackBufferHeight = (int)height;   // altura

        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _viewportBounds = new Rectangle(0, 0, (int)width, (int)height);

        InitializeCubes();
		InitializeMenus();
		ConfigureAreas(); // deixa it here
        InitializeObjectTextures();

        _arrowTexture = LoadTextureOrFallback("seta.png", "arrow");

        Console.WriteLine(Directory.GetCurrentDirectory());
    }
    private void InitializeCubes()
    {
        var cubeTexturePaths = new Dictionary<string, string[]>
        {
			
            ["cubo0"] = new[]
            {
                "Escada/background.png",
                "BanhoDeSol/background.png",
                "Cantina/background.png",
                "QuadraTrancada/background.png",
                "corredor_hall.png"
            },
            ["cubo1"] = new[]
            {
                "Escada/background.png",
            }
        };

        foreach (var (cubeId, facePaths) in cubeTexturePaths)
        {
            var cubeDefinition = new CubeDefinition(cubeId);
            for (var faceIndex = 0; faceIndex < facePaths.Length; faceIndex++)
            {
                var texture = LoadTextureOrFallback($"coltecube/{facePaths[faceIndex]}", $"cubos:{cubeId}:{faceIndex}");
                cubeDefinition.AddFace(new CubeFaceDefinition(faceIndex, texture));
            }
            _cubes[cubeId] = cubeDefinition;
        }

        if (_cubes.Count > 0)
        {
            _currentCubeId = _cubes.Keys.First();
        }

        _currentFaceIndex = 0;
        lastFace = 0;
    }
	private void InitializeMenus()
	{
		menuTextures = new Dictionary<string, string[]>
		{
			["botoes"] = new[]
			{
				"botoes/bt001.png",
			},
		};

		foreach (var a in menuTextures)
		{
			var local = new CubeDefinition(a.Key);
			for (int b = 0; b < a.Value.Length; b++)
			{
				var chave =  $"menus:{a.Key}:{b}";
				var texture = LoadTextureOrFallback($"/{a.Value[b]}",chave);
				local.AddFace(new CubeFaceDefinition(b, texture));
			}
			menus[a.Key] = local;
		}
	}

    private void ConfigureAreas()
    {
        if (_cubes.TryGetValue("cubo0", out var cube0))
        {
            cube0.GetFace(2).AddArea(new SceneArea(
                "door_to_cubo1",
                new Rectangle(11, -125, 145, 340),
                new SceneAreaAction(SceneAreaActionKind.ChangeCube, targetCubeId: "cubo1", targetFaceIndex: 2)));

            cube0.GetFace(0).AddArea(new SceneArea(
                "lock",
                new Rectangle(-60, -65, 50, 80),
                new SceneAreaAction(SceneAreaActionKind.ShowObject, objectId: "lock")));
        }

        if (_cubes.TryGetValue("cubo1", out var cube1))
        {
            cube1.GetFace(0).AddArea(new SceneArea(
                "door_to_cubo0",
                new Rectangle(-167, -109, 150, 325),
                new SceneAreaAction(SceneAreaActionKind.ChangeCube, targetCubeId: "cubo0", targetFaceIndex: 0)));
        }

		// menus
		if (menus.TryGetValue("botoes", out var botoes))
		{
			botoes.GetFace(0).AddArea(new SceneArea(
				"return",
				new Rectangle((int)(width-616)/2,(int)(height-265)/2,616,265), // nã importa o tamanho
				new SceneAreaAction(SceneAreaActionKind.ConfigReturn, targetCubeId: "configuracoes", targetFaceIndex: 0)));
		}
    }

    private void InitializeObjectTextures()
    {
        var objectPaths = new Dictionary<string, string>
        {
            ["lock"] = "objects/lock_locked.png"
        };

        foreach (var (id, path) in objectPaths)
        {
            _objectTextures[id] = LoadTextureOrFallback(path, $"objetos:{id}", Color.White);
        }
    }

    private Texture2D ReloadTexture(string path)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        return Texture2D.FromStream(GraphicsDevice, stream);
    }

    private Texture2D LoadTextureOrFallback(string path, string label, Color? fallbackColor = null)
    {
        path = "data/" + path;
        try
        {
            if (File.Exists(path))
            {
                return ReloadTexture(path);
            }

            Console.WriteLine($"[{label}] Arquivo não encontrado: {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{label}] Falha ao carregar '{path}': {ex.Message}");
        }

        var color = fallbackColor ?? Color.Magenta;
        var texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new[] { color });
        return texture;
    }

    private void ConfigureInteractiveObjects()
    {
        if (_objectTextures.TryGetValue("lock", out var lockTexture))
        {
            var lockObject = new LockCombinationObject("lock", lockTexture, _arrowTexture, _arrowSourceRect, _uiFont);
             // quando CombinationChanged é invocado void func(int [] digits){}
            lockObject.CombinationChanged += digits => Console.WriteLine($"[lock] combinação: {string.Join(string.Empty, digits)}");
            _interactiveObjects[lockObject.Id] = lockObject;
        }
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        try
        {
            _uiFont = Content.Load<SpriteFont>("FonteArial");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[content] Falha ao carregar FonteArial: {ex.Message}");
            throw;
        }

        ConfigureInteractiveObjects();
    }

    protected override void Update(GameTime gameTime)
    {
        var gamePadState = GamePad.GetState(PlayerIndex.One);
        var keyboardState = Keyboard.GetState();

        if (gamePadState.Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
        { // Se está no objeto ativo e aperta para voltar
            if (_activeObject?.IsActive == true)
            {
                HideActiveObject();
            }
            else
            {
                Exit();
                return;
            }
        }

        // pega position
        EntryDevices.Update();
        Point mousePos = new Point(EntryDevices.x, EntryDevices.y);
        bool preProcess_ActivateTransition = false;

        if (EntryDevices.enter)
        {
            Console.WriteLine("Real: " + mousePos.X + ", " + mousePos.Y + "; Relativa ao centro: " + (mousePos.X - width / 2) + ", " + (mousePos.Y - height / 2));
        }

        bool objectActive = _activeObject?.IsActive == true;
        int preProcess_Face = _currentFaceIndex;
        string preProcess_Cube = _currentCubeId;

        if (objectActive) // se o objeto estiver ativo
        {
            _activeObject!.Update(gameTime);

            if (EntryDevices.mleft)
            {
                bool consumed = _activeObject.HandleClick(mousePos, MouseButton.Left);
                bool clickedNavigationArrow =
                    areaSetaDireita.Contains(mousePos) ||
                    areaSetaEsquerda.Contains(mousePos) ||
                    areaSetaCima.Contains(mousePos) ||
                    areaSetaBaixo.Contains(mousePos);

                if (!consumed && (clickedNavigationArrow || !_activeObject.Bounds.Contains(mousePos)))
                {
                    HideActiveObject();
                }
            }

            if (EntryDevices.mright)
            {
                HideActiveObject();
            }
        }
        else if (EntryDevices.mleft) // caso contrario
        {
            HandleSceneClick(mousePos);
        }

        if (preProcess_ActivateTransition)
        {
            int a = _currentFaceIndex;
            string b = _currentCubeId;
            _currentFaceIndex = preProcess_Face; _currentCubeId = preProcess_Cube;
            preFace = a; preCube = b;
            activateTransition();
        }

        if (EntryDevices.tecladoAtual.IsKeyDown(Keys.P)) showAreas = !showAreas; // press p toogle areas
		if (EntryDevices.space) inConfiguracoes = !inConfiguracoes; // press space toogle config

        base.Update(gameTime);
    }	
	
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
		if (inMenu)
		{
			Menu();
		}else if (inConfiguracoes)
		{
			Configuracoes();
		}
		else{
        	runDrawGame(gameTime);
		}
		_spriteBatch.End();
        base.Draw(gameTime);
    }
	private void Menu()
	{
		
	}
	private void Configuracoes()
	{
		float tamTelaNormal = 616f;
        float division = 1f; // 1.5f
        int tamX = (int)(tamTelaNormal / division);
        int tamY = (int)(tamTelaNormal/4*3 / division);
        int padX = -150, padY = -25; // equal and 5
        float zoom = 1f / division;       
        float rotation = 0f;
        Vector2 position = new Vector2((int)(width-616)/2,(int)(height-265)/2);
        Color color = Color.White;

		_spriteBatch.Draw(menus["botoes"].GetFace(0).Texture, position, null, color, rotation, Vector2.Zero, zoom, SpriteEffects.None, 0);
	}
	private void runDrawGame(GameTime gameTime)
	{
		//
        // 400,295
        float tamTelaNormal = 2048f;
        float division = 1.5f;
        int tamX = (int)(tamTelaNormal / division);
        int tamY = (int)(tamTelaNormal/4*3 / division);
        int padX = -150, padY = -25; // equal and 5
        float zoom = 1f / division;       
        float rotation = 0f;
        Vector2 position = Vector2.Zero;

        bool objectActive = _activeObject?.IsActive == true;

        // TODO: Add your drawing code here
        if (_currentFaceIndex == 4)
        {
            position = new Vector2(width / 2 - tamX / 2+ padX, height / 2 - tamY / 2+padY);
            if (lastFace % 2 == 1)
            {
                rotation = MathHelper.PiOver2 * ((lastFace - 1));
                position = new Vector2(width / 2 - tamX / 2 + tamX * (lastFace - lastFace % 2) / 2 , height / 2 - tamY / 2 + tamY * (lastFace - lastFace % 2) / 2+padX);

                // position = new Vector2(height / 2 - tamY / 2, width / 2 - tamX / 2);
            }
            Console.WriteLine("rot.: " + rotation);

        }
        else
        {
            position = new Vector2(width / 2 - tamX / 2+padX, height / 2 - tamY / 2+padY);
        }

        Color color = objectActive ? new Color(100, 100, 100, 255) : Color.White;

        _spriteBatch.Draw(CurrentFace.Texture, position, null, color, rotation, Vector2.Zero, zoom, SpriteEffects.None, 0);

        DrawActiveObject();

        setas(tamX, tamY,padX,padY, zoom, rotation);

        showareas(showAreas);
        transi(tamX,tamY);

       
	}

    private void DrawActiveObject()
    {
        _activeObject?.Draw(_spriteBatch);
    }

    private void HandleSceneClick(Point mousePos)
    {
        string preProcess_Cube = _currentCubeId;
        int preProcess_Face = _currentFaceIndex;
        if (HandleNavigationArrows(mousePos))
        {
            int a = _currentFaceIndex;
            string b = _currentCubeId;
            _currentFaceIndex = preProcess_Face; _currentCubeId = preProcess_Cube;
            preFace = a; preCube = b;
            activateTransition();

            return;
        }

        ProcessSceneAreas(mousePos);
    }

    private bool HandleNavigationArrows(Point mousePos)
    {
        if (areaSetaDireita.Contains(mousePos))
        {
            if (_currentFaceIndex == 4) _currentFaceIndex = lastFace;
            _currentFaceIndex = _currentFaceIndex < 3 ? _currentFaceIndex + 1 : 0;
            return true;
        }

        if (areaSetaEsquerda.Contains(mousePos))
        {
            if (_currentFaceIndex == 4) _currentFaceIndex = lastFace;
            _currentFaceIndex = _currentFaceIndex > 0 ? _currentFaceIndex - 1 : 3;
            return true;
        }

        if (areaSetaCima.Contains(mousePos))
        {
            if (_currentFaceIndex < 4)
            {
                lastFace = _currentFaceIndex;
                _currentFaceIndex = 4;
            }
            else
            {
                _currentFaceIndex = (lastFace + 2) % 4;
            }
            return true;
        }

        if (areaSetaBaixo.Contains(mousePos))
        {
            _currentFaceIndex = lastFace;
            return true;
        }

        return false;
    }

    private void ProcessSceneAreas(Point mousePos)
    {
        foreach (var area in CurrentFace.Areas)
        {
            var screenRect = TranslateAreaToScreen(area.Bounds);
            if (!screenRect.Contains(mousePos))
            {
                continue;
            }

            ExecuteAreaAction(area);
            break;
        }
    }

    private void ExecuteAreaAction(SceneArea area)
    {
        switch (area.Action.Kind)
        {
            case SceneAreaActionKind.ChangeCube:
                activateTransition();
                if (area.Action.TargetCubeId is { } targetCube && _cubes.ContainsKey(targetCube))
                {
                    _currentCubeId = targetCube;
                    _currentFaceIndex = area.Action.TargetFaceIndex ?? 0;
                    if (_currentFaceIndex != 4)
                    {
                        lastFace = _currentFaceIndex;
                    }
                }
                break;
            case SceneAreaActionKind.ShowObject:
                activateTransition();
                if (area.Action.ObjectId is { } objectId)
                {
                    ShowObject(objectId);
                }
                break;
			case SceneAreaActionKind.ConfigReturn:
				activateTransition();
				inConfiguracoes = false;
				break;
        }
    }

    private void ShowObject(string objectId)
    {
        if (_interactiveObjects.TryGetValue(objectId, out var interactive))
        {
            interactive.Show(_viewportBounds);
            _activeObject = interactive;
        }
        else
        {
            Console.WriteLine($"[objetos] Objeto '{objectId}' não encontrado.");
        }
    }

    private void HideActiveObject()
    {
        if (_activeObject == null)
        {
            return;
        }

        _activeObject.Hide();
        _activeObject = null;
    }

    private Rectangle TranslateAreaToScreen(Rectangle area)
    {
        return new Rectangle(
            (int)(width / 2 + area.X),
            (int)(height / 2 + area.Y),
            area.Width,
            area.Height
        );
    }

    private void setas(int tamX, int tamY,int lastPadX,int lastPadY, float zoom, float _)
    {
        int padX = 5,padY = 5;
        int tamY2 = 20;
        int tamX2 = 17;
        int marginX = 5;
        var rotation = 0f;
        var flip = SpriteEffects.None;
        zoom = 1f;

        var cut = _arrowSourceRect;
        
        var position = new Vector2(width / 2 + tamX / 2 - tamX2 - marginX + lastPadX, height / 2 - tamY2 / 2 + lastPadY);
        // var cut = new Rectangle(padX, padY, tamX2, tamY2);
        
        areaSetaDireita = new Rectangle(
            (int)position.X,
            (int)position.Y,
            cut.Width,
            cut.Height
        );
        _spriteBatch.Draw(_arrowTexture, position, cut, Color.White, rotation, Vector2.Zero, zoom, flip, 0);

        flip = SpriteEffects.FlipHorizontally;
        position = new Vector2(width / 2 - tamX / 2 + marginX+lastPadX, height / 2 - cut.Height / 2f+lastPadY);
        areaSetaEsquerda = new Rectangle(
            (int)position.X,
            (int)position.Y,
            cut.Width,
            cut.Height
        );
        _spriteBatch.Draw(_arrowTexture, position, cut, Color.White, rotation, Vector2.Zero, zoom, flip, 0);

        flip = SpriteEffects.None;
        rotation = -MathHelper.PiOver2;
        position = new Vector2(width / 2 - cut.Height / 2f+lastPadX, height / 2 - tamY / 2 + cut.Width + marginX+lastPadY);
        areaSetaCima = new Rectangle(
            (int)position.X,
            (int)(position.Y - cut.Width),
            cut.Height,
            cut.Width
        );
        _spriteBatch.Draw(_arrowTexture, position, cut, Color.White, rotation, Vector2.Zero, zoom, flip, 0);

        if (_currentFaceIndex == 4)
        {
            rotation = MathHelper.PiOver2;
            position = new Vector2(width / 2 + cut.Height / 2f+lastPadX, height / 2 + tamY / 2 - cut.Width - marginX+lastPadY);
            areaSetaBaixo = new Rectangle(
                (int)position.X - cut.Width - (cut.Height - cut.Width),
                (int)position.Y,
                cut.Height,
                cut.Width
            );
            _spriteBatch.Draw(_arrowTexture, position, cut, Color.White, rotation, Vector2.Zero, zoom, flip, 0);
        }
        else
        {
            areaSetaBaixo = Rectangle.Empty;
        }
    }

    private void showareas(bool show)
    {
        if (!show) return;
        // debug areas
        Texture2D rect = new Texture2D(GraphicsDevice, 1, 1);
        rect.SetData(new[] { Color.White });

        // direita
        _spriteBatch.Draw(rect, areaSetaDireita, Color.Red * 0.5f);
        // esquerda
        _spriteBatch.Draw(rect, areaSetaEsquerda, Color.Red * 0.5f);
        // cima
        _spriteBatch.Draw(rect, areaSetaCima, Color.Red * 0.5f);
        // baixo
        _spriteBatch.Draw(rect, areaSetaBaixo, Color.Red * 0.5f);

        foreach (var area in CurrentFace.Areas)
        {
            var screenRect = TranslateAreaToScreen(area.Bounds);
            _spriteBatch.Draw(rect, screenRect, Color.Blue * 0.5f);
        }
    }

    public void activateTransition()
    {
        shadowTransition = -1f;
        Console.WriteLine("activateTransition");
    }

    private void transi(int tamX, int tamY)
    {
        Texture2D rect = new Texture2D(GraphicsDevice, 1, 1);
        rect.SetData(new[] { Color.White });
        float variation = 1 / 20f;
        Console.WriteLine("transi");

        // direita
        if (shadowTransition == -1f)
        {
            shadowTransition = variation;
        }
        else if (shadowTransition > 0f)
        {
            if (shadowTransition < 1f && inShadowTransitionUp)
            {
                shadowTransition += variation;
            }

            if (shadowTransition >= 1f && inShadowTransitionUp)
            {
                inShadowTransitionUp = false;
                _currentFaceIndex = preFace;
                _currentCubeId = preCube;
                shadowTransition -= variation;
            }
            else if (shadowTransition > 0f && !inShadowTransitionUp)
            {
                shadowTransition -= variation;
            }

            if (shadowTransition <= 0f && !inShadowTransitionUp)
            {
                inShadowTransitionUp = true;
                shadowTransition = 0f;
            }
            Console.WriteLine("transi: " + shadowTransition + " " + inShadowTransitionUp);
        }
        _spriteBatch.Draw(rect, new Rectangle((int)0, (int)0, (int)width, (int)height), Color.Black * shadowTransition);
        
    }
}
