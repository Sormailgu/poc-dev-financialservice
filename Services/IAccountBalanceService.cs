using System;
using FinancialService.Models;

namespace FinancialService.Services;

public interface IAccountBalanceService
{
    AccountBalance? GetAccountBalance(int userId);
    void UpdateBalance(int userId, decimal newBalance);
    void CreateAccountBalance(int userId, decimal initialBalance);
    void DisableAccountBalance(int userId);
}