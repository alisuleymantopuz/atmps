using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vpos.seb.domain
{
    /// <summary>
    /// Bank account.
    /// </summary>
    public class BankAccount : IEntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        public decimal Balance { get; set; }

        public string Number { get; set; }
        
        public string Name { get; set; }

        public long? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
