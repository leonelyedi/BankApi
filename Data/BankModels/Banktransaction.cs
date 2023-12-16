using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class Banktransaction
{
    public int Id { get; set; }

    public int Accountid { get; set; }

    public int Transactiontype { get; set; }

    public decimal Amount { get; set; }

    public int? Externalaccount { get; set; }

    public DateTime Regdate { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Transactiontype TransactiontypeNavigation { get; set; } = null!;
}
