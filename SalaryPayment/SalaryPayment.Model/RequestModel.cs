using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryPayment.Model
{
	public class RequestModel
	{
		public EmployeeSalaryPaymentModel Data { get; set; }
		public string OverTimeCalculator { get; set; }

	}
}
