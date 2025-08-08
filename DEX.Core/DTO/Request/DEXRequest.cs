namespace DEX.Core.DTO.Request
{
    public class DEXRequest
    {
        public char Machine { get; set; }
        public string DexFile { get; set; } = string.Empty;
    }
    public class DexData
    {
        public string ID1 { get; set; } = string.Empty;
        public DateTime ID5 { get; set; } = default;
        public decimal VA1 { get; set; } = 0m;
        public List<(string ProductIdentifier, decimal Price, int NumberOfVends, decimal ValueOfPaidSales)> PARecords { get; set; } = new();
    }
}
