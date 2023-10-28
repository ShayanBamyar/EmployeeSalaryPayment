using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvetimePolicies
{
	public interface IOvetimePolicies
	{
		Task<decimal> CalcurlatorA(decimal basicSalary, decimal attractionRight);
		Task<decimal> CalcurlatorB(decimal basicSalary, decimal attractionRight);
		Task<decimal> CalcurlatorC(decimal basicSalary, decimal attractionRight);
	}
	public class OvetimePolicies : IOvetimePolicies
	{
		public Task<decimal> CalcurlatorA(decimal basicSalary, decimal attractionRight)
		{
			return Task.FromResult(basicSalary * 0.1m + attractionRight * 0.2m);
		}

		public Task<decimal> CalcurlatorB(decimal basicSalary, decimal attractionRight)
		{
			return Task.FromResult(basicSalary * 0.2m + attractionRight * 0.3m);
		}

		public Task<decimal> CalcurlatorC(decimal basicSalary, decimal attractionRight)
		{
			return Task.FromResult(basicSalary * 0.3m + attractionRight * 0.4m);
		}
	}	
}
