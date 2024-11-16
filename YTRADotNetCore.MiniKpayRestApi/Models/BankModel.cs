namespace YTRADotNetCore.MiniKpayRestApi.Models
{
    public class BankModel
    {
        public int Id { get; set; }
        public string  BankName { get; set; }
        public string BankAccNo { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
