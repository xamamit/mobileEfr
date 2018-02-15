
using System;

namespace Acs.Domain.Models.RequestModels
{
    public class PaitentAuthModel : BaseModel
    {
        public PaitentAuthModel() { }

        public PaitentAuthModel(string accountNumber)
        {
            if (null == accountNumber)
            {
                throw new ArgumentNullException($"{nameof(accountNumber)} can not be null");
            }

            if (0 >= accountNumber.Length)
            {
                throw new ArgumentException($"{nameof(accountNumber)} can not contain an empty value");
            }

            AccountNumber = accountNumber.Trim();
        }

        public string AccountNumber { get; set; }
    }
}