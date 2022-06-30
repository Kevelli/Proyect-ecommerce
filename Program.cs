using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LeoEcommerce.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<DomainContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireManager", policy => policy.RequireRole("Manager"));
    options.AddPolicy("RequireSupervisor", policy => policy.RequireRole("Supervisor"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "RequireAdmin");
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

var app = builder.Build();

/*builder.services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    //previous code removed for clarity reasons

    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    opt.Lockout.MaxFailedAccessAttempts = 3;
});
*/


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

SeedDatabase();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();

async void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        using (var roleManagement = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>())
        using (var userManagement = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>())
        {
            
            // Seeding roles.

            var seedRoles = new List<string> { "Admin", "Supervisor", "Manager", "Customer" };

            foreach (var newRole in seedRoles)
            {
                if (await roleManagement.RoleExistsAsync(newRole)) continue;
                await roleManagement.CreateAsync(new IdentityRole(newRole));
            }
            
            // Seeding supervisor user.

            if (userManagement.Users.Any(user => user.UserName == "supervisor@admin.com")) return;
            var supervisorUser = new IdentityUser("supervisor@admin.com");
            await userManagement.CreateAsync(supervisorUser, "Supervisor123!");
            await userManagement.AddToRolesAsync(supervisorUser, new List<string>()
            {
                "supervisor",
                "manager",
                "admin"
            });
            
            // Seeding manager user.dotnet ef database update
            
            if (userManagement.Users.Any(user => user.UserName == "manager@admin.com")) return;
            var managerUser = new IdentityUser("manager@admin.com");
            await userManagement.CreateAsync(managerUser, "Manager123!");
            await userManagement.AddToRolesAsync(managerUser, new List<string>()
            {
                "manager",
                "admin"
            });
            
        }
    }
}