namespace DEX.Core.Entities
{
    public class DEXLaneMeter(Guid iD, Guid dEXMeterID, string productIdentifier, decimal price, int numberOfVends, decimal valueOfPaidSales)
    {
        public Guid ID { get; set; } = iD;
        public Guid DEXMeterID { get; set; } = dEXMeterID;
        public string ProductIdentifier { get; set; } = productIdentifier;
        public decimal Price { get; set; } = price;
        public int NumberOfVends { get; set; } = numberOfVends;
        public decimal ValueOfPaidSales { get; set; } = valueOfPaidSales;
    }
}
