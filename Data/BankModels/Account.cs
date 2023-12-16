using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class Account
{
    public int Id { get; set; }

    public int Accounttype { get; set; }

    public int? Clientid { get; set; }

    public decimal Balance { get; set; }

    public DateTime Regdate { get; set; }

    public virtual Accounttype AccounttypeNavigation { get; set; } = null!;

    public virtual ICollection<Banktransaction> Banktransactions { get; set; } = new List<Banktransaction>();

    public virtual Client Client { get; set; } = null!;
}
