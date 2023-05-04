using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVendingMachine.Models.Dtos
{
    public class TransactionDetailToAddDto
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}
