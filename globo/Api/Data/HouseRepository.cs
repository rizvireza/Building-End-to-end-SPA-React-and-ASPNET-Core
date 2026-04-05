using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<IEnumerable<HouseDto>> GetAll();
    Task<HouseDetailDto?> GetById(int id);
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

    public async Task<HouseDetailDto?> GetById(int id)
    {
        var h = await _dbContext.Houses.Where(h => h.Id == id)
            .SingleOrDefaultAsync(h => h.Id == id);

        if (h == null)
        {
            return null;
        }

        return new HouseDetailDto(h.Id, h.Address, h.Country, h.Price, h.Description, h.Photo);
    }   
}