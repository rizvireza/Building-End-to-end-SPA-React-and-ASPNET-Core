public interface IBidRepository
{
    Task<List<BidDto>> Get(int houseId);
    Task<BidDto> Add(BidDto bidDto);
}

public class BidRepository : IBidRepository
{
    private readonly HouseDbContext _dbContext;

    public BidRepository(HouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BidDto>> Get(int houseId)
    {
        return await _dbContext.Bids
            .Where(b => b.HouseId == houseId)
            .Select(b => new BidDto(b.Id, b.HouseId, b.Bidder, b.Amount))
            .ToListAsync();
    }

    public async Task<BidDto> Add(BidDto bidDto)
    {
        var entity = new BidEntity
        {
            HouseId = bidDto.HouseId,
            Bidder = bidDto.Bidder,
            Amount = bidDto.Amount
        };
        _dbContext.Bids.Add(entity);
        await _dbContext.SaveChangesAsync();
        return new BidDto(entity.Id, entity.HouseId, entity.Bidder, entity.Amount);
    }
}