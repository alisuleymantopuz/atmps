using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vpos.seb.domain
{
    /// <summary>
    /// Customer.
    /// </summary>
    public class Customer : IEntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; } 
    }
}
