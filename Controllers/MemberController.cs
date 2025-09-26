using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class MemberController(IMemberRepository memberRepository) : BaseApiController
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Members>>> GetMembers()
    {
        return Ok(await memberRepository.GetMembersAsync());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Members>> GetMember(string id)
    {
        var member = await memberRepository.GetMemberByIdAsync(id);
        if (member == null) return NotFound("Member not found");
        return Ok(member);
    }

    [HttpGet("{id}/photos")]
    public async Task<ActionResult<IReadOnlyList<Photo>>> GetPhotosByMember(string id)
    {
        var photos = await memberRepository.GetPhotosByMemberAsync(id);
        return Ok(photos);
    }

}
