
using Newtonsoft.Json;

namespace YTRADotNetCore.BirdWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            app.MapGet("/birds", () =>
            {
                string folderPath = "Data/birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
                return Results.Ok(result.Tbl_Bird);
            })
.WithName("GetBirds")
.WithOpenApi();
            app.MapPost("/birds", (BirdModel bird) =>
            {
                string folderPath = "Data/birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

                var birdList = result.Tbl_Bird?.ToList() ?? new List<BirdModel>();
                birdList.Add(bird);

                result.Tbl_Bird = birdList.ToArray();

                var updatedJsonStr = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(folderPath, updatedJsonStr);

                return Results.Ok(result.Tbl_Bird);
            })
.WithName("CreateBirds")
.WithOpenApi();

            app.MapPut("/birds/{id}", (int id, BirdModel bird) =>
            {
                string folderPath = "Data/birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

                var item = result.Tbl_Bird.FirstOrDefault(b => b.Id == id);
                if (item == null)
                {
                    return Results.NotFound($"Bird with Id {id} not found.");
                }

                item.BirdMyanmarName = bird.BirdMyanmarName;
                item.BirdEnglishName = bird.BirdEnglishName;
                item.Description = bird.Description;
                item.ImagePath = bird.ImagePath;

                var updatedJsonStr = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(folderPath, updatedJsonStr);

                return Results.Ok(result.Tbl_Bird);
            })
            .WithName("UpdateBird")
            .WithOpenApi();

            app.MapDelete("/birds/{id}", (int id) =>
            {
                string folderPath = "Data/birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

                var bird = result.Tbl_Bird.FirstOrDefault(b => b.Id == id);
                if (bird == null)
                {
                    return Results.NotFound($"Bird with Id {id} not found.");
                }

                var birdList = result.Tbl_Bird.ToList();
                birdList.Remove(bird);
                result.Tbl_Bird = birdList.ToArray();

                var updatedJsonStr = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(folderPath, updatedJsonStr);

                return Results.Ok(result.Tbl_Bird);
            })
            .WithName("DeleteBird")
            .WithOpenApi();

            
            app.Run();
        }
    }

    public class BirdResponseModel
    {
        public BirdModel[] Tbl_Bird { get; set; }
    }

    public class BirdModel
    {
        public int Id { get; set; }
        public string BirdMyanmarName { get; set; }
        public string BirdEnglishName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }

}
