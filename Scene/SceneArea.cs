using Microsoft.Xna.Framework;

#nullable enable

namespace TrabalhoFinal.Scene;

public enum SceneAreaActionKind
{
    None, // não faz nada
    Navigate, // muda a face do cubo
    ChangeCube, // muda o cubo
    ShowObject // mostra um objeto
}

public sealed record SceneAreaAction
{
    public SceneAreaAction(SceneAreaActionKind kind, string? targetCubeId = null, int? targetFaceIndex = null, string? objectId = null)
    {
        Kind = kind;
        TargetCubeId = targetCubeId;
        TargetFaceIndex = targetFaceIndex;
        ObjectId = objectId;
    }

    public SceneAreaActionKind Kind { get; }
    public string? TargetCubeId { get; }
    public int? TargetFaceIndex { get; }
    public string? ObjectId { get; }
}

public sealed class SceneArea
{
    public SceneArea(string id, Rectangle bounds, SceneAreaAction action)
    {
        Id = id; // identificador único da área
        Bounds = bounds; // em coordenadas da face do cubo
        Action = action;
    }

    public string Id { get; }
    public Rectangle Bounds { get; }
    public SceneAreaAction Action { get; }
}
