using DotetTrainingB5.shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using YTRADotNetCore.MiniKpayRestApi.Models;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace YTRADotNetCore.MiniKpayRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DdepositController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=THURA\\MSSQLSERVER2;Initial Catalog=MINIKBZ;User Id=sa;Password=sasa;";
        private readonly DapperService _dapperService;

        public DdepositController()
        {
            _dapperService = new DapperService(_connectionString);
          
        }
        [HttpPost]
        public IActionResult CreateDeposit(DepositModel user)
        {
         

            using (IDbConnection db = new SqlConnection())
            {
                string query = $@"UPDATE [dbo].[Tbl_User]
   SET 

        [FullName] =@FullName    
      ,[CustomerAccNo] =@CustomerAccNo
      ,[MobileNumber]  =@MobileNumber
      ,[Balance]        =@Balance
      ,[PinNo]          =@PinNo
    
      ,[UpdatedDate]    =@updatedDate
    
 WHERE  [FullName] =@FullName and [CustomerAccNo] =@CustomerAccNo and  [PinNo] =@PinNo";

                int result = _dapperService.Execute(query, new UserModels
                {

                    FullName = user.FullName,
                    CustomerAccNo = user.CustomerAccNo,
                    MobileNumber = user.MobileNumber,
                    Balance = user.Balance,
                    PinNo = user.PinNo,
                    updatedDate = DateTime.Now,
                 

                });
                return Ok(result == 1 ? "Deposit  SuccessFully" : " Deposit  Failed");
            }
         
        }
    }
}
