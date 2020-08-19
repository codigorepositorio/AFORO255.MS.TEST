using AFORO255.MS.TEST.Security.Model;
using System.Collections.Generic;

namespace AFORO255.MS.TEST.Security.Service
{
    public interface IAccessService
    {
        IEnumerable<Access> GetAll();
        bool Validate(string userName, string password);
    }
}
