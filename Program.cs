using Microsoft.EntityFrameworkCore;
using PruebaTecnicaApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MercadoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));


builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var ReglasCores = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy
    (name: ReglasCores, builder =>
    { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});





var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(ReglasCores);

app.UseAuthorization();

app.MapControllers();

app.Run();
