namespace YTRADotNetCore.MiniKpayRestApi.Models
{
    public class TransferModel
    {
      
    

        public string FromAcc { get; set; }
        public int PinNoFC { get; set; }
        public string ToAcc { get; set; }
        public decimal balance { get; set; }
        public decimal AmountOfFC { get; set; }
        public decimal AmountOfTC { get; set; }
        public int MobileNumberFC { get; set; }
        public int MobileNumberTC { get; set; }
    }
}
