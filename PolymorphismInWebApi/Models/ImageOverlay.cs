using System.ComponentModel;

namespace PolymorphismInWebApi.Models;

/// <summary>
/// Image Overlay
/// </summary>
public class ImageOverlay : Overlay
{
    /// <summary>
    /// Angle of Image overlay
    /// </summary>
    public double Angle { get; init; }


    [DefaultValue(OverlayType.ImageOverlay)]
    public override OverlayType Type { get; init; }
}
