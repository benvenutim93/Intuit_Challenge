using Microsoft.EntityFrameworkCore;

namespace Intuit.EF
{
    public partial class IntuitContext
    {

        private string _connectionString;

        public IntuitContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseSqlServer(_connectionString);
  
    }
}
