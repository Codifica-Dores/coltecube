using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TrabalhoFinal.Scene;

public sealed class CubeDefinition // um cubo com suas faces
{
    private readonly Dictionary<int, CubeFaceDefinition> _faces = new(); // key: face index (0-4)
 
    public CubeDefinition(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public IEnumerable<CubeFaceDefinition> Faces => _faces.Values; // todas as faces do cubo

    public CubeFaceDefinition GetFace(int index) => _faces[index];

    public void AddFace(CubeFaceDefinition face) => _faces[face.Index] = face;
}

public sealed class CubeFaceDefinition
{
    private readonly List<SceneArea> _areas = new();

    public CubeFaceDefinition(int index, Texture2D texture)
    {
        Index = index;
        Texture = texture;
    }

    public int Index { get; } //0,1,2,3,4 (no floor)
    public Texture2D Texture { get; } // Imagem da face
    public IReadOnlyList<SceneArea> Areas => _areas; // Áreas interativas na face

    public void AddArea(SceneArea area) => _areas.Add(area);
}
