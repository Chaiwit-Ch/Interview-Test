using System.Security.Cryptography;
using System.Text;

namespace Interview_Test.Middlewares;

public class AuthenMiddleware : IMiddleware
{
    private const string hashedKey = "05C2B92D51F2EBBB93B441277DCEB22E531EAECB3ABA966C50BD64D7AA778E5656CA0C0AA3E496237A115B89F9F01163E648D9831688A290F5F0B0B2B20B9CB8";

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var apiKeyHeader = context.Request.Headers["x-api-key"];

        if (string.IsNullOrEmpty(apiKeyHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API Key is missing");
            return;
        }

        var inputApiKey = apiKeyHeader.ToString();
        var hashedInputApiKey = ComputeSha512Hash(inputApiKey);

        if (!string.Equals(hashedInputApiKey, hashedKey, StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await next(context);
    }

    private static string ComputeSha512Hash(string rawData)
    {
        using var sha512 = SHA512.Create();
        var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToHexString(bytes);
    }
}