using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atmps.domain
{
    /// <summary>
    /// Bank info.
    /// </summary>
    public class BankInfo : IEntityBase<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string VposUrl { get; set; }
        public bool IsOperated { get; set; }
        public virtual ICollection<BankIdentificationNumber> BankIdentificationNumbers { get; set; }
    }
}
