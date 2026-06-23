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

    // --- Demo account --------------------------------------------------
    // GIVE these credentials to students (they are in the worksheet) so they
    // can see what a successful login looks like, and what a failed one looks
    // like when the password is wrong.
    //
    // --- Target account ------------------------------------------------
    // This is the account students must CRACK. bob's password is a whole
    // number between 1 and 99 - change it before the lesson if you like.
    bool ok = (username == "demo" && password == "letmein")
           || (username == "bob"  && password == "42");

    ctx.Response.Redirect(ok ? "/admin.html" : "/login.html");
});

app.Run();
