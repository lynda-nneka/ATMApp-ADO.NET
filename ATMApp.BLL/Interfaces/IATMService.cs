using ATMApp.DAL.Models;
using System.Threading.Tasks;

namespace ATMApp.DAL
{
    public interface IATMService
    {
       
        Task EnrollCustomerAsync(CustomerViewModel model);

        Task Withdraw();
        Task Transfer();
        Task CheckBalance();
        void Exit();
    }
}

