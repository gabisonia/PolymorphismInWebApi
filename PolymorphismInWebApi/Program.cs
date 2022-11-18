using JsonSubTypes;
using PolymorphismInWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Register subtypes and define discriminator
        options.SerializerSettings.Converters.Add(
        JsonSubtypesConverterBuilder
        .Of(typeof(Overlay), "type")
        .RegisterSubtype(typeof(AvatarOverlay), OverlayType.AvatarOverlay)
        .RegisterSubtype(typeof(ImageOverlay), OverlayType.ImageOverlay)
        .SerializeDiscriminatorProperty()
        .Build());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.UseOneOfForPolymorphism();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
