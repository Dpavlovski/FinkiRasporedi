using FinkiRasporedi.Models.Identity;
using FinkiRasporedi.Repository;
using FinkiRasporedi.Repository.Data;
using FinkiRasporedi.Repository.Impl;
using FinkiRasporedi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    options.UseLazyLoadingProxies();
    options.EnableSensitiveDataLogging();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Student>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IProfessorRepository), typeof(ProfessorRepository));
builder.Services.AddScoped(typeof(ISubjectRepository), typeof(SubjectRepository));
builder.Services.AddScoped(typeof(ICourseRepository), typeof(CourseRepository));
builder.Services.AddScoped(typeof(ISemesterRepository), typeof(SemesterRepository));
builder.Services.AddScoped(typeof(IScheduleRepository), typeof(ScheduleRepository));
builder.Services.AddScoped(typeof(IRoomRepository), typeof(RoomRepository));
builder.Services.AddScoped(typeof(ILectureRepository), typeof(LectureRepository));
builder.Services.AddScoped(typeof(ICustomLectureRepository), typeof(CustomLectureRepository));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder
            .AllowAnyOrigin() // You can replace this with specific origins if needed
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();
app.UseCors("AllowAnyOrigin");



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
