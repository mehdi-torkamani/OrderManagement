using System.Threading.Tasks;
using Orleans;

namespace OrderManagement.Interfaces
{
	public interface IStock : IGrainWithStringKey
    {
        Task Subscribe(IStockReporter observer);
        Task Unsubscribe(IStockReporter observer);
        Task Update(decimal change);
    }
}
