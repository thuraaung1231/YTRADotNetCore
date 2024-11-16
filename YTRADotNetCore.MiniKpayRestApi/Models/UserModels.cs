namespace YTRADotNetCore.MiniKpayRestApi.Models
{
    public class UserModels
    {
        public int              Id                 { get; set; }
        public string?          FullName            { get; set; }
        public string           CustomerAccNo       { get; set; }
        public int              MobileNumber { get; set; }
        public decimal          Balance         { get; set; }
        public int               PinNo           { get; set; }
        public DateTime         createdDate         { get; set; }=DateTime.Now;
        public DateTime         updatedDate     { get; set; }
        public bool             DeleteFlag      { get; set; }



    }
}
