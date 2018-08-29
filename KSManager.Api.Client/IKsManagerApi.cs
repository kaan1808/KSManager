using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KSManager.Api.Client.Model;


namespace KSManager.Api
{
    public interface IKsManagerApi
    {
        string AccessToken { get; }

        Task Authenticate(string username, string password);

        Task Authenticate(string username, string password, CancellationToken cancellationToken);

        Task<IEnumerable<SmallPasswordEntry>> GetPasswordEntriesSmall();

        Task<IEnumerable<SmallPasswordEntry>> GetPasswordEntriesSmall(CancellationToken cancellationToken);

        Task<PasswordEntry> GetPasswordEntry(Guid id);
        Task<PasswordEntry> GetPasswordEntry(Guid id,CancellationToken cancellationToken);

        Task UpdatePasswordEntry(PasswordEntry entry);
        Task UpdatePasswordEntry(PasswordEntry entry, CancellationToken cancellationToken);
    }
}
