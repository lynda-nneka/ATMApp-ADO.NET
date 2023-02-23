using System;
using ATMApp.DAL.Enums;
using System.Collections.Generic;

namespace ATMApp.BLL
{
    public class TemporalFile
    {
        public IEnumerable<string> GetLanguageOptions()
        {
            string[] languageOptions = Enum.GetNames(typeof(LanguageOptions));
            return languageOptions;
        }

        public IEnumerable<string> GetWithdrawalOptions()
        {
            string[] withdrawOptions = Enum.GetNames(typeof(WithdrawalOptions));
            return withdrawOptions;
        }

    }
}

