using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ReservaSalas.Aplicacao.Abstracoes;
using ReservaSalas.Aplicacao.DTOs.Reserva;
using ReservaSalas.Aplicacao.Servico;
using ReservaSalas.Aplicacao.Validators;
using ReservaSalas.Infraestrutura.Persistencia;
using ReservaSalas.Infraestrutura.Persistencia.Repos;

var builder = WebApplication.CreateBuilder(args);

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection string (via User Secrets ou appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("Default");

// Configura o EF Core com SQLite
builder.Services.AddDbContext<ReservaSalasDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IReservasRepositorioLeitura, ReservasRepositorio>();
builder.Services.AddScoped<IReservasRepositorioEscrita, ReservasRepositorio>();
builder.Services.AddScoped<ILocaisRepositorioLeitura, LocaisRepositorio>();

builder.Services.AddScoped<ReservaAppService>();
builder.Services.AddScoped<LocaisAppService>();

builder.Services.AddScoped<IValidator<CriarReservaDto>, CriarReservaValidator>();
builder.Services.AddScoped<IValidator<AtualizarReservaDto>, AtualizarReservaValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
