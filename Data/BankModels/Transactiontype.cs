using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class Transactiontype
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Regdate { get; set; }

    public virtual ICollection<Banktransaction> Banktransactions { get; set; } = new List<Banktransaction>();
}
