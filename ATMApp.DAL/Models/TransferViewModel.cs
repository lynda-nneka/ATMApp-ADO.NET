using System;
namespace ATMApp.DAL.Models
{
    public class TransferViewModel
    {
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public decimal Amount { get; set; }

    }

}