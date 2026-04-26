public static class WebApplicationBidExtensions
{
    public static WebApplication MapBidEndpoints(this WebApplication app)
    {
        app.MapGet("/houses/{houseId:int}/bids", async (int houseId, IBidRepository bidRepository, IHouseRepository houseRepository) =>
    {
        if (await houseRepository.GetById(houseId) == null)
        {
            return Results.Problem($"House with ID {houseId} not found.", statusCode: StatusCodes.Status404NotFound);
        }
        var bids = await bidRepository.Get(houseId);
        return Results.Ok(bids);
    }).Produces<List<BidDto>>(StatusCodes.Status200OK).ProducesProblem(404);

    app.MapPost("/houses/{houseId:int}/bids", async (int houseId, [FromBody] BidDto bidDto, IBidRepository bidRepository) =>
    {
        if(bidDto.houseId != houseId)
        {
            return Results.Problem($"No match: ({bidDto.houseId}).", statusCode: StatusCodes.Status400BadRequest);
        }

        if (!MiniValidator.TryValidate(bidDto, out var errors))
        {
            return Results.ValidationProblem(errors);
        }    
        var newBid = await bidRepository.Add(bidDto);
        return Results.Created($"/houses/{houseId}/bids/{newBid.Id}", newBid);
        }).Produces<BidDto>(StatusCodes.Status201Created).ProducesValidationProblem().ProducesProblem(statusCode: StatusCodes.Status400BadRequest);
    }
}
