// using System;
// using System.Collections.Generic;
// using System.Text.Json.Serialization;

namespace BankApi.Data.DTOs;

public partial class AccountDtoIn
{

    public int Id { get; set; }

    public int Accounttype { get; set; }
    public int? Clientid { get; set; }

    public decimal Balance { get; set; }

    
}
