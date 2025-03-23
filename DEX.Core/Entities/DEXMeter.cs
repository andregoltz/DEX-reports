namespace DEX.Core.Entities
{
    public class DEXMeter(Guid iD, char machine, DateTime dEXDateTime, string machineSerialNumber, decimal valueOfPaidVends)
    {
        public Guid ID { get; set; } = iD;
        public char Machine { get; set; } = machine;
        public DateTime DEXDateTime { get; set; } = dEXDateTime;
        public string MachineSerialNumber { get; set; } = machineSerialNumber;
        public decimal ValueOfPaidVends { get; set; } = valueOfPaidVends;
    }
}
