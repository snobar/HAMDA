using HAMDA.Data;
using HAMDA.Provider.Middleware;
using HAMDA.Repository.IService;
using HAMDA.Service.Service;
using HAMDA.Service.Service.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
{
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequiredLength = 3;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender>(serviceProvider =>
{
    var emailSettings = serviceProvider.GetRequiredService<IOptions<EmailSettings>>().Value;
    return new EmailSender(
        emailSettings.SmtpHost,
        emailSettings.SmtpPort,
        emailSettings.FromEmail,
        emailSettings.SmtpUser,
        emailSettings.SmtpPass);
});


builder.Services.AddScoped<ICostumer, Costumer>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IAdminDashboard, AdminDashboard>();
builder.Services.AddScoped<IAccountService, AccountService>();


var logFilePath = Path.Combine(builder.Environment.ContentRootPath, "Logs", "error.log");
builder.Services.AddSingleton(new FileLoggerService(logFilePath));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseMiddleware<BlockIdentityMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
