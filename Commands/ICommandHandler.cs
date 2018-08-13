using System.Threading;
using System.Threading.Tasks;

namespace Commands
{
    public interface ICommandHandler<TCmd> where TCmd : Command
    {
        Task HandleAsync<TCmd>(TCmd command, CancellationToken cancellationToken = default(CancellationToken));
    }
}
