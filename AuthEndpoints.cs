// public static class AuthEndpoints
// {
//     public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
//     {
//         var auth = group.MapGroup("/auth");

//         auth.MapPost("/login", (LoginRequest request) =>
//         {
//             if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
//                 return Results.BadRequest(new { message = "Email and password are required." });

//             if (request.Email == "user@example.com" && request.Password == "password123")
//             {
//                 var token = Guid.NewGuid().ToString("N"); // TODO: Replace with JWT token generation
//                 return Results.Ok(new LoginResponse(request.Email, token));
//             }

//             return Results.Unauthorized();
//         })
//         .WithName("Login")
//         .WithSummary("Authenticate a user and return a token");

        
//         auth.MapPost("/forgot-password", (ForgotPasswordRequest request) =>
//         {
//             if (string.IsNullOrWhiteSpace(request.Email))
//                 return Results.BadRequest(new { message = "Email is required." });

//             return Results.Ok(new { message = "If that email exists, a reset link has been sent." });
//         })
//         .WithName("ForgotPassword")
//         .WithSummary("Request a password reset link");

//         auth.MapPost("/reset-password", (ResetPasswordRequest request) =>
//         {
//             if (string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.NewPassword))
//                 return Results.BadRequest(new { message = "Token and new password are required." });

//             // TODO: Validate the token and update the user's password
//             return Results.Ok(new { message = "Password has been reset successfully." });
//         })
//         .WithName("ResetPassword")
//         .WithSummary("Reset a user's password using a valid token");

//         return group;
//     }
// }

// record LoginRequest(string Email, string Password);
// record LoginResponse(string Email, string Token);
// record ForgotPasswordRequest(string Email);
// record ResetPasswordRequest(string Token, string NewPassword);
