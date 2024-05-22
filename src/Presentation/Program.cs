using Infrastructure.Identity;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

// Injects depedencies
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPresentationServices();

// Adds services to the container.
builder.Services.AddControllersWithViews();


// Remove this when done testing
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-----------------------------------

// Remove this when done testing

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapIdentityApi<ApplicationUser>();

//-----------------------------------

// Configures the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapEndpoints();

app.Run();
