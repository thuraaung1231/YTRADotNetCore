using DotetTrainingB5.shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using YTRADotNetCore.MiniKpayRestApi.Models;

namespace YTRADotNetCore.MiniKpayRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidhdrawController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=THURA\\MSSQLSERVER2;Initial Catalog=MINIKBZ;User Id=sa;Password=sasa;";
        private readonly DapperService _dapperService;

        public WidhdrawController()
        {
            _dapperService = new DapperService(_connectionString);

        }
        [HttpPost]
        public IActionResult widhdrawBalance(WithdrawModel withdraw) {
            string getBalanceUser = "SELECT Balance FROM Tbl_User WHERE MobileNumber = @MobileNumber AND DeleteFlag = 0";
            var currentBalance = _dapperService.Query<WithdrawModel>(getBalanceUser, new   WithdrawModel
            {
                MobileNumber = withdraw.MobileNumber

            }).FirstOrDefault();

         
            if (currentBalance is null)
            {
                return BadRequest("No Data Found");
            }


            if (currentBalance.Balance < withdraw.Balance)
            {
                return BadRequest("Insufficient balance.");
            }
            decimal newBalanceUser = currentBalance.Balance - withdraw.Balance;
     

            string updateBalanceUser = "UPDATE Tbl_User SET Balance = @Balance WHERE MobileNumber = @MobileNumber AND DeleteFlag = 0";
            int updateResultUser = _dapperService.Execute(updateBalanceUser, new WithdrawModel
            {
                Balance = newBalanceUser,
                MobileNumber = withdraw.MobileNumber
            });
            return Ok("withdrawsuccess");
         

         
          
        }
    }
}
