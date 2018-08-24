using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSManager.Api
{
    public interface IKsManagerApi
    {
        bool Authenticate(string username, string password);


    }
}
