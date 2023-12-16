using BankApi.Data;
using BankApi.Data.BankModels;
using BankApi.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services;

public class AccountServices
{
    private readonly BankContext _context;

    public AccountServices(BankContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountDtoOut>> GetAll()
    {
        return await _context.Accounts.Select(a => new AccountDtoOut
        {
            Id = a.Id,
            AccountName = a.AccounttypeNavigation.Name,
            ClientName = a.Client != null ? a.Client.Name : "",
            Balance = a.Balance,
            Regdate = a.Regdate

        }).ToListAsync();
    }

     public async Task<AccountDtoOut?> GetByIdDto(int id)
    {
        return await _context.Accounts.Where(a => a.Id == id).Select(a => new AccountDtoOut
        {
            Id = a.Id,
            AccountName = a.AccounttypeNavigation.Name,
            ClientName = a.Client != null ? a.Client.Name : "",
            Balance = a.Balance,
            Regdate = a.Regdate

        }).SingleOrDefaultAsync();
    }

    public async Task<Account?> GetById(int id)
    {
        return await _context.Accounts.FindAsync(id);
    }

    public async Task<Account> Create(AccountDtoIn newAccountDTO)
    {
        var newAccount = new Account();

        newAccount.Accounttype = newAccountDTO.Accounttype;
        newAccount.Clientid = newAccountDTO.Clientid;
        newAccount.Balance = newAccountDTO.Balance;

        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();

        return newAccount;
    }

    public async Task Update(AccountDtoIn account)
    {
        var existingAccount = await GetById(account.Id);

        if (existingAccount is not null)
        {
            existingAccount.Accounttype = account.Accounttype;
            existingAccount.Clientid = account.Clientid;
            existingAccount.Balance = account.Balance;

            await _context.SaveChangesAsync();
            // return NoContent();
        }
    }

    public async Task Delete(int id)
    {
        var accountDelete = await GetById(id);

        if (accountDelete is not null)
        {
            _context.Accounts.Remove(accountDelete);

            await _context.SaveChangesAsync();
            // return NoContent();
        }
    }
}