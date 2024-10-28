using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging; // Ensure you have this using directive
using FinancialService.Models;

namespace FinancialService.Services;

public class AccountBalanceService : IAccountBalanceService
{
    private List<AccountBalance> _accountBalances;
    private readonly ILogger<AccountBalanceService> _logger; // Add logger

    public AccountBalanceService(ILogger<AccountBalanceService> logger) // Inject logger
    {
        _logger = logger;
        _accountBalances = new List<AccountBalance>
            {
                new AccountBalance { AccountId = Guid.NewGuid(), UserId = 1, Balance = 100.0m, LastUpdated = DateTime.UtcNow, IsActive = true },
                new AccountBalance { AccountId = Guid.NewGuid(), UserId = 2, Balance = 200.0m, LastUpdated = DateTime.UtcNow, IsActive = true }
            };
    }

    public AccountBalance? GetAccountBalance(int userId)
    {
        return _accountBalances.FirstOrDefault(ab => ab.UserId == userId && ab.IsActive);
    }

    public void UpdateBalance(int userId, decimal newBalance)
    {
        var accountBalance = GetAccountBalance(userId);
        if (accountBalance != null)
        {
            accountBalance.Balance = newBalance;
            accountBalance.LastUpdated = DateTime.UtcNow;
        }
    }

    public void CreateAccountBalance(int userId, decimal initialBalance)
    {
        if (_accountBalances.Any(ab => ab.UserId == userId))
        {
            throw new InvalidOperationException("Account already exists.");
        }

        var newAccountBalance = new AccountBalance
        {
            AccountId = Guid.NewGuid(),
            UserId = userId,
            Balance = initialBalance,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        // Log the new account balance being added
        _logger.LogInformation("Adding new account balance: {@NewAccountBalance}", newAccountBalance);
        
        _accountBalances.Add(newAccountBalance);
        
        // Log the updated list of account balances
        _logger.LogInformation("Current account balances: {@AccountBalances}", _accountBalances);
    }

    public void DisableAccountBalance(int userId)
    {
        var accountBalance = GetAccountBalance(userId);
        if (accountBalance != null)
        {
            accountBalance.IsActive = false;
            accountBalance.LastUpdated = DateTime.UtcNow;
        }
    }
}
