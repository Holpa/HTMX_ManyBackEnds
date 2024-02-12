using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace HTMX_BackEndCore.Controllers
{
    public class GoogleAuthController : Controller
    {
        // Action to initiate Google authentication
        [HttpGet("signin-google")]
        public IActionResult SignInWithGoogle()
        {
            // Specify the callback URL where Google should redirect after authentication
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };

            // Issue a challenge to the Google authentication scheme
            return Challenge(authenticationProperties, GoogleDefaults.AuthenticationScheme);
        }

        // Action to handle the response from Google
        [HttpGet("google-response")]
        public IActionResult GoogleResponse()
        {
            // Perform the necessary checks and operations after Google authentication

            // Then redirect to the front end application
            return Redirect("https://localhost:7286");
        }
    }
}