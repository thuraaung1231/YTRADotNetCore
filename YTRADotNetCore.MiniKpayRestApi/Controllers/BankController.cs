using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using DotetTrainingB5.shared;
using System.Data.SqlClient;
using System.Data;
using YTRADotNetCore.MiniKpayRestApi.Models;

namespace YTRADotNetCore.MiniKpayRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=THURA\\MSSQLSERVER2;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=sasa;";
      private readonly DapperService _dapperService;
        public BankController()
        {
            _dapperService = new DapperService(_connectionString);
        }
        

        }


    }

