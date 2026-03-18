var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ----------------------------------------------------
// 1️⃣ Force HTTPS
// ----------------------------------------------------
app.UseHttpsRedirection();

// ----------------------------------------------------
// 2️⃣ Global Exception Handling
// ----------------------------------------------------
app.UseExceptionHandler("/Home/Error");

// ----------------------------------------------------
// 3️⃣ Content Security Policy (Basic Security)
// ----------------------------------------------------
app.Use(async (context, next) =>
{
    context.Response.Headers.Add(
        "Content-Security-Policy",
        "default-src 'self'; script-src 'self'; style-src 'self';"
    );

    await next();
});

// ----------------------------------------------------
// 4️⃣ Serve Static Files (wwwroot)
// ----------------------------------------------------
app.UseStaticFiles();

// ----------------------------------------------------
// 5️⃣ Custom Logging Middleware
// ----------------------------------------------------
app.Use(async (context, next) =>
{
    Console.WriteLine("----- REQUEST START -----");
    Console.WriteLine($"Method: {context.Request.Method}");
    Console.WriteLine($"URL: {context.Request.Path}");

    await next();

    Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
    Console.WriteLine("----- REQUEST END -----");
});

// ----------------------------------------------------
// 6️⃣ Routing
// ----------------------------------------------------
app.UseRouting();

app.UseAuthorization();

// ----------------------------------------------------
// 7️⃣ Default MVC Route
// ----------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();