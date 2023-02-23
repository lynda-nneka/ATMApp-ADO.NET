using ATMApp.DAL.Models;
using System.Threading.Tasks;

namespace ATMApp.DAL
{
    public interface IATMService
    {
       
        Task EnrollCustomerAsync(CustomerViewModel model);
        
        void Withdraw(WithdrawViewModel model);
        void Transfer(TransferViewModel model);
        Task CheckBalance();
        void Exit();
    }
}

