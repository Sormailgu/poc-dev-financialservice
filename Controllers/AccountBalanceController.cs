using Microsoft.AspNetCore.Mvc;
using FinancialService.Services;
using FinancialService.Dtos;
using FinancialService.Models;

using System;

namespace FinancialService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountBalanceController : ControllerBase
    {
        private readonly IAccountBalanceService _accountBalanceService;

        public AccountBalanceController(IAccountBalanceService accountBalanceService)
        {
            _accountBalanceService = accountBalanceService;
        }

        /// <summary>
        /// Gets the balance for a specific user.
        /// </summary>
        [HttpGet("{userId}")]
        public ActionResult<AccountBalanceResponseDto> GetAccountBalance(int userId)
        {
            var accountBalance = _accountBalanceService.GetAccountBalance(userId);
            if (accountBalance == null)
            {
                return NotFound();
            }
            var response = new AccountBalanceResponseDto(accountBalance);

            return Ok(response);
        }

        /// <summary>
        /// Updates the balance for a specific user.
        /// </summary>
        [HttpPut("{userId}/update")]
        public ActionResult<AccountBalanceResponseDto> UpdateBalance(int userId, [FromBody] UpdateBalanceRequestDto requestDto)
        {
            var accountBalance = _accountBalanceService.GetAccountBalance(userId);
            if (accountBalance == null)
            {
                return NotFound();
            }

            _accountBalanceService.UpdateBalance(userId, requestDto.NewBalance);

            // Retrieve the newly created account balance
            accountBalance = _accountBalanceService.GetAccountBalance(userId);
            var response = new AccountBalanceResponseDto(accountBalance);
            // Return the accountBalance as JSON response
            return Ok(response); // Changed from CreatedAtAction to Ok
        }

        /// <summary>
        /// Creates a new account balance for a specific user.
        /// </summary>
        [HttpPost("{userId}/create")]
        public ActionResult<AccountBalanceResponseDto> CreateAccountBalance(int userId, [FromBody] CreateAccountBalanceRequestDto requestDto)
        {
            if (requestDto == null || requestDto.InitialBalance < 0)
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                _accountBalanceService.CreateAccountBalance(userId, requestDto.InitialBalance);

                // Retrieve the newly created account balance
                var accountBalance = _accountBalanceService.GetAccountBalance(userId);

                // Return the accountBalance as JSON response
                var response = new AccountBalanceResponseDto(accountBalance);
                return Ok(response); // Changed from CreatedAtAction to Ok
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Disables a specific user's account balance.
        /// </summary>
        [HttpPut("{userId}/disable")]
        public IActionResult DisableAccountBalance(int userId)
        {
            var accountBalance = _accountBalanceService.GetAccountBalance(userId);
            if (accountBalance == null)
            {
                return NotFound();
            }

            _accountBalanceService.DisableAccountBalance(userId);
            return NoContent();
        }
    }
}