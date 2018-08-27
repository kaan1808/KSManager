using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KSManager_API.Dto;

namespace KSManager.Api
{
    public interface IKsManagerApi
    {
        string AccessToken { get; }

        Task Authenticate(string username, string password);

        Task Authenticate(string username, string password, CancellationToken cancellationToken);

        Task<IEnumerable<SmallPasswordEntry>> GetPasswordEntriesSmall();

        Task<IEnumerable<SmallPasswordEntry>> GetPasswordEntriesSmall(CancellationToken cancellationToken);
    }
}
