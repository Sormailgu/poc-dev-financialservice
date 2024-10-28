namespace FinancialService.Models;

public class AccountBalance
{
    public Guid AccountId { get; set; }
    public int UserId { get; set; }
    public decimal Balance { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool IsActive { get; set; }
}
