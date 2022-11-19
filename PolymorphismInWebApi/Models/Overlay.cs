namespace PolymorphismInWebApi.Models;

/// <summary>
/// Base model for overlay
/// </summary>
public abstract class Overlay
{
    /// <summary>
    /// Overlay Type
    /// </summary>
    public abstract OverlayType Type { get; init; }

    /// <summary>
    /// Asset Id
    /// </summary>
    public Guid AssetId { get; init; }

    /// <summary>
    /// X position of overlay
    /// </summary>
    public double XPosition { get; init; }

    /// <summary>
    /// Y position of overlay
    /// </summary>
    public double YPosition { get; init; }

    /// <summary>
    /// Height position of overlay,  Default 512
    /// </summary>
    public double? Height { get; init; } = 512;

    /// <summary>
    /// Width position of overlay,  Default 512
    /// </summary>
    public double? Width { get; init; } = 512;

    /// <summary>
    /// Scale position of overlay, Default 1.0
    /// </summary>
    public double? Scale { get; init; } = 1.0;
}
