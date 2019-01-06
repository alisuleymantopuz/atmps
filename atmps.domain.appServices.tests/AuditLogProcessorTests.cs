using System.Collections.Generic;
using System.Linq;
using atmps.domain.infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    public class AuditLogProcessorTests
    {
        Mock<AppDbContext> _appDbContext = new Mock<AppDbContext>();

        /// <summary>
        /// Processes the should work with correct parameters.
        /// </summary>
        [Fact]
        public void Process_ShouldWorkWithCorrectParameters()
        {
            var auditLogs = (new List<AuditLog>()).AsQueryable();
            var auditLogsDbSet = new Mock<DbSet<AuditLog>>();
            auditLogsDbSet.As<IQueryable<AuditLog>>().Setup(m => m.Provider).Returns(auditLogs.Provider);
            auditLogsDbSet.As<IQueryable<AuditLog>>().Setup(m => m.Expression).Returns(auditLogs.Expression);
            auditLogsDbSet.As<IQueryable<AuditLog>>().Setup(m => m.ElementType).Returns(auditLogs.ElementType);
            auditLogsDbSet.As<IQueryable<AuditLog>>().Setup(m => m.GetEnumerator()).Returns(auditLogs.GetEnumerator());
            _appDbContext.Setup(m => m.AuditLogs).Returns(auditLogsDbSet.Object);

            var auditLogProcessor = new AuditLogProcessor(_appDbContext.Object);
            auditLogProcessor.Process(It.IsAny<string>(), It.IsAny<OperationType>(), It.IsAny<bool>());

            auditLogsDbSet.Verify(x => x.Add(It.IsAny<AuditLog>()), Times.Once);
            _appDbContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
