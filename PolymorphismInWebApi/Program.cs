using JsonSubTypes;
using PolymorphismInWebApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

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

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"PolymorphismInWebApi.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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

AddLogger(builder.Logging);

app.MapControllers();

app.Run();


static void AddLogger(ILoggingBuilder loggingBuilder)
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
        .Build();

    var elasticEnabled = configuration.GetValue<bool>("Logging:Elastic");
    var debugLogEnabled = configuration.GetValue<bool>("Logging:Console");
    var dbLogEnabled = configuration.GetValue<bool>("Logging:Database");

    var connectionString = configuration["ConnectionStrings:Default"];

    var logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Conditional(evt => elasticEnabled, wt => wt
            .Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearchUrl"]))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                IndexFormat = $"Application-Logs-{environment}-{DateTime.UtcNow:yyyy-MM}"
            })
            .Enrich.WithProperty("Environment", environment))
        .WriteTo.Conditional(evt => debugLogEnabled, wt => wt.Console())
        .WriteTo.Conditional(evt => dbLogEnabled, wt => wt.PostgreSQL(connectionString,
            "Logs",
            needAutoCreateTable: true,
            respectCase: true,
            restrictedToMinimumLevel: LogEventLevel.Error,
            useCopy: false))
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog(logger);
}