using Exo.WebApi.Contexts;
using Exo.WebApi.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExoContext, ExoContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Forma de autenticação.
builder.Services.AddAuthentication( options =>{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JktBearer";
})
// Parametros de validaçãodo token.
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {   
    // Valida quem está solicitando
    ValidateIssuer = true,
    //Valida quem está recebendo
    ValidateAudience = true,
    // Define se o tempo de expiração será validado.
    ValidateLifetime = true,
    // Criptografia e validaçãoda chave de autenticação.
    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao")),
    //Valida tempo de expiração do Token
    ClockSkew = TimeSpan.FromMinutes(30),
    // Nome do ussuer, da origem
    ValidIssuer = "exoapi.webapi",
    ValidAudience = "exoapi.webapi"

    };
});
builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();
builder.Services.AddTransient<UsuarioRepository, UsuarioRepository>();

var app = builder.Build();
// Configure HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

// Habilita a autenticação
app.UseAuthentication();

//Habilita a autorização
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
