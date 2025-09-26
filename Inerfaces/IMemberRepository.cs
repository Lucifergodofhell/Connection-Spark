using API.Entities;

namespace API.Interfaces;

public interface IMemberRepository
{
    public void UpdateMember(Members member);
    public Task<bool> SaveAllAsync();
    public Task<IReadOnlyList<Members>> GetMembersAsync();
    public Task<Members?> GetMemberByIdAsync(string id);
    public Task<IReadOnlyList<Photo>> GetPhotosByMemberAsync(string memberId);
    
}