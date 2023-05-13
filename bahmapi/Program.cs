using System.Text;
using bahmapi.Entities;
using bahmapi.Middewares;
using bahmapi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<TokenService>();


builder.Services.AddTransient<EmailService>();

byte[] key = Encoding.ASCII.GetBytes(Secret.SecretString);


builder.Services.AddCors(p=>p.AddPolicy("corspolicy",build=>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSwaggerGen(c =>
  {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });

      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
      {
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer",
          BearerFormat = "JWT",
          In = ParameterLocation.Header,
          Description = "JWT Authorization header using the Bearer scheme.",
      });
      c.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
      });
  });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corspolicy");
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseMiddleware<IpVerificationMiddleware>();
app.MapControllers();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/", async context =>
    {
        await context.Response.WriteAsync("<a href='swagger/index.html'>Documentacao API</a>");
    });
});





app.Run();
void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<DatabaseContext>();
    services.AddTransient<IUsuarioService, UsuarioService>();
    services.AddTransient<IClienteService, ClienteService>();
    services.AddTransient<IImagemService, ImagemService>();
    services.AddTransient<IPaginaService, PaginaService>();
    services.AddTransient<IIconeService,IconeService>();
    services.AddTransient<IConcessionariaService,ConcessionariaService>();
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddScoped<AuthenticatedUser>();
    
}

public static class Secret
{
    public static string SecretString = Guid.NewGuid().ToString();
}