using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using Microsoft.EntityFrameworkCore;

namespace MafiCo.Infrastructure.Persistence.Repositories;

public class ProfileRepository : IProfileRepository {
    private readonly ApplicationContext _context;
    
    public ProfileRepository(ApplicationContext context) {
        _context = context;
    }
    
    public Profile Add(Profile profile) {
        return _context.Profiles.Add(profile).Entity!;
    }

    public void Update(Profile profile) {
        _context.Entry(profile).State = EntityState.Modified;
    }

    public async Task<Profile?> GetAsync(Guid id) {
        return await _context.Profiles.FindAsync(id);
    }

    public async Task<List<Profile>> GetAllAsync() {
        return await _context.Profiles.ToListAsync();
    }
}