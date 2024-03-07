using System.Text;
using fluttyBackend.Controller.Controllers.Utils;
using fluttyBackend.Controller.Handler;
using fluttyBackend.Controller.Middlware;
using fluttyBackend.Domain;
using fluttyBackend.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// setting http 2
// builder.WebHost.UseKestrel(options =>
// {
//     options.ListenAnyIP(5000, listenOptions =>
//     {
//         listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
//     });
// });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration.GetSection("jwtStrings:secret").Value;
        var issuer = builder.Configuration.GetSection("jwtStrings:issuer").Value;
        var audience = builder.Configuration.GetSection("jwtStrings:audience").Value;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            ClockSkew = TimeSpan.FromSeconds(10),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)!
            )
        };
    });

builder.Services.AddDomain(builder.Configuration);
builder.Services.AddMyService(builder.Configuration);
builder.Services.AddTransient<RoleHandlerMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddlware>();

app.UseWhen(context => context.Request.Path.StartsWithSegments($"/{PathNameConstants.Company}"), branch =>
{
    branch.UseMiddleware<RoleHandlerMiddleware>();
});

app.UseWhen(context => context.Request.Path.StartsWithSegments($"/{PathNameConstants.Moderator}"), branch =>
{

});

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
//     endpoints.MapHub<ChatHub>("/chat");
// });

app.MapControllers();

app.Run();
