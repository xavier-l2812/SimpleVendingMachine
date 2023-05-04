using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVendingMachine.Models.Dtos
{
    public class TransactionQuery
    {
        public int? Skip { get; set; }
        public int Take { get; set; } = 20;
    }
}
