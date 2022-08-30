using Data_Access.Interfaces;
using Data_Access.Services;
using Invitation_Website.Seed;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<InvitationToolContext>(options => options
                                                    .UseMySql(connection,serverVersion)
                                                    .LogTo(Console.WriteLine, LogLevel.Information)
                                                    .EnableSensitiveDataLogging()
                                                    .EnableDetailedErrors());

builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IPersonService, PersonService>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<InvitationToolContext>();
        ContextSeed.SeedUser(context);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();
app.UseRouting();
app.MapDefaultControllerRoute();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
