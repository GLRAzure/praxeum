using System.Threading.Tasks;

namespace Praxeum.Domain
{
    public interface IEventPublisher
    {
        Task PublishAsync(
            string type,
            object data);

        Task PublishAsync(
            string[] types,
            object data);
    }
}