using Microsoft.OpenApi.Models;
using PermissionApi;
using PermissionApi.Middleware;
using PermissionApi.Models;
using PermissionApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TestApp",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
               {
                 new OpenApiSecurityScheme
                 {
                   Reference = new OpenApiReference
                   {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                   }
                  },
                  new string[] { }
               }
            });
});

builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<PermissionService>();
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<UserClaims>();
builder.Services.AddScoped<EndpointPermissionMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserClaimsMiddleware>();
app.UseMiddleware<EndpointPermissionMiddleware>();

app.MapControllers();

app.Run();
