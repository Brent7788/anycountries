using Countries.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IRestCountriesApi, RestCountriesApi>();
builder.Services.AddHttpClient<IRestCountriesApi, RestCountriesApi>(client =>
{
    //TODO Add in the app settings
    client.BaseAddress = new Uri("https://restcountries.com/v3.1/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder.WithOrigins("https://localhost:44492", "https://localhost:7257")
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();
