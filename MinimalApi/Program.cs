using MinimalApi.Models;
using MinimalApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options=>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        
        Scheme = "Bearer",
        BearerFormat ="JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description="Bearer Auth",
        Type = SecuritySchemeType.Http
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<String>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,   
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"], 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer(); // dependency injection
builder.Services.AddSingleton<IMovieService,MovieService>();
builder.Services.AddSingleton<IUserService,UserService>();

var app = builder.Build();

app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();


app.MapGet("/", () => "Hellooooo World!");

app.MapPost("/create",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    (Movie movie, IMovieService service) => Create (movie, service));

app.MapGet("/get", 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standart,Admin")]
    (int id, IMovieService service) => Get(id, service));

app.MapGet("/getall", (IMovieService service) => GetAll(service));

app.MapPut("/update",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    (Movie movie, IMovieService service) => Update(movie, service));

app.MapDelete("/delete",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    (int id, IMovieService service) => Delete(id, service));

app.MapPost("/login", (UserLogin user, IUserService service) => Login(user, service));


 IResult Create(Movie movie, IMovieService service)
 {
    var result = service.Create(movie);
    return Results.Ok(result);
 }

 IResult Get(int id, IMovieService service)
 {
   var movie = service.Get(id);
    if(movie == null) return Results.NotFound("Movie not found");
    return Results.Ok(movie);
 }

 IResult GetAll(IMovieService service)
 {
    var movies = service.GetAll();
    return Results.Ok(movies);
 }

 IResult Update(Movie movie, IMovieService service)
 {
    var updateMovie = service.Update(movie);

    if(updateMovie == null) return Results.NotFound("Movie not found");
    return Results.Ok(updateMovie);

 }

 IResult Delete(int id, IMovieService service)
 {
    var result = service.Delete(id);
    if (!result) return Results.BadRequest("Something went wrong..");
    return Results.Ok(result);

 }

 IResult Login(UserLogin user, IUserService service)
 {
    if(!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
    { 
        var loggedInUser = service.Get(user);
        if (loggedInUser == null) return Results.NotFound("User not found " + user.UserName);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,loggedInUser.UserName),
            new Claim(ClaimTypes.Email,loggedInUser.EmailAddress),
            new Claim(ClaimTypes.GivenName,loggedInUser.Name),
            new Claim(ClaimTypes.Surname,loggedInUser.Surname),
            new Claim(ClaimTypes.Role,loggedInUser.Role),

        };

        var token = new JwtSecurityToken
            (
                issuer: builder.Configuration["Jwt:Issuer"],
                audience: builder.Configuration["Jwt:Audience"],
                claims: claims,
                expires:DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),SecurityAlgorithms.HmacSha256)
            );
      
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Results.Ok(tokenString);
    }
    return Results.BadRequest();
}

 app.UseSwaggerUI();
 app.Run();
