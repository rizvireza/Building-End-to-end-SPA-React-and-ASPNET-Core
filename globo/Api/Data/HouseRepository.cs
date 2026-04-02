using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<IEnumerable<HouseDto>> GetAll();
}

public class HouseRepository : IHouseRepository
{
    private readonly HouseDbContext _dbContext;

    public HouseRepository(HouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<HouseDto>> GetAll()
    {
        return await _dbContext.Houses.Select(h => new HouseDto(h.Id, h.Address, h.Country, h.Price)).ToListAsync();
    }
}