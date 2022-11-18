namespace PolymorphismInWebApi.Models;

public class CreateCommand
{
    public Overlay[] Overlays { get; init; } = Array.Empty<Overlay>();
}