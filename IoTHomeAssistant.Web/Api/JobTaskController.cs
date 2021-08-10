using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTaskController : ControllerBase
    {
        private readonly IJobTaskService _jobTaskService;

        public JobTaskController(IJobTaskService pluginService)
        {
            _jobTaskService = pluginService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JobTask> Get(int id)
        {
            return await _jobTaskService.GetJobTask(id);
        }

        [HttpGet]
        public PageResponse<JobTask> Items(int pageNumber = 1, int pageSize = 10)
        {
            return _jobTaskService.GetPaggedList(
                new PageRequest() { 
                    PageNumber = pageNumber, 
                    PageSize = pageSize 
                }).Result;
        }

        [HttpPost]
        public async Task Add(JobTask request)
        {
            await _jobTaskService.AddJobTask(request);
        }

        [HttpPut]
        public async Task Update(JobTask request)
        {
            await _jobTaskService.UpdateJobTask(request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        {
            await _jobTaskService.RemoveJobTask(id);
        }
    }
}