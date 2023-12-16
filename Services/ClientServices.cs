using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services;

public class ClientServices
{
    private readonly BankContext _context;

    public ClientServices(BankContext context)
    {
        _context = context;
    }

   public async Task<IEnumerable<Client>> GetAll()
   {
    return await _context.Clients.ToListAsync();
   }

    public async Task<Client?> GetById(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

   public async Task<Client> Create (Client newClient)
   {
        _context.Clients.Add(newClient);
        await _context.SaveChangesAsync();

        return newClient;
   }

   public async Task Update( int id, Client client)
   {
    var existingClient = await GetById(id);

        if (existingClient is not null)
        {
            existingClient.Name = client.Name;
            existingClient.Phonenumber = client.Phonenumber;
            existingClient.Email = client.Email;

            await _context.SaveChangesAsync();
        // return NoContent();
        }
   }

   public async Task Delete(int id)
   {
    var ClientDelete = await GetById(id);

        if (ClientDelete is not null)
        {
           _context.Clients.Remove(ClientDelete);

            await _context.SaveChangesAsync();
        // return NoContent();
        }
   }
}