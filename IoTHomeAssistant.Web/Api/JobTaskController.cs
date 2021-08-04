using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Mvc;

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
        public JobTask Get(int id)
        {
            return _jobTaskService.GetJobTask(id);
        }

        [HttpGet]
        public PageResponse<JobTask> Items(int pageNumber = 1, int pageSize = 10)
        {
            return _jobTaskService.GetPaggedList(
                new PageRequest() { 
                    PageNumber = pageNumber, 
                    PageSize = pageSize 
                });
        }

        [HttpPost]
        public void Add(JobTask request)
        {
            _jobTaskService.AddJobTask(request);
        }

        [HttpPut]
        public void Update(JobTask request)
        {
            _jobTaskService.UpdateJobTask(request);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _jobTaskService.RemoveJobTask(id);
        }
    }
}