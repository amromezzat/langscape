using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.Interceptors
{
    public interface IAuditableEntitiesInterceptor : ISaveChangesInterceptor
    {  
    }
}