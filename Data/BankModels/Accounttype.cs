using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class Accounttype
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Regdate { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
