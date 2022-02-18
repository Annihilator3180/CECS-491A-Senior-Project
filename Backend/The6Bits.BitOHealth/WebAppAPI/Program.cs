using The6Bits.Authentication.Contract;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.DAL.Implementations;
using WebAppMVC.Development;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//JSON Config
builder.Configuration.GetConnectionString("cbassMac");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//pass in conn string . IS there a better way to do this?
builder.Services.AddScoped<IRepositoryUM<User>>(provider => new MsSqlUMDAO<User>(builder.Configuration.GetConnectionString("cbassMac")));
builder.Services.AddScoped<IRepositoryAuth<string>>(provider =>
    new AccountMsSqlDao(builder.Configuration.GetConnectionString("cbassMac"))); 
builder.Services.AddTransient<IAuthenticationService, JWTAuthenticationService>();
builder.Services.AddScoped<ILogDal, SQLLogDAO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var b = new dbBuilder();
    b.buildDB(builder.Configuration.GetConnectionString("cbassMac"));
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
