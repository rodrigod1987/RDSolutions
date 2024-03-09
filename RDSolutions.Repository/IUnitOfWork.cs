using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace RDSolutions.Repository;

public interface IUnitOfWork
{
    IDbContextTransaction CreateTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}
