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
    public class UserController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=THURA\\MSSQLSERVER2;Initial Catalog=MINIKBZ;User Id=sa;Password=sasa;";
        private readonly DapperService _dapperService;
        public UserController()
        {
            _dapperService = new DapperService(_connectionString);
        }

        [HttpPost]

        public IActionResult CreateUser(UserModels user)
        {
            using (IDbConnection db = new SqlConnection())
            {
                string query = $@"INSERT INTO [dbo].[Tbl_User]
           ([Id]
           ,[FullName]
           ,[CustomerAccNo]
           ,[MobileNumber]
           ,[Balance]
           ,[PinNo]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[DeleteFlag])
     VALUES
           (  @Id            
           ,  @FullName
           ,  @CustomerAccNo
           ,  @MobileNumber     
           ,  @Balance      
           ,  @PinNo       
           ,  @createdDate  
           ,  @updatedDate  
           ,  @DeleteFlag )";

                int result = _dapperService.Execute(query, new UserModels
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    CustomerAccNo = user.CustomerAccNo,
                    MobileNumber = user.MobileNumber,
                    Balance = user.Balance,
                    PinNo = user.PinNo,
                    createdDate = DateTime.Now,
                    updatedDate= DateTime.Now,
                    DeleteFlag = false,

                });
                return Ok(result == 1 ? "Creating User SuccessFully" : " Creating User Failed");
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {

            string query = "select * from Tbl_User  WHERE DeleteFlag = 0 AND Id=@Id";
            var item = _dapperService.Query<UserModels>(query, new UserModels
            {
                Id = id
            }).FirstOrDefault();

            if (item is null)
            {
                return NotFound();
            };

            return Ok(item);


                }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserModels user)
        {
            string query = $@"
            UPDATE [dbo].[Tbl_User]
   SET 

        [FullName] =@FullName    
      ,[CustomerAccNo] =@CustomerAccNo
      ,[MobileNumber]  =@MobileNumber
      ,[Balance]        =@Balance
      ,[PinNo]          =@PinNo
      ,[CreatedDate]    =@createdDate
      ,[UpdatedDate]    =@updatedDate
      ,[DeleteFlag]     =@DeleteFlag
 WHERE [Id]=@id  " ;

            int result = _dapperService.Execute(query, new UserModels
            {
                Id=id,
                FullName = user.FullName,
                CustomerAccNo = user.CustomerAccNo,
                MobileNumber = user.MobileNumber,
                Balance = user.Balance,
                PinNo = user.PinNo,
                createdDate = user.createdDate,
                updatedDate = DateTime.Now,
                DeleteFlag = false,
            });

            return Ok(result == 1 ? "Updating Successful." : "Updating Fail");

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            string query = "UPDATE [dbo].[Tbl_User] SET DeleteFlag = 1 WHERE Id = @Id";
            int result = _dapperService.Execute(query, new UserModels { Id = id });

            return Ok(result == 0 ? "Failed Deleting User Account!" : "Successfully Deleted User");
        }
    }
}
