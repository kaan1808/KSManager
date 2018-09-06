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
        string AccessToken { get;}

        Task Authenticate(string username, string password);

        Task Authenticate(string username, string password, CancellationToken cancellationToken);

        Task<IEnumerable<SmallPasswordEntry>> GetPasswordEntriesSmall();

        Task<IEnumerable<SmallPasswordEntry>> GetPasswordEntriesSmall(CancellationToken cancellationToken);

        Task<PasswordEntry> GetPasswordEntry(Guid id);
        Task<PasswordEntry> GetPasswordEntry(Guid id,CancellationToken cancellationToken);

        Task<PasswordEntry> SavePasswordEntry(PasswordEntry entry);

        Task<PasswordEntry> SavePasswordEntry(PasswordEntry entry, CancellationToken cancellationToken);

        Task DeletePasswordEntry(Guid id);

        Task DeletePasswordEntry(Guid id, CancellationToken cancellationToken);


        Task<RegisterObject> Register(RegisterObject registerObject);

        Task<RegisterObject> Register(RegisterObject registerObject, CancellationToken cancellationToken);

        void ClearProperties();
    }
}
