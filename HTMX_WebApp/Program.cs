var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseDefaultFiles(); // To serve the default file such as index.html from wwwroot
app.UseStaticFiles(); // To enable static file serving

app.Run();
