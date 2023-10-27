using Microsoft.EntityFrameworkCore;
using SalaryPayment.Model;

namespace SalaryPayment.Repository.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }
        public DbSet<EmployeeSalaryPaymentModel> EmployeeSalaryPayment { get; set; }
    }
}
