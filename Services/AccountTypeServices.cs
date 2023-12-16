using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services;

public class AccountTypeServices
{
    private readonly BankContext _context;

    public AccountTypeServices(BankContext context)
    {
        _context = context;
    }

  

    public async Task<Accounttype?> GetById(int id)
    {
        return await _context.Accounttypes.FindAsync(id);
    }
}

//    public async Task<Account> Create (Account newAccount)
//    {
//         _context.Accounts.Add(newAccount);
//         await _context.SaveChangesAsync();

//         return newAccount;
//    }

//    public async Task Update( int id, Account account)
//    {
//     var existingAccount = await GetById(id);

//         if (existingAccount is not null)
//         {
//             existingAccount.Accounttype = account.Accounttype;
//             existingAccount.Clientid = account.Clientid;
//             existingAccount.Balance = account.Balance;

//             await _context.SaveChangesAsync();
//         // return NoContent();
//         }
//    }

//    public async Task Delete(int id)
//    {
//     var accountDelete = await GetById(id);

//         if (accountDelete is not null)
//         {
//            _context.Accounts.Remove(accountDelete);

//             await _context.SaveChangesAsync();
//         // return NoContent();
//         }
//    }
// }