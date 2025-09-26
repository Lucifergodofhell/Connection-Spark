namespace API.DTOs;

public class CreateExpenseDtos
{
    public string Description { get; set; } = "";
    public int Amount { get; set; } = 0;
    public string CurrencyCode { get; set; } = "";
    public int PaidBy { get; set; }
    public List<int> InvolvedUsers { get; set; } = new List<int>();
}