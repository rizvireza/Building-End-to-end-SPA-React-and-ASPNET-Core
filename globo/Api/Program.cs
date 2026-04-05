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
}).Produces<IEnumerable<HouseDto>>(200);

app.MapGet("/house/{houseId:int}", async (int houseId, IHouseRepository houseRepository) =>
{
    var house = await houseRepository.GetById(houseId);
    if (house == null)
    {
        return Results.Problem($"House with id {houseId} not found", statusCode: 404);
    }
    return Results.Ok(house);
}).ProducesProblem(404).Produces<HouseDetailDto>(200);



app.Run();
