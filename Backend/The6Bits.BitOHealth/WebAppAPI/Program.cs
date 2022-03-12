using The6Bits.Authentication.Contract;
using The6Bits.Authentication.Implementations;
using The6Bits.Authorization.Contract;
using The6Bits.Authorization.Implementations;
using The6Bits.BitOHealth.ControllerLayer;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.DAL.Implementations;
using The6Bits.DBErrors;
using The6Bits.EmailService;
using The6Bits.HashAndSaltService.Contract;
using The6Bits.HashAndSaltService.Implementations;
using WebAppMVC.Development;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//JSON Config
builder.Configuration.GetConnectionString("Connection2");

var connstring  = builder.Configuration.GetConnectionString("Connection2");
var Configuration = builder.Configuration;



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//pass in conn string . IS there a better way to do this?
builder.Services.AddScoped<IRepositoryUM<User>>(provider => new MsSqlUMDAO<User>(connstring));
builder.Services.AddScoped<IRepositoryAuth<string>>(provider =>
    new AccountMsSqlDao(connstring));
builder.Services.AddScoped<IRepositoryMedication<string>>(provider =>
    new MsSqlMedicationDAO(connstring));
builder.Services.AddTransient<IDrugDataSet, OpenFDADAO>();
builder.Services.AddTransient<IAuthenticationService>(provider => new JWTAuthenticationService(builder.Configuration.GetSection("PKs")["JWT"]));
builder.Services.AddTransient<IDBErrors, MsSqlDerrorService>();
builder.Services.AddTransient<ISMTPEmailService, AWSSesService>();
builder.Services.AddScoped<ILogDal, SQLLogDAO>();
builder.Services.AddSingleton<IConfiguration>(Configuration);
builder.Services.AddScoped<IAuthorizationDao>(provider => new MsSqlRoleAuthorizationDao(connstring));
builder.Services.AddScoped<IHashDao>(provider=> new MsSqlHashDao(connstring));



builder.Configuration.AddEnvironmentVariables();


builder.Services.AddTransient<IRepositoryWeightManagementDao>(provider => new WeightManagementMsSqlDao(connstring));
//builder.Services.AddTransient<IAccountService, AccountService>();

var app = builder.Build();


//START ARCHIVER

var archive = new ArchivingController();
archive.Archive();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var b = new dbBuilder();
    b.builAccountdDB(connstring);
    b.buildVerifyCodes(connstring);
    b.buildFailedAttempts(connstring);
    b.buildTrackerLogs(connstring);
    b.buildRecovery(connstring);
    b.buildWMGoals(connstring);
    b.addBossAdmin(connstring);
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseAuthorization();

app.MapControllers();

app.Run();
