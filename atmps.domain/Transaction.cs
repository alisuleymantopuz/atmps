using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atmps.domain
{
    /// <summary>
    /// Transaction.
    /// </summary>
    public class Transaction : IEntityBase<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public CardType CardType { get; set; }
        public TransactionType TransactionType { get; set; }
        public long? BankInfoId { get; set; }
        public virtual BankInfo BankInfo { get; set; }
        public decimal? TransactionFee { get; set; }
        public decimal? DepositAmount { get; set; }
        public decimal? WithdrawAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool OperationResult { get; set; }
    }
}
