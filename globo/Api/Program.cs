using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<HouseDbContext>(options =>
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<IHouseRepository, HouseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
});

app.UseHttpsRedirection();

app.MapGet("/houses", (IHouseRepository houseRepository) =>
{
    return houseRepository.GetAll();
}).Produces<IEnumerable<HouseDto>>(StatusCodes.Status200OK);

app.MapGet("/house/{houseId:int}", async (int houseId, IHouseRepository houseRepository) =>
{
    var house = await houseRepository.GetById(houseId);
    if (house == null)
    {
        return Results.NotFound($"House with ID {houseId} not found.");
    }

    return Results.Ok(house);
})
.Produces<HouseDetailDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.Run();
