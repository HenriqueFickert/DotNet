using DotNet.Presentation.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddFluentValidationConfiguration();

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddCorsConfiguration();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/NotFound");

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();