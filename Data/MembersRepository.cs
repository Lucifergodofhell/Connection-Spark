using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext context;
    public MemberRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<Members?> GetMemberByIdAsync(string id)
    {
        return await context.Members.FindAsync(id);
    }

    public async  Task<IReadOnlyList<Members>> GetMembersAsync()
    {
        return await context.Members.ToListAsync();
    }

    public async Task<IReadOnlyList<Photo>> GetPhotosByMemberAsync(string memberId)
    {
        return await context.Members
                            .Where(p => p.Id == memberId)
                            .SelectMany(x=>x.Photos).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await Task.FromResult(context.SaveChanges() > 0);
    }

    public async  void UpdateMember(Members member)
    {
        context.Entry(member).State = EntityState.Modified;
        await Task.CompletedTask;
    }
}