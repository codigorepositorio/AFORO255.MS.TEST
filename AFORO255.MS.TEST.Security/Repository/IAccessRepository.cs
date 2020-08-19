using AFORO255.MS.TEST.Security.Model;
using System.Collections.Generic;

namespace AFORO255.MS.TEST.Security.Repository
{
    public interface IAccessRepository
    {

        IEnumerable<Access> GetAll();

    }
}
