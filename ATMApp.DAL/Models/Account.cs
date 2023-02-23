using System;
namespace ATMApp.DAL.Models
{
    public class Account
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedTime { get; set; }
        
    }
}

