using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniValidation;

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

app.MapPost("/houses", async ([FromBody] HouseDetailDto houseDetailDto, IHouseRepository houseRepository) =>
{
    if(!MiniValidator.TryValidate(houseDetailDto, out var errors))
    {
        return Results.ValidationProblem(errors);
    }
    var newHouse = houseRepository.Add(houseDetailDto);
    return Results.Created($"/house/{newHouse.Id}", newHouse);
}).Produces<HouseDetailDto>(StatusCodes.Status201Created).ProducesValidationProblem();

app.MapPut("/houses/{houseId:int}", async ([FromBody] HouseDetailDto houseDetailDto, IHouseRepository houseRepository) =>
{
    if(!MiniValidator.TryValidate(houseDetailDto, out var errors))
    {
        return Results.ValidationProblem(errors);
    }
    if (await houseRepository.GetById(houseDetailDto.Id) == null)
    {
        return Results.Problem($"House with ID {houseDetailDto.Id} not found.", statusCode: StatusCodes.Status404NotFound);
    }

    var updatedHouse = await houseRepository.Update(houseDetailDto);
    return Results.Ok(updatedHouse);
}).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK).ProducesValidationProblem();

app.MapDelete("/houses/{houseId:int}", async (int houseId, IHouseRepository houseRepository) =>
{
    if (await houseRepository.GetById(houseId) == null)
    {
        return Results.Problem($"House with ID {houseId} not found.", statusCode: StatusCodes.Status404NotFound);
    }

    await houseRepository.Delete(houseId);
    return Results.Ok();
}).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);

app.Run();
