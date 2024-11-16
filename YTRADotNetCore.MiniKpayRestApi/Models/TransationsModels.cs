namespace YTRADotNetCore.MiniKpayRestApi.Models
{
    public class TransationsModels
    {
      
        public string FromAcc { get; set; }
        public string ToAcc { get; set; }
        public string VoucherNo { get; set; }
        public decimal AmountOfFC { get; set; }
        public decimal AmountOfTC { get; set; }
        public string Description { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public bool DeleteFlag { get; set; }

    }
}
