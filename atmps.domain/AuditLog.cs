using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atmps.domain
{
    /// <summary>
    /// Audit log.
    /// </summary>
    public class AuditLog : IEntityBase<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string CardNumber { get; set; }
        public OperationType OperationType { get; set; }
        public virtual DateTime LogDate { get; set; }
        public bool OperationResult { get; set; }
    }
}
