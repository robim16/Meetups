

using Meetups.WebApp.Data;
using Meetups.WebApp.Features.CreateEvent;
using Meetups.WebApp.Features.EditEvent;
using Meetups.WebApp.Features.ViewCreatedEvents;
using Meetups.WebApp.Shared;
using Meetups.WebApp.Shared.Layout;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<SharedHelper>();
builder.Services.AddTransient<CreateEventService>();
builder.Services.AddTransient<ViewCreatedEventsService>();
builder.Services.AddTransient<EditEventService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()  
    .AddInteractiveServerRenderMode();

app.Run();
