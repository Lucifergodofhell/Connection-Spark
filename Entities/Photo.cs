namespace API.Entities;

public class Photo
{
    public string Id { get; set; } = null!;
    public string Url { get; set; } = "";
    public string? PublicId { get; set; } = "";

    public Members Members { get; set; } = null!;

    public string MembersId { get; set; } = null!;
}