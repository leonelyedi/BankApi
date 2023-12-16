using System.Text;
using BankApi.Data;
using BankApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection de depencia de DbContext
builder.Services.AddNpgsql<BankContext>(builder.Configuration.GetConnectionString("BankConnection"));


//injectar nuestro servicos o services layer
builder.Services.AddScoped<ClientServices>();
builder.Services.AddScoped<AccountServices>();
builder.Services.AddScoped<AccountTypeServices>();
builder.Services.AddScoped<LoginServices>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(Options =>
{
#pragma warning disable CS8604 // Possible null reference argument.
    Options.TokenValidationParameters = new TokenValidationParameters 
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
      ValidateIssuer = false,
      ValidateAudience = false
    };
#pragma warning restore CS8604 // Possible null reference argument.
});

builder.Services.AddAuthorization(options => {
  options.AddPolicy ("SuperAdmin", policy => policy.RequireClaim("AdminType","Super"));
});

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

app.MapControllers();

app.Run();
