using System.ComponentModel;

namespace PolymorphismInWebApi.Models;

public class AvatarOverlay : Overlay
{
    public string Script { get; init; } = string.Empty;

    public Guid VoiceId { get; init; }

    public Guid? VoiceOverId { get; init; }

    [DefaultValue(OverlayType.AvatarOverlay)]
    public override OverlayType Type { get; init; }
}
