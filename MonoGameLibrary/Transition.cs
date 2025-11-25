using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGameLibrary;

public class Transition
{
    private Texture2D _blackPixel;
    private float _fadeAlpha = 0f;
    private float _fadeSpeed = 2.0f; // Velocidade (2.0f = 0.5 seg de fade)
    private bool _isFadingOut = true;
    
    /// <summary>
    /// Retorna true se uma transição (fade) estiver ativa.
    /// </summary>
    public bool IsTransitioning { get; private set; }
    
    /// <summary>
    /// Evento disparado quando o fade-out (escurecimento) está completo.
    /// </summary>
    public event Action OnFadeOutComplete;

    /// <summary>
    /// Cria a textura de 1x1 pixel usada para o fade.
    /// </summary>
    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        _blackPixel = new Texture2D(graphicsDevice, 1, 1);
        _blackPixel.SetData(new[] { Color.White });
    }

    /// <summary>
    /// Inicia o processo de transição (fade-out).
    /// </summary>
    public void Start()
    {
        if (IsTransitioning) return; // Já está em transição
        
        IsTransitioning = true;
        _isFadingOut = true;
        _fadeAlpha = 0f;
    }

    /// <summary>
    /// Atualiza a lógica do fade.
    /// </summary>
    public void Update(GameTime gameTime)
    {
        if (!IsTransitioning) return;

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_isFadingOut)
        {
            // Escurecendo para preto
            _fadeAlpha = Math.Min(_fadeAlpha + _fadeSpeed * delta, 1.0f);
            
            if (_fadeAlpha == 1.0f)
            {
                // A tela está preta
                _isFadingOut = false;
                
                // Avisa ao Core que é hora de trocar a cena
                OnFadeOutComplete?.Invoke();
            }
        }
        else
        {
            // Clareando (voltando da transição)
            _fadeAlpha = Math.Max(_fadeAlpha - _fadeSpeed * delta, 0.0f);
            if (_fadeAlpha == 0.0f)
            {
                IsTransitioning = false; // Transição terminou
            }
        }
    }

    /// <summary>
    /// Desenha o retângulo preto com a transparência (alpha) atual.
    /// </summary>
    public void Draw(SpriteBatch spriteBatch, Viewport viewport)
    {
        if (_fadeAlpha > 0)
        {
            // Desenha o fade por cima de tudo
            spriteBatch.Draw(_blackPixel, viewport.Bounds, Color.Black * _fadeAlpha);
        }
    }
}
