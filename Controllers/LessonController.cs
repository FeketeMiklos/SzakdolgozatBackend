using Microsoft.AspNetCore.Mvc;
using SzakdolgozatBackend.Dtos.Lesson;
using SzakdolgozatBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SzakdolgozatBackend.Controllers
{
    [Route("api/lesson")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<List<LessonGetDto>> GetAllLessons()
        {
            return await _lessonService.GetAllLessonsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonGetDto>> GetLessonById(int id)
        {
            try
            {
                var lesson = await _lessonService.GetLessonByIdAsync(id);
                return Ok(lesson);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<LessonGetDto>> CreateLesson([FromBody] LessonCreateDto lessonCreateDto)
        {
            try
            {
                await _lessonService.CreateLessonAsync(lessonCreateDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Lesson created successfully.");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchLesson(int id, [FromBody] LessonPatchDto lessonPatchDto)
        {
            try
            {
                await _lessonService.UpdateLessonAsync(id, lessonPatchDto);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _lessonService.DeleteLessonAsync(id);
                return Ok("Lesson deleted successfully.");
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
