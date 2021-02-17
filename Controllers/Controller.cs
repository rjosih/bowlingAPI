using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace QuickStart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateController : ControllerBase
    {
        private IService service;

        private readonly ILogger<CalculateController> logger;
        private readonly IMapper<PlayerRequestModel, Player> playerMapper;
        private readonly IMapper<RequestModel, InputModel> inputModelMapper;
        private readonly IMapper<OutputModel, ResponseModel>  responseModelMapper;

        public CalculateController(ILogger<CalculateController> logger)
        {
            this.logger = logger;
            this.service = new Service(logger);
            this.playerMapper = new PlayerMapper();
            this.inputModelMapper = new InputModelMapper();
            this.responseModelMapper = new ResponseModelMapper();
        }

        [HttpPost]        
        public ResponseModel Post(RequestModel requestModel) 
        {
        this.logger.LogInformation($"{requestModel.ToString()}");
    
            try
            {
                var inputModel = this.inputModelMapper.Map(requestModel);
                var outputModel = this.service.CalculateScore(inputModel);
                this.logger.LogInformation($"{outputModel}");
                return this.responseModelMapper.Map(outputModel);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}