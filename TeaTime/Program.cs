using Microsoft.EntityFrameworkCore;
using TeaTime.DataAccess.Data;
using TeaTime.DataAccess.Repository;
using TeaTime.DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// ��Ʈw�]�w������s�[
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// �s�W��Ʈw�s����Repository
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// �s�WUnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
app.Run();
