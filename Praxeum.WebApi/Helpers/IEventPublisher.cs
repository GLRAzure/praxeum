using System.Threading.Tasks;

namespace Praxeum.WebApi.Helpers
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