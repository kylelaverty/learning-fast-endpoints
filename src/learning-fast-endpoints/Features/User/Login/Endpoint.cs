using FastEndpoints.Security;
using Learning.FastEndpoionts.Utils;

namespace Learning.FastEndpoionts.Features.User.Login;

public class Endpoint : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/user/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var jwtToken = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = "The secret used to sign tokens that is longer.";
                o.ExpireAt = DateTime.UtcNow.AddHours(1);
                o.User.Roles.Add("Manager", "Auditor");
                o.User.Claims.Add(("UserName", req.Username));
                o.User.Permissions.Add(Allow.User_Create);
                o.User["UserId"] = "001";
                o.Audience = "The audience";
                o.Issuer = "The issuer";
            });

        var response = new Response
        {
            Username = req.Username,
            Token = jwtToken
        };

        await SendAsync(response);
    }
}