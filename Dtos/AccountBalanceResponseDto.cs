using FinancialService.Models;

namespace FinancialService.Dtos;

public class AccountBalanceResponseDto
{
    public Guid AccountId { get; set; }
    public int UserId { get; set; }
    public decimal Balance { get; set; }
    public DateTime LastUpdated { get; set; }

    public AccountBalanceResponseDto(AccountBalance accountBalance)
    {
        AccountId = accountBalance.AccountId;
        UserId = accountBalance.UserId;
        Balance = accountBalance.Balance;
        LastUpdated = accountBalance.LastUpdated;
    }
}