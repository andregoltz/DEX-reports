using Dapper;
using DEX.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DEX.Infra.Repository
{
    public class DEXRepository(IConfiguration configuration)
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");


        public async Task SaveDEXMeterAsync(DEXMeter meter)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                var parameters = new
                {
                    ID = meter.ID,
                    Machine = meter.Machine,
                    DEXDateTime = meter.DEXDateTime,
                    MachineSerialNumber = meter.MachineSerialNumber,
                    ValueOfPaidVends = meter.ValueOfPaidVends
                };
                await connection.ExecuteAsync("SaveDEXMeter", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SaveDEXLaneMeterAsync(DEXLaneMeter laneMeter)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                var parameters = new
                {
                    ID = laneMeter.ID,
                    DEXMeterID = laneMeter.DEXMeterID,
                    ProductIdentifier = laneMeter.ProductIdentifier,
                    Price = laneMeter.Price,
                    ValueOfPaidSales = laneMeter.ValueOfPaidSales,
                    NumberOfVends = laneMeter.NumberOfVends
                };
                await connection.ExecuteAsync("SaveDEXLaneMeter", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAll()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                await connection.ExecuteAsync("DeleteStoredData", commandType: CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
