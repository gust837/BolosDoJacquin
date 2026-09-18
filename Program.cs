using BolosDoJacquin.BdContextBolos;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adicionando Swagger
builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Insira um token válido para ter acesso aos endpoint da API"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
}); 

// Configuração do EFCore
builder.Services.AddDbContext<BolosContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Injeção de dependência
builder.Services.AddScoped<ICategoria, CategoriaRepository>();
builder.Services.AddScoped<IProduto, ProdutoRepository>();
builder.Services.AddScoped<IUsuario, UsuarioRepository>();
builder.Services.AddScoped<IAvaliacao, AvaliacaoRepository>();

// AUTENTICAÇÃO JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "BolosDoJacquin",
        ValidateAudience = true,
        ValidAudience = "BolosDoJacquin",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5),
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSecret") ?? "fallback-secreta-api-bolos-jacquin-senai-12345678")    
        )
    };
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<BolosDoJacquin.Middlewares.ErrorMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

