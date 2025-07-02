using Dapper;
using FormIOProject.Areas.Identity.Data;
using FormIOProject.Areas.Identity.Model;
using FormIOProject.Areas.Identity.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormIOProject.Areas.Identity.Implementation
{
    public class DapperServices : IDapper  // Renamed to avoid class-name conflict
    {
        private readonly string _connectionString;
        private readonly ApplicationDbContext _context;

     
        public DapperServices(IConfiguration configuration, ApplicationDbContext context)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")?? string.Empty;
            _context = context;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task AddFormsTable(FormIO form)
        {
             var sql = @"
                INSERT INTO Forms (
                    Title, CreatedBy, ModifiedBy,
                    CreatedUtc, ModifiedUtc,
                    VersionId, Latest, FormFields
                ) VALUES (
                    @Title, @CreatedBy, @ModifiedBy,
                    @CreatedUtc, @ModifiedUtc,
                    @VersionId, @Latest, @FormFields
                )";
               

            using var conn = CreateConnection();
            conn.Open();

            // If FormIO has a schema and name:
            var parameters = new
            {
                form.Title,
                form.CreatedBy,
                form.ModifiedBy,
                form.CreatedUtc,
                form.ModifiedUtc,
                form.VersionId,
                form.Latest,
                form.FormFields,
                CreatedAt = DateTime.UtcNow
            };

            // Execute insert and get new ID
            form.Id = await conn.ExecuteScalarAsync<int>(sql, parameters);
        }
    }
}
