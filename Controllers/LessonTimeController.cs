using Microsoft.AspNetCore.Mvc;
using SzakdolgozatBackend.Dtos.Lesson;
using SzakdolgozatBackend.Dtos.LessonTime;
using SzakdolgozatBackend.Services;

namespace SzakdolgozatBackend.Controllers
{
    [Route("api/lesson-time")]
    [ApiController]
    public class LessonTimeController : ControllerBase
    {
        private ILessonTimeService _lessonTimeService;

        public LessonTimeController(ILessonTimeService lessonTimeService)
        {
            _lessonTimeService = lessonTimeService;
        }

        [HttpGet]
        public async Task<List<LessonTimeGetDto>> GetLessonTimes()
        {
            return await _lessonTimeService.GetAllLessonTimesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonTimeGetDto>> GetLessonTimeById(int id)
        {
            try
            {
                var lessonTime = await _lessonTimeService.GetLessonTimeByIdAsync(id);
                return Ok(lessonTime);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<LessonTimeGetDto>> CreateLessonTime([FromBody] LessonTimeCreateDto lessonTimeCreateDto)
        {
            try
            {
                await _lessonTimeService.CreateLessonTimeAsync(lessonTimeCreateDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Lesson time(s) created successfully.");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchLessonTime(int id, [FromBody] LessonTimePatchDto lessonTimePatchDto)
        {
            try
            {
                await _lessonTimeService.UpdateLessonTimeAsync(id, lessonTimePatchDto);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLessonTime(int id)
        {
            try
            {
                await _lessonTimeService.DeleteLessonTimeAsync(id);
                return Ok("Lesson time deleted successfully.");
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
