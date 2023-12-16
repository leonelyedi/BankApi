using BankApi.Services;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BankApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientServices _services;
    public ClientController(ClientServices service)
    {
        _services = service;
    }

    [HttpGet("getall")]
    public async Task<IEnumerable<Client>> Get()
    {
        return await _services.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetById(int id)
    {
        var client = await _services.GetById(id);

        if(client is null)
          return NotFound(new {message = $"El cliente con ID = {id} no existe."});
         /// ClientNotFound(id); //1

        return client;
    }
   [Authorize(Policy = "SuperAdmin")]
    [HttpPost("add")]
    public async Task<IActionResult> Create(Client client)
    {
        var newClient = await _services.Create(client);
        
        return CreatedAtAction(nameof(GetById), new {id = newClient.Id}, newClient);
    }


    [Authorize(Policy = "SuperAdmin")]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, Client client)
    {
        ///Personalizar respuesta al servidor
        if (id != client.Id)
            return BadRequest(new  {message = $"El ID = {id} de la URL no coincide con el ({client.Id}) del cuerpo de la solicitud."});
         
        var clienteUpdate = await _services.GetById(id);

        if(clienteUpdate is not null) 
        {
            await _services.Update(id, client);
            return NoContent();
        }
        else
        {
            return NotFound(new {message = $"El cliente con ID = {id} no existe."});
            ///ClientNotFound(id); //2
        }
        // var existingClient = _context.Clients.Find(id);
        // if(existingClient is null)
        //     return NotFound();

        // existingClient.Name = client.Name;
        // existingClient.Phonenumber = client.Phonenumber;
        // existingClient.Email = client.Email;

        // _context.SaveChanges();
        // return NoContent();
    }
    
    [Authorize(Policy = "SuperAdmin")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
       
      
        var clientDelete = await _services.GetById(id);

        if(clientDelete is not null) 
        {
           await _services.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound(new {message = $"El cliente con ID = {id} no existe."});
            //ClientNotFound(id);   //3
        }



    //   var existingClient = _context.Clients.Find(id);
    //   if (existingClient is null)
    //       return NotFound();

    //     _context.Clients.Remove(existingClient);
    //     _context.SaveChanges();
    //     return Ok();
    }

    // public NotFoundObjectResult ClientNotFound(int id)
    // {
    //   return NotFound(new {message = $"El cliente con ID = {id} no existe."});
    // }
}