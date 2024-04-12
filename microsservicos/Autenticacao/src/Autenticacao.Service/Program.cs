using Autenticacao.Service.Extensions;
using Autenticacao.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost.ConfigureKestrel(opt => {
//     opt.ListenAnyIP(5000);
//     // opt.ListenAnyIP(7002, listOpt => {
//     //     listOpt.UseHttps("/https/certificado.pfx", "");
//     // });
// });

builder.Services.AddCors(options =>
    options.AddPolicy ("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
);  

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddDIConfiguration();

// builder.Service.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

builder.Services.AddDbContext<AutenticacaoContext>(options => options
    .UseMySQL(builder.Configuration.GetConnectionString("MysqlConnectionString") ?? "",
        p => p.MigrationsHistoryTable("__Migrations"))
    .EnableSensitiveDataLogging(),
    ServiceLifetime.Scoped
);

// https://guilherme-rmendes95.medium.com/autentica%C3%A7%C3%A3o-e-autoriza%C3%A7%C3%A3o-jwt-net-409b191ba20c
builder.Services.AddAutenticationJwt(builder.Configuration);

builder.Services.AddCustomizacaoErros();

// builder.Service.AddConsumidorHandler();
// builder.Service.AddPainelAtendimentoObtencaoHandler();

// builder.Service.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseDeveloperExceptionPage();
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
