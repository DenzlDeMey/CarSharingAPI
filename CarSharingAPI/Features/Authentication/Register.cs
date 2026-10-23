using Microsoft.AspNetCore.Identity;

namespace CarSharingAPI.Features.Authentication;

public static class Register
{
    public record RegisterRequest(string Username, string Password);

    public static RouteGroupBuilder MapRegisterEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("register", async (
            RegisterRequest request,
            UserManager<IdentityUser> userManager) =>
        {
            var user = new IdentityUser { UserName = request.Username };
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return Results.BadRequest(result.Errors);

            return Results.Ok(new { message = "Gebruiker aangemaakt." });
        });

        return group;
    }
}