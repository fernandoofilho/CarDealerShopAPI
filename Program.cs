
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllers();

// Configura a conexão com o MongoDB
var connectionString = "mongodb+srv://fernandoofilho:faaf_123@cardealershop.bctcbh0.mongodb.net/";
var databaseName = "CarDealerShop";
var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase(databaseName);
Console.WriteLine("Conectado à base de dados.");

// Registra o serviço IMongoDatabase no contêiner de injeção de dependência
builder.Services.AddSingleton<MongoDatabaseBase>((MongoDatabaseBase)mongoDatabase);

var app = builder.Build();

// Configura o pipeline de solicitação HTTP.
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
