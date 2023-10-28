using Newtonsoft.Json;
using SalaryPayment.Model;
using SalaryPayment.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SalaryPayment.Service.FillEmployeeSalaryPaymentModel
{
	public interface IFillSalaryPaymentModel
	{
		Task<EmployeeSalaryPaymentModel> CreateEmployeeFromCustomData(string customData);
		Task<EmployeeSalaryPaymentModel> CreateEmployeeFromJson(string jsonInput);
		Task<EmployeeSalaryPaymentModel> CreateEmployeeFromXml(string xmlInput);
		Task<EmployeeSalaryPaymentModel> CreateEmployeeFromCsvData(string csvData);
	}

	public class FillSalaryPaymentModel : IFillSalaryPaymentModel
	{
		public Task<EmployeeSalaryPaymentModel> CreateEmployeeFromCustomData(string customData)
		{
			var model = FillModelFromCustomData(customData);
			return Task.FromResult(model);
		}

		public Task<EmployeeSalaryPaymentModel> CreateEmployeeFromJson(string jsonInput)
		{
			EmployeeSalaryPaymentModel model = JsonConvert.DeserializeObject<EmployeeSalaryPaymentModel>(jsonInput);
			return Task.FromResult(model);
		}

		public Task<EmployeeSalaryPaymentModel> CreateEmployeeFromXml(string xmlInput)
		{
			EmployeeSalaryPaymentModel model = FillModelFromXml(xmlInput);
			return Task.FromResult(model);
		}
		public Task<EmployeeSalaryPaymentModel> CreateEmployeeFromCsvData(string csvData)
		{
			EmployeeSalaryPaymentModel model = FillModelFromCsvData(csvData);
			return Task.FromResult(model);
		}

		private EmployeeSalaryPaymentModel FillModelFromCustomData(string data)
		{
			var entries = data.Split("\r\n")[1].Split('/');
			var order = data.Split("\r\n")[0].Split('/');

			if (entries.Length < 6 || order.Length < 6)
			{
				throw new ArgumentException(Messages.InvalidInputData);
			}

			var dictionary = new Dictionary<string, string>();
			for (int i = 0; i < order.Length; i++)
			{
				dictionary[order[i]] = entries[i];
			}

			var model = new EmployeeSalaryPaymentModel
			{
				FirstName = dictionary["FirstName"],
				LastName = dictionary["LastName"],
				BasicSalary = decimal.Parse(dictionary["BasicSalary"]),
				Allowance = decimal.Parse(dictionary["Allowance"]),
				Transportation = decimal.Parse(dictionary["Transportation"]),
				Date = DateTime.ParseExact(dictionary["Date"], "yyMMdd", null)
			};

			return model;
		}


		private EmployeeSalaryPaymentModel FillModelFromXml(string xmlInput)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlInput);
			if (xmlDoc.DocumentElement.ChildNodes.Count < 6)
			{
				throw new ArgumentException(Messages.InvalidInputData);
			}
			var model = new EmployeeSalaryPaymentModel
			{
				FirstName = xmlDoc.SelectSingleNode("Employee/FirstName").InnerText,
				LastName = xmlDoc.SelectSingleNode("Employee/LastName").InnerText,
				BasicSalary = decimal.Parse(xmlDoc.SelectSingleNode("Employee/BasicSalary").InnerText),
				Allowance = decimal.Parse(xmlDoc.SelectSingleNode("Employee/Allowance").InnerText),
				Transportation = decimal.Parse(xmlDoc.SelectSingleNode("Employee/Transportation").InnerText),
				Date = DateTime.ParseExact(xmlDoc.SelectSingleNode("Employee/Date").InnerText, "yyMMdd", null)
			};

			return model;
		}

		private EmployeeSalaryPaymentModel FillModelFromCsvData(string data)
		{
			var entries = data.Split('/');
			if (entries.Length < 6)
			{
				throw new ArgumentException(Messages.InvalidInputData);
			}

			var model = new EmployeeSalaryPaymentModel
			{
				FirstName = entries[0],
				LastName = entries[1],
				BasicSalary = decimal.Parse(entries[2]),
				Allowance = decimal.Parse(entries[3]),
				Transportation = decimal.Parse(entries[4]),
				Date = DateTime.ParseExact(entries[5], "yyMMdd", null)
			};

			return model;
		}
	}
}