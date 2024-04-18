
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner.
builder.Services.AddControllers();

// Configura a conex�o com o MongoDB
var connectionString = "mongodb+srv://fernandoofilho:faaf_123@cardealershop.bctcbh0.mongodb.net/";
var databaseName = "CarDealerShop";
var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase(databaseName);
Console.WriteLine("Conectado � base de dados.");

// Registra o servi�o IMongoDatabase no cont�iner de inje��o de depend�ncia
builder.Services.AddSingleton<MongoDatabaseBase>((MongoDatabaseBase)mongoDatabase);

var app = builder.Build();

// Configura o pipeline de solicita��o HTTP.
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Configura os endpoints dos controllers da API
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
