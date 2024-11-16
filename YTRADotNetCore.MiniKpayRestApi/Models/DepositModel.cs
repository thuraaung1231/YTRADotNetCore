namespace YTRADotNetCore.MiniKpayRestApi.Models
{
    public class DepositModel
    {
        public int Id { get; set; } 
            public string FullName { get; set; }
        public string CustomerAccNo { get; set; }   
            public int MobileNumber { get; set; } 
            public int PinNo {  get; set; }
            public decimal Balance { get; set; }    
       
    }
}
