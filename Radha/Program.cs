using Radha.Data;
using Radha.Repositories;
using Radha.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<BookContext>(); 
builder.Services.AddScoped<IUnitOfRepository, UnitOfRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>(); 
builder.Services.AddScoped<IHolidayRepository, HolidayRepository>(); 
builder.Services.AddScoped<IWeekendRepository, WeekendRepository>(); 
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors(builder =>
{
    builder //.WithOrigins("https://localhost:port")
        .SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.MapControllers();

app.Run();