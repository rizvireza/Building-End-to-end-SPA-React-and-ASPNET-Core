using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<IEnumerable<HouseDto>> GetAll();
    Task<HouseDetailDto?> GetById(int id);
    Task<HouseDetailDto> Add(HouseDetailDto houseDetailDto);
    Task<HouseDetailDto> Update(HouseDetailDto houseDetailDto);
    Task Delete(int id);
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

        return EntityToDto(h);
    }

    private static void DtoToEntity(HouseDetailDto houseDetailDto, HouseEntity entity)
    {
        entity.Address = houseDetailDto.Address;
        entity.Country = houseDetailDto.Country;
        entity.Price = houseDetailDto.Price;
        entity.Description = houseDetailDto.Description;
        entity.Photo = houseDetailDto.Photo;
    }

    private static HouseDetailDto EntityToDto(HouseEntity entity)
    {
        return new HouseDetailDto(entity.Id, entity.Address, entity.Country, entity.Price, entity.Description, entity.Photo);
    }

    public async Task<HouseDetailDto> Add(HouseDetailDto houseDetailDto)
    {
        var entity = new HouseEntity();
        DtoToEntity(houseDetailDto, entity);
        _dbContext.Houses.Add(entity);
        await _dbContext.SaveChangesAsync();
        return EntityToDto(entity);
    }

    public async Task<HouseDetailDto> Update(HouseDetailDto houseDetailDto)
    {
        var entity = await _dbContext.Houses.FindAsync(houseDetailDto.Id);
        if (entity == null)
        {
            throw new ArgumentException($"Error updating house with ID {houseDetailDto.Id}.");
        }

        DtoToEntity(houseDetailDto, entity);
        // Mark the entity as modified since entity tracking is off
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return EntityToDto(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await _dbContext.Houses.FindAsync(id);
        if (entity == null)
        {
            throw new ArgumentException($"Error deleting house with ID {id}.");
        }

        _dbContext.Houses.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}