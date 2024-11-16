using DotetTrainingB5.shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using YTRADotNetCore.MiniKpayRestApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace YTRADotNetCore.MiniKpayRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=THURA\\MSSQLSERVER2;Initial Catalog=MINIKBZ;User Id=sa;Password=sasa;";
        private readonly DapperService _dapperService;

        public TransferController()
        {
            _dapperService = new DapperService(_connectionString);

        }
        [HttpPost]
        public IActionResult CreateTransfer(TransferModel transfer)
        {
            string fromTransferQuery = "SELECT * FROM Tbl_User WHERE MobileNumber = @MobileNumberFC AND DeleteFlag = 0";
            string toTransferQuery = "SELECT * FROM Tbl_User WHERE MobileNumber = @MobileNumberTC AND DeleteFlag = 0";

            var fromTransfer = _dapperService.Query<UserModels>(fromTransferQuery, new
            {
                MobileNumberFC = transfer.MobileNumberFC,
            }).FirstOrDefault();

            var toTransfer = _dapperService.Query<UserModels>(toTransferQuery, new
            {
                MobileNumberTC = transfer.MobileNumberTC
            }).FirstOrDefault();

            if (fromTransfer is null)
                return BadRequest("Invalid Mobile Number.");

            if (toTransfer is null)
                return BadRequest("Invalid Mobile Number.");

            if (transfer.MobileNumberTC == transfer.MobileNumberFC)
                return BadRequest("Sender and receiver mobile numbers must be different.");

            if (fromTransfer.PinNo != transfer.PinNoFC)
                return Unauthorized("Invalid PIN.");

            if (fromTransfer.Balance < transfer.balance)
                return BadRequest("Insufficient balance.");


            fromTransfer.Balance -= transfer.balance;
            toTransfer.Balance += transfer.balance;


            string queryFC = $@"
            UPDATE [dbo].[Tbl_User]
   SET 

      
      [Balance]        =@Balance
      
      ,[UpdatedDate]    =@updatedDate
     
 WHERE [Id]=@id  ";


            var resultFC = _dapperService.Execute(queryFC, new UserModels
            { Id=fromTransfer.Id,
                MobileNumber = transfer.MobileNumberFC,
           updatedDate = DateTime.Now,
                Balance = fromTransfer.Balance,
              
            });
            string queryTC = $@"
            UPDATE [dbo].[Tbl_User]
   SET 

      
      [Balance]        =@Balance
      
      ,[UpdatedDate]    =@updatedDate
     
 WHERE [Id]=@id  ";


            var resultTC = _dapperService.Execute(queryTC, new UserModels
            {
                Id = toTransfer.Id,
                MobileNumber = transfer.MobileNumberTC,
                updatedDate = DateTime.Now,
                Balance = toTransfer.Balance,

            });
            string TransationQuery = $@"
INSERT INTO[dbo].[Tbl_Transation]
           ( [FromAcc]
             , [ToAcc]
             ,[AmountOfFC]
             , [AmountOfTC]
             , [description]
             ,[CreatedDate]
          
            ,[DeleteFlag]
)
     VALUES
            
          ( @FromAcc
           ,@ToAcc
           ,@AmountOfFC
           ,@AmountOfTC
           ,@Description
           ,@createdDate
         
            ,@DeleteFlag  
           )";
            var resultTransfer = _dapperService.Execute(TransationQuery, new TransationsModels
            {

                FromAcc = fromTransfer.CustomerAccNo,
                ToAcc = toTransfer.CustomerAccNo,
                AmountOfFC = transfer.balance,
                AmountOfTC = transfer.balance,
                Description = "transfer from " + transfer.FromAcc + " To " + transfer.ToAcc,
                createdDate = DateTime.Now,
                DeleteFlag = false,


            });

            return Ok(resultTC > 0 ? "Transfer completed successfully." : "An error occurred while processing the transfer");


        }
    }
}
