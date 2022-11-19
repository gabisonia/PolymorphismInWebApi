using System.ComponentModel;

namespace PolymorphismInWebApi.Models;

/// <summary>
/// Avatar Overlay
/// </summary>
public class AvatarOverlay : Overlay
{
    /// <summary>
    /// Script
    /// </summary>
    public string Script { get; init; } = string.Empty;

    /// <summary>
    /// Voice Id for Avatar
    /// </summary>
    public Guid VoiceId { get; init; }

    /// <summary>
    /// Voiceover Id for Avatar
    /// </summary>
    public Guid? VoiceOverId { get; init; }

    [DefaultValue(OverlayType.AvatarOverlay)]
    public override OverlayType Type { get; init; }
}
