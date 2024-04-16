using Pedidos.Service.Extensions;
using Pedidos.Data.Context;
using Pedidos.Application.AutoMappers;
using Microsoft.EntityFrameworkCore;
using Pedidos.Application.MessageBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy ("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
);  

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddDIConfiguration(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperMappingProfile));
builder.Services.AddHostedService<RabbitMQPedidoConsumer>();
builder.Services.AddIntegration(builder.Configuration);

builder.Services.AddDbContext<PedidoContext>(options => options
    .UseMySQL(builder.Configuration.GetConnectionString("MysqlConnectionString") ?? "",
        p => p.MigrationsHistoryTable("__Migrations")),
    ServiceLifetime.Scoped
);

builder.Services.AddAutenticationJwt(builder.Configuration);
builder.Services.AddCustomizacaoErros();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();