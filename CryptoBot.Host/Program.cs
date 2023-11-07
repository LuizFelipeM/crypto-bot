using System.Text.Json.Serialization;
using CryptoBot.Domain;
using CryptoBot.Host;
using CryptoBot.Host.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions => serverOptions.ListenAnyIP(int.Parse(Environment.GetEnvironmentVariable("PORT"))));

// Add services to the container.
builder.Services
    .AddCors()
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtConfig = builder.Configuration.GetSection("JWT").Get<JwtConfig>();
builder.Services.AddSingleton(jwtConfig);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(jwtConfig?.SecretBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

MapperStartup.RegisterMappers();
ContainerStartup.RegisterJobs(builder.Configuration, builder.Services);
ContainerStartup.RegisterLavinMQClients(builder.Configuration, builder.Services);
ContainerStartup.RegisterServices(builder.Configuration, builder.Services);
ContainerStartup.RegisterRepositories(builder.Configuration, builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt => opt.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
