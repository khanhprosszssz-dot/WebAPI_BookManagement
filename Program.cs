using Microsoft.EntityFrameworkCore;
using WebAPI_BookManagement.Data;
using WebAPI_BookManagement.Repositories;

var builder =
    WebApplication.CreateBuilder(args);


//database connection

var connectionString =
    builder.Configuration
        .GetConnectionString(
            "DefaultConnection");

builder.Services
    .AddDbContext<AppDbContext>(
        options =>
            options.UseSqlServer(
                connectionString));


//repository injection

builder.Services
    .AddScoped<
        IBookRepository,
        SQLBookRepository>();

builder.Services
    .AddScoped<
        IAuthorRepository,
        SQLAuthorRepository>();

builder.Services
    .AddScoped<
        IPublisherRepository,
        SQLPublisherRepository>();


//controller injection

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();