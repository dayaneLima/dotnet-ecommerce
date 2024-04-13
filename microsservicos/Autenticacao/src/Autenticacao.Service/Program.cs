using Autenticacao.Service.Extensions;
using Autenticacao.Data.Context;
using Autenticacao.Application.AutoMappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy ("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
);  

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddDIConfiguration();

builder.Services.AddAutoMapper(typeof(AutoMapperMappingProfile));

builder.Services.AddDbContext<AutenticacaoContext>(options => options
    .UseMySQL(builder.Configuration.GetConnectionString("MysqlConnectionString") ?? "",
        p => p.MigrationsHistoryTable("__Migrations")),
    // .EnableSensitiveDataLogging(),
    ServiceLifetime.Scoped
);

builder.Services.AddAutenticationJwt(builder.Configuration);

// builder.Services.AddCustomizacaoErros();

// builder.Service.AddHttpContextAccessor();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
