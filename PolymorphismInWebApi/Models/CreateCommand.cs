namespace PolymorphismInWebApi.Models;

/// <summary>
/// Test Command
/// </summary>
public class CreateCommand
{
    /// <summary>
    /// Array of overlays
    /// </summary>
    public Overlay[] Overlays { get; init; } = Array.Empty<Overlay>();
}