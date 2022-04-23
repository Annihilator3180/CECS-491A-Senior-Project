using System.Runtime.CompilerServices;
using FoodAPI;
using FoodAPI.Contracts;
using The6Bits.Authentication.Contract;
using The6Bits.Authentication.Implementations;
using The6Bits.Authorization.Contract;
using The6Bits.Authorization.Implementations;
using The6Bits.BitOHealth.ControllerLayer;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.Models.WeightManagement;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.DAL.Implementations;
using The6Bits.DBErrors;
using The6Bits.EmailService;
using The6Bits.HashAndSaltService;
using The6Bits.HashAndSaltService.Contract;
using The6Bits.HashAndSaltService.Implementations;
using The6Bits.SMTPEmailService.Implementation;
using WebAppMVC.Development;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//JSON Config
builder.Configuration.GetConnectionString("DefaultConnection");

var connstring  = builder.Configuration.GetConnectionString("DefaultConnection");
var Configuration = builder.Configuration;



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Configuration.AddJsonFile("secrets.json");


//pass in conn string . IS there a better way to do this?
builder.Services.AddScoped<IRepositoryDietRecommendations, DietRecommendationsMsSqlDao>(provider => new DietRecommendationsMsSqlDao(connstring));
builder.Services.AddScoped<IRepositoryUM<User>>(provider => new MsSqlUMDAO<User>(connstring));
builder.Services.AddScoped<IRepositoryAuth<string>>(provider =>
    new AccountMsSqlDao(connstring));
builder.Services.AddScoped<IRepositoryMedication<string>>(provider =>
    new MsSqlMedicationDAO(connstring));
builder.Services.AddHttpClient<IDrugDataSet, OpenFDADAO>(client =>
{
    client.BaseAddress = new Uri("https://api.fda.gov/drug/");
});
builder.Services.AddSingleton(new openFDAConfig
{
    APIKey = builder.Configuration["OpenFda"],
});

builder.Services.AddTransient<IAuthenticationService>(provider => new JWTAuthenticationService(builder.Configuration["jwt"]));
builder.Services.AddTransient<IDBErrors, MsSqlDerrorService>();

builder.Services.AddTransient<ISMTPEmailService, AWSSesService>();
builder.Services.AddScoped<IReminderDatabase>(provider => new ReminderMsSqlDao(connstring));
builder.Services.AddScoped<ILogDal, SQLLogDAO>();
builder.Services.AddSingleton<IConfiguration>(Configuration);
builder.Services.AddTransient<HotTopicsService>(provider=>new HotTopicsService("0c0dc5fd4cc641a58578260b7e4815ff"));
builder.Services.AddTransient<HashNSaltService>(provider => new HashNSaltService(new MsSqlHashDao(connstring), builder.Configuration["jwt"]));

builder.Services.AddScoped<IAuthorizationDao>(provider => new MsSqlRoleAuthorizationDao(connstring));
builder.Services.AddScoped<IHashDao>(provider=> new MsSqlHashDao(connstring));



//Weight Management
builder.Services.AddScoped<IFoodAPI<Parsed>, EdamamAPIService<Parsed>>();
builder.Services.AddHttpClient<EdamamAPIService<Parsed>>();
builder.Services.AddSingleton(new EdamamConfig {
    AppKey = builder.Configuration["Edamam_Key"], 
    AppId = builder.Configuration["Edamam_ID"],
});



//SES

builder.Services.AddSingleton(new SESConfig()
{
    SMTP_USERNAME = builder.Configuration["AWSUser"],
    SMTP_PASSWORD = builder.Configuration["AWSPass"],

});
builder.Configuration.AddEnvironmentVariables();


builder.Services.AddTransient<IRepositoryWeightManagementDao<IWeightManagerResponse>>(provider => new WeightManagementMsSqlDao(connstring));
builder.Services.AddTransient<IRepositoryWeightManagementImageDao<IWeightManagerResponse>>(provider => new WeightManagementWindowsDao(builder.Configuration.GetSection("FilePaths")["WeightManagementPath"]));
builder.Services.AddTransient<IRepositoryHealthRecorderDAO>(provider => new HealthRecorderMsSqlDAO(connstring));
//builder.Services.AddTransient<IAccountService, AccountService>();

var app = builder.Build();


//START ARCHIVER

var archive = new ArchivingController();
archive.Archive();

var recoveryReset = new RecoveryResetController();
recoveryReset.ResetRecovery(connstring);



// Configure the HTTP request pipeline.

    var b = new dbBuilder();
    b.builAccountdDB(connstring);
    b.buildVerifyCodes(connstring);
    b.buildFailedAttempts(connstring);
    b.buildTrackerLogs(connstring);
    b.buildRecovery(connstring);
    b.buildWMGoals(connstring);
    b.buildFavoriteMedication(connstring);
    b.addBossAdmin(connstring);
    b.BuildHealthRecorder(connstring);
    b.buildDiet(connstring);
    b.buildRemiders(connstring);
    b.buildFoodLog(connstring);
    b.buildWeightGoalImageDB(connstring);
    b.buildFavoriteRecipe(connstring);
    //app.UseSwagger();
    //app.UseSwaggerUI();


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
