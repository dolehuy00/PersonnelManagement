using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ProjectDTO projectDTO)
        {
            var titleResponse = "Create an project.";
            try
            {
                var project = await _projectService.Add(projectDTO);
                return Ok(new ResponseObjectDTO<ProjectDTO>(titleResponse, [project]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] ProjectDTO projectDTO)
        {
            var titleResponse = "Update an project.";
            try
            {
                var project = await _projectService.Edit(projectDTO);
                return Ok(new ResponseObjectDTO<ProjectDTO>(titleResponse, [project]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var titleResponse = "Delete an project.";
            try
            {
                await _projectService.Delete(id);
                return Ok(new ResponseMessageDTO(titleResponse, [$"Delete project id = {id} successfully."]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMany([FromQuery] long[] id)
        {
            var titleResponse = "Delete many project.";
            try
            {
                var messages = await _projectService.DeleteMany(id);
                return Ok(new ResponseMessageDTO(titleResponse, messages));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var titleResponse = "Get an project.";
            try
            {
                var project = await _projectService.Get(id);
                return Ok(new ResponseObjectDTO<ProjectDTO>(titleResponse, [project]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }


        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] ProjectFilterDTO filterDTO)
        {
            var titleResponse = "Filter project.";
            try
            {
                var (results, totalPage, totalRecords) = await _projectService.FilterAsync(filterDTO);
                return Ok(new ResponseObjectDTO<ProjectDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chi tiết: {ex.StackTrace}");
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
