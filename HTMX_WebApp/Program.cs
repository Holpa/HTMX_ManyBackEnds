using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    // Set the login path and access denied path
    options.LoginPath = "/";
    options.AccessDeniedPath = "/accessdenied";
})
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    googleOptions.CallbackPath = new PathString("/"); // This should match the redirect URI
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles(); // Add this line to enable default files
app.UseStaticFiles();  // This line already exists in your cod

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/signin-google", async context =>
{
    await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
    {
        RedirectUri = "/", // Or wherever you want to redirect after successful login
    });
});

// Map the check-auth route to the CheckAuth handler method
app.MapGet("/check-auth", (HttpContext context) =>
{
    // Determine if the user is authenticated and get the user's name if available
    var isAuthenticated = context.User.Identity.IsAuthenticated;
    var userName = isAuthenticated ? context.User.Identity.Name : "Guest";

    // Create an HTML snippet that reflects the user's authentication status
    var htmlSnippet = isAuthenticated
        ? $"<p>User is authenticated: <strong>true</strong></p><p>User name: <strong>{userName}</strong></p>"
        : "<p>User is not authenticated. Please <a href='/signin-google'>sign in with Google</a>.</p>";

    return Results.Content(htmlSnippet, "text/html");
});

app.Run();