using BankApi.Data;
using BankApi.Data.BankModels;
using BankApi.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services;

public class LoginServices
{
    //hacemos referencia al contexto inyectado al constructo
    private readonly BankContext context1;
    public LoginServices(BankContext context)
    {
        context1 = context;
    }

    public async Task<Administrator?> GetAdmin(AdminDto admin)
    {
        return await context1.Administrators.
                    SingleOrDefaultAsync(x => x.Email == admin.Email && x.Pwd == admin.Pwd);
    }
}