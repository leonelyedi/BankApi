using BankApi.Services;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using BankApi.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace BankApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountServices _accountServices;
    private readonly AccountTypeServices _accountTypeservices;
    private readonly ClientServices _clientServices;
    public AccountController(AccountServices accountServices,
                            AccountTypeServices accountTypeservices,
                            ClientServices clientServices)
    {
        this._accountServices = accountServices;
        this._accountTypeservices = accountTypeservices;
        this._clientServices = clientServices;
    }

    [HttpGet("getall")]
    public async Task<IEnumerable<AccountDtoOut>> Get()
    {
        return await _accountServices.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDtoOut>> GetById(int id)
    {
        var account = await _accountServices.GetByIdDto(id);

        if (account is null)
            return NotFound(new { message = $"El cliente con ID = {id} no existe." });

        return account;
    }
    [Authorize(Policy = "SuperAdmin")]
    [HttpPost("add")]
    public async Task<IActionResult> Create(AccountDtoIn account)
    {
        string validationResult = await ValidateAccount(account);
        if (!validationResult.Equals("Valid"))
           return BadRequest(new {message = validationResult});


        var newAccount = await _accountServices.Create(account);

        return CreatedAtAction(nameof(GetById), new { id = newAccount.Id }, newAccount);
    }

    [Authorize(Policy = "SuperAdmin")]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, AccountDtoIn account)
    {
        ///Personalizar respuesta al servidor
        if (id != account.Id)
            return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ({account.Id}) del cuerpo de la solicitud." });

        var accountUpdate = await _accountServices.GetById(id);

        if (accountUpdate is not null)
        {
            string validationResult = await ValidateAccount(account);
            if(!validationResult.Equals("Valid"))
              return BadRequest(new {message = validationResult});

            await _accountServices.Update(account);
            return NoContent();
        }
        else
        {
            return NotFound(new { message = $"El cliente con ID = {id} no existe." });

        }

    }
    
    [Authorize(Policy = "SuperAdmin")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {


        var accountDelete = await _accountServices.GetById(id);

        if (accountDelete is not null)
        {
            await _accountServices.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound(new { message = $"El cliente con ID = {id} no existe." });
            //ClientNotFound(id);   //3
        }

    }
//     {
//   "accounttype": 3,
//   "clientId": 3,
//   "balance": 600
//  }

    public async Task<string> ValidateAccount(AccountDtoIn account)
    {
        string result = "Valid";

        var accountType = await _accountTypeservices.GetById(account.Accounttype);

        if (accountType is null)
            result = $"El tipo de cuenta {account.Accounttype} no existe.";

        // tenemos que hacer queno sea nullo
        var clientID = account.Clientid.GetValueOrDefault();

        var client = await _clientServices.GetById(clientID);

        if (client is null)
            result = $"El cliente  {clientID} no existe.";

        return result;

    }

}