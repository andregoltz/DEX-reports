using DEX.Core.Entities;
using DEX.Core.Helper;
using DEX.Infra.Repository;

namespace DEX.Core.Services
{
    public class DEXService
    {
        private readonly DEXRepository _repository;

        public DEXService(DEXRepository repository)
        {
            _repository = repository;
        }

        public async Task<(DEXMeter, DEXLaneMeter)> ProcessDEXFileAsync(string dexFile, char machine)
        {
            //Decode DEX File
            string machineSerialNumber = DEXHelper.DecodeID1(dexFile);
            DateTime dexDateTime = DEXHelper.DecodeID5(dexFile);
            decimal valueOfPaidVends = DEXHelper.DecodeVA1(dexFile);

            var (productIdentifier, price) = DEXHelper.DecodePA1(dexFile);
            var (numberOfVends, valueOfPaidSales) = DEXHelper.DecodePA2(dexFile);

            //Create DexMeter Data
            DEXMeter dEXMeter = new(Guid.NewGuid(), machine, dexDateTime, machineSerialNumber, valueOfPaidVends);

            //Create DexLaneMeter Data
            DEXLaneMeter dEXLaneMeter = new(Guid.NewGuid(), dEXMeter.ID, productIdentifier, price, numberOfVends, valueOfPaidSales);

            // Save data asynchronously
            await _repository.SaveDEXMeterAsync(dEXMeter);
            await _repository.SaveDEXLaneMeterAsync(dEXLaneMeter);

            return (dEXMeter, dEXLaneMeter);
        }
    }
}
