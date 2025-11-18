using System.Collections.Generic;

namespace coltecube.Systems;

public static class Inventory
{
    // Itens que o jogador carrega (ex: "chavequadra", "pernadodavid")
    private static HashSet<string> _items = new HashSet<string>();

    // Eventos que aconteceram (ex: "energia_caiu", "tempo_mudou")
    private static HashSet<string> _gameFlags = new HashSet<string>();

    public static void AddItem(string itemName)
    {
        _items.Add(itemName.ToLower());
    }

    public static bool HasItem(string itemName)
    {
        return _items.Contains(itemName.ToLower());
    }

    public static void RemoveItem(string itemName)
    {
        _items.Remove(itemName.ToLower());
    }

    public static void SetFlag(string flagName)
    {
        _gameFlags.Add(flagName.ToLower());
    }

    public static bool HasFlag(string flagName)
    {
        return _gameFlags.Contains(flagName.ToLower());
    }

    public static void Clear()
    {
        _items.Clear();
        _gameFlags.Clear();
    }
}
