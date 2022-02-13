using The6Bits.Authorization.Contract;
using The6Bits.Authorization.Implementations;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.DAL.Implementations;
using WebAppMVC.Development;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//JSON Config
builder.Configuration.GetConnectionString("DefaultConnection");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositoryUM<User>, MsSqlUMDAO<User>>();
builder.Services.AddTransient<IAuthorizationService, DESAuthorizationService>();
builder.Services.AddScoped<ILogDal, SQLLogDAO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var b = new dbBuilder();
    b.buildDB(builder.Configuration.GetConnectionString("DefaultConnection"));
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
