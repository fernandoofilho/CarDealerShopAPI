using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace CarDealerShopAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "mongodb+srv://fernandoofilho:faaf_123@cardealershop.bctcbh0.mongodb.net/";//Configuration.GetConnectionString("MongoDB");
            string databaseName = "CarDealerShop";//Configuration.GetValue<string>("MongoDB:DatabaseName");

            MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);
            MongoClient mongoClient = new MongoClient(settings);

            MongoDatabaseBase mongoDatabase = (MongoDatabaseBase)mongoClient.GetDatabase(databaseName);
            services.AddCors(options =>
                        {
                            options.AddPolicy("AllowLocalhost3000",
                                builder =>
                                {
                                    builder.WithOrigins("http://localhost:3000")
                                        .AllowAnyMethod()
                                        .AllowAnyHeader();
                                });
                        });
            services.AddSingleton<MongoDatabaseBase>(mongoDatabase);
            services.AddControllers().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseCors("AllowLocalhost3000");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
