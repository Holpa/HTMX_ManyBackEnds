
using HTMX_BackEndCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("SpecificPolicy", builder =>
//    {
//        builder.WithOrigins("https://example.com") // Replace with your client's URL
//               .WithMethods("GET", "POST", "PUT", "DELETE") // Specify the methods you want to allow
//               .WithHeaders("Content-Type", "Authorization"); // Specify the headers you want to allow
//    });
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
//app.UseCors("SpecificPolicy");
app.UseHttpsRedirection();

app.MapGet("/weatherforecast", Functions.GetWeatherForecast);

app.Run();
