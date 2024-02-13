namespace HTMX_BackEndCore
{
    public class Functions
    {
        public static IResult GetWeatherForecast(HttpContext context)
        {
            var forecast = new[] // Simulate some data
            {
            new { Date = DateTime.Now, TemperatureC = 25, Summary = "Sunny" },
            // ...other data
        };

            return Results.Ok(forecast);
        }

    }
}
