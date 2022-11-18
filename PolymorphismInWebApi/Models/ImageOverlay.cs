using System.ComponentModel;

namespace PolymorphismInWebApi.Models;

public class ImageOverlay : Overlay
{
    public double Angle { get; init; }

    [DefaultValue(OverlayType.ImageOverlay)]
    public override OverlayType Type { get; init; }
}
