namespace PolymorphismInWebApi.Models;

public abstract class Overlay
{
    /// <summary>
    /// Overlay Type
    /// </summary>
    public abstract OverlayType Type { get; init; }

    public Guid AssetId { get; init; }

    public double XPosition { get; init; }

    public double YPosition { get; init; }

    public double? Height { get; init; } = 512;

    public double? Width { get; init; } = 512;

    public double? Scale { get; init; } = 1.0;
}
