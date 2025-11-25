using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using coltecube.Objects; 
using System;

namespace coltecube.Systems;

public class Inventory : MultipleObject
{
    // Itens que o jogador carrega (ex: "chavequadra", "pernadodavid")
    private HashSet<string> _items = new HashSet<string>();

    // Eventos que aconteceram (ex: "energia_caiu", "tempo_mudou")
    private HashSet<string> _gameFlags = new HashSet<string>();

	// Construtor
	public Inventory(Texture2D texture, Vector2 position, float scale = 1.0f)
		: base(new List<InteractiveObject>(), texture, position, scale) 
	{
		Rotation = 0f;
	}

    public void AddItem(InteractiveObject item)
	{
		objects.Add(item);
		_items.Add(item.name);
	}

    public bool HasItem(InteractiveObject item)
    {
        return _items.Contains(item.name);
    }

    public void RemoveItem(InteractiveObject item)
    {
        objects.Remove(item);
		_items.Remove(item.name);
    }

    // public void SetFlag(string flagName)
    // {
    //     _gameFlags.Add(flagName.ToLower());
    // }

    // public bool HasFlag(string flagName)
    // {
    //     return _gameFlags.Contains(flagName.ToLower());
    // }

    public void Clear()
    {
        _items.Clear();
        _gameFlags.Clear();
		objects.Clear();
    }

	public void Charge()
	{
		_items = new HashSet<string>();
		_gameFlags = new HashSet<string>();
		//
	}	

	public override void Draw(SpriteBatch spriteBatch)
	{
		
		// locals
		int qtColls = 2;
		int qtLines = 8;
		float padding = 10f;
		int difY = -Game1.NATIVE_HEIGHT/2+(int)(qtLines/2*(padding+Texture.Height*base.Scale)/2);
		int difX = (int)(200+2048*base.Scale*qtColls);
		for (int i = 0; i < qtColls; i++)
		{
			for (int j = 0; j < qtLines; j++)
			{
				// Desenha o fundo do inventário
				Vector2 pos = new Vector2(
						Position.X + i * (Texture.Width * Scale)+difX,
						Position.Y + j * (Texture.Height * Scale + padding)+difY
					);

				spriteBatch.Draw(
					Texture,
					pos,
					null,
					Color.White,
					0f,
					Vector2.Zero,
					Scale,
					SpriteEffects.None,
					0f
				);
			}
		}
		//items -- final
		for (int i = 0; i < qtColls; i++)
		{
			for (int j = 0; j < qtLines; j++)
			{
				if (i*qtLines+j >= objects.Count) return;

				Vector2 pos = new Vector2(
						Position.X + i * (Texture.Width * Scale)+difX,
						Position.Y + j * (Texture.Height * Scale + padding)+difY
					);
				objects[i*qtLines+j].Position = pos;
				objects[i*qtLines+j].Draw(spriteBatch);
			}
		}	
	}

	
}
