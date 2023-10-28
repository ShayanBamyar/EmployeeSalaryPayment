using OvetimePolicies;
using SalaryPayment.Model;
using SalaryPayment.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SalaryPayment.Service.EmployeePaymentModelValidator
{
	public interface IRequestModelValidator
	{
		void ValidateEmployeePaymentModel(EmployeeSalaryPaymentModel model);
		void ValidateOvetimePoliciesMethodName(string overtimeCalculator);
	}
	public class RequestModelValidator : IRequestModelValidator
	{
		public void ValidateEmployeePaymentModel(EmployeeSalaryPaymentModel model)
		{
			Type modelType = model.GetType();
			PropertyInfo[] properties = modelType.GetProperties();

			foreach (PropertyInfo property in properties)
			{
				PropertyInfo modelProperty = modelType.GetProperty(property.Name);
				if (modelProperty == null)
				{
					throw new ArgumentException(Messages.InvalidInputData);
				}

				object value = modelProperty.GetValue(model);
				if (value == null || value.GetType() != property.PropertyType)
				{
					throw new ArgumentException(Messages.InvalidInputData);
				}

				if (property.PropertyType == typeof(int))
				{
					if ((int)property.GetValue(model) <= 0)
					{
						throw new ArgumentException(Messages.InvalidInputDataValue);
					}
				}
				else if (property.PropertyType == typeof(string))
				{
					if (string.IsNullOrEmpty((string)property.GetValue(model)))
					{
						throw new ArgumentException(Messages.InvalidInputDataValue);
					}
				}
				else if (property.PropertyType == typeof(decimal))
				{
					if ((decimal)property.GetValue(model) <= 0)
					{
						throw new ArgumentException(Messages.InvalidInputDataValue);
					}
				}
				else if (property.PropertyType == typeof(DateTime))
				{
					if ((DateTime)property.GetValue(model) == DateTime.MinValue)
					{
						throw new ArgumentException(Messages.InvalidInputDataValue);
					}
				}
				else
				{
					throw new ArgumentException(Messages.InvalidInputData);
				}
			}
		}
		public void ValidateOvetimePoliciesMethodName(string overtimeCalculator)
		{
			MethodInfo method = typeof(IOvetimePolicies).GetMethod(overtimeCalculator);
			if (method == null)
			{
				throw new ArgumentException(Messages.InvalidMethodName);
			}
		}
	}
}
