using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KSManager.Api
{
    public interface IKsManagerApi
    {


        Task Authenticate(string username, string password);

        Task Authenticate(string username, string password, CancellationToken cancellationToken);
    }
}
