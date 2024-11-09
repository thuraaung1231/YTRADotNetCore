
using Newtonsoft.Json;
using System.Xml;

namespace YTRADotNetCore.incompatible_food
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

            app.MapGet("/Incompatible-food", () =>
            {
                string folderPath = "Data/Incompatible-food.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<IncompatiblefoodResponseModel>(jsonStr)!;
                return Results.Ok(result.Tbl_IncompatibleFood);
            })
.WithName("GetIncompatible-food")
.WithOpenApi();
            app.MapPost("/Incompatible-food", (IncompatiblefoodModel Incompatiblefood) =>
            {
                string folderPath = "Data/Incompatible-food.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<IncompatiblefoodResponseModel>(jsonStr)!;

                var IncompatiblefoodList = result.Tbl_IncompatibleFood?.ToList() ?? new List<IncompatiblefoodModel>();
                IncompatiblefoodList.Add(Incompatiblefood);

                result.Tbl_IncompatibleFood = IncompatiblefoodList.ToArray();

                var updatedJsonStr = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(folderPath, updatedJsonStr);

                return Results.Ok(result.Tbl_IncompatibleFood);
            })
.WithName("CreateIncompatible-food")
.WithOpenApi();

            app.MapPut("/Incompatible-food/{id}", (int id, IncompatiblefoodModel Incompatiblefood) =>
            {
                string folderPath = "Data/Incompatible-food.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<IncompatiblefoodResponseModel>(jsonStr)!;

                var item = result.Tbl_IncompatibleFood.FirstOrDefault(b => b.Id == id);
                if (item == null)
                {
                    return Results.NotFound($"Incompatiblefood with Id {id} not found.");
                }
       
                item.FoodA = Incompatiblefood.FoodA;
                item.FoodB = Incompatiblefood.FoodB;
                item.Description = Incompatiblefood.Description;
         

                var updatedJsonStr = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(folderPath, updatedJsonStr);

                return Results.Ok(result.Tbl_IncompatibleFood);
            })
            .WithName("UpdateIncompatible-food")
            .WithOpenApi();

            app.MapDelete("/Incompatible-food/{id}", (int id) =>
            {
                string folderPath = "Data/Incompatible-food.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<IncompatiblefoodResponseModel>(jsonStr)!;

                var Incompatiblefood = result.Tbl_IncompatibleFood.FirstOrDefault(b => b.Id == id);
                if (Incompatiblefood == null)
                {
                    return Results.NotFound($"Incompatiblefood with Id {id} not found.");
                }

                var IncompatiblefoodList = result.Tbl_IncompatibleFood.ToList();
                IncompatiblefoodList.Remove(Incompatiblefood);
                result.Tbl_IncompatibleFood = IncompatiblefoodList.ToArray();

                var updatedJsonStr = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(folderPath, updatedJsonStr);

                return Results.Ok(result.Tbl_IncompatibleFood);
            })
            .WithName("DeleteIncompatible-food")
            .WithOpenApi();

            app.Run();
        }
    }

    public class IncompatiblefoodResponseModel
    {
        public IncompatiblefoodModel[] Tbl_IncompatibleFood { get; set; }
    }

    public class IncompatiblefoodModel
    {
        public int Id { get; set; }
        public string FoodA { get; set; }
        public string FoodB { get; set; }
        public string Description { get; set; }
    }

}
