using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class Administrator
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public string Admintype { get; set; } = null!;

    public DateTime Regdate { get; set; }
}
