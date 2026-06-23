// =====================================================================
//  TARGET WEBSITE  (run this, then attack it with the client)
// =====================================================================
//  This is a DELIBERATELY INSECURE login site for the lesson. The
//  /validate endpoint:
//    - accepts credentials over GET *or* POST
//    - compares them in plaintext
//    - has no rate limiting and no account lockout
//  Those are the weaknesses being demonstrated, not bugs to fix.
//
//  Press Ctrl+F5 to run it. It serves on http://localhost:5000
// =====================================================================

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();   // serve wwwroot/index.html at "/"
app.UseStaticFiles();

app.MapMethods("/validate", new[] { "GET", "POST" }, async (HttpContext ctx) =>
{
    string username = ctx.Request.Query["username"];
    string password = ctx.Request.Query["password"];

    if (string.IsNullOrEmpty(username) && ctx.Request.HasFormContentType)
    {
        var form = await ctx.Request.ReadFormAsync();
        username = form["username"];
        password = form["password"];
    }

    // Known logins.
    // For the class task, bob's password is "a number between 1 and 99".
    // Change the number below before the lesson if you like.
    bool ok = (username == "username" && password == "p@ssw0rd")
           || (username == "bob" && password == "42");

    ctx.Response.Redirect(ok ? "/admin.html" : "/login.html");
});

app.Run();
