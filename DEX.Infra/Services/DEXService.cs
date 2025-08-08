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

        public async Task<(DEXMeter dexMeter, List<DEXLaneMeter> dexLaneMeters)> ProcessDEXFileAsync(string dexFile, char machine)
        {
            var parsedDexFile = DEXHelper.ParseDexFile(dexFile);

            var dexMeter = new DEXMeter(Guid.NewGuid(), machine, parsedDexFile.ID5, parsedDexFile.ID1, parsedDexFile.VA1);

            var dexLaneMeters = new List<DEXLaneMeter>();

            foreach (var (ProductIdentifier, Price, NumberOfVends, ValueOfPaidSales) in parsedDexFile.PARecords)
            {
                dexLaneMeters.Add(new DEXLaneMeter(
                    Guid.NewGuid(),
                    dexMeter.ID,
                    ProductIdentifier,
                    Price,
                    NumberOfVends,
                    ValueOfPaidSales
                ));
            }

            await _repository.SaveDEXMeterAsync(dexMeter);
            await _repository.SaveDEXLaneMetersAsync(dexLaneMeters);

            return (dexMeter, dexLaneMeters);
        }
    }
}
