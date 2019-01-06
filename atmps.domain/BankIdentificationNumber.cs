using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atmps.domain
{
    /// <summary>
    /// BIN.
    /// </summary>
    public class BankIdentificationNumber : IEntityBase<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string BIN { get; set; }
        public long? BankInfoId { get; set; }
        public virtual BankInfo BankInfo { get; set; }
    }
}
