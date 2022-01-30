using FreeCourse.Services.Discount.Services;
using FreeCourse.Services.Discount.Settings;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

var requireAuthorizePolicy =
    new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerURL"];
        options.Audience = "resource_discount";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings")
);

builder.Services.AddSingleton<IDatabaseSettings>(
    serviceProvider => serviceProvider
        .GetRequiredService<IOptions<DatabaseSettings>>
    ().Value
);

builder.Services.AddScoped<IDbConnection>(
    serviceProvider =>
    {
        var databaseSettings = serviceProvider
            .GetRequiredService<IDatabaseSettings>();

        return new NpgsqlConnection
        {
            ConnectionString = $"Server={databaseSettings.Host};" +
                $"Port={databaseSettings.PortNumber};" +
                $"User ID={databaseSettings.Username};" +
                $"Password={databaseSettings.Password};" +
                $"Database={databaseSettings.DatabaseName};" +
                $"Integrated Security={databaseSettings.IntegratedSecurity};" +
                $"Pooling={databaseSettings.Pooling};"
        };
    }
);

//builder.Services.AddScoped<IDbConnection>(
//    serviceProvider => new NpgsqlConnection
//    {
//        ConnectionString = builder.Configuration
//            .GetConnectionString("PostgreSql")
//    });

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
