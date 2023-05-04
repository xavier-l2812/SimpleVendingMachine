using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVendingMachine.Models.Dtos
{
    public class AccountDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string VerificationCode { get; set; }
    }
}
