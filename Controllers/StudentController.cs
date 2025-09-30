using Microsoft.AspNetCore.Mvc;
using SzakdolgozatBackend.Dtos.Student;
using SzakdolgozatBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SzakdolgozatBackend.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<List<StudentGetDto>> GetAllStudents()
        {
            return await _studentService.GetAllStudentsAsync();
        }

        [HttpGet("{neptunCode}")]
        public async Task<ActionResult<StudentGetDto>> GetStudentById(string neptunCode)
        {
            try
            {
                var student = await _studentService.GetStudentByNeptunCodeAsync(neptunCode);
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<StudentGetDto>> CreateStudent([FromBody] StudentCreateDto studentCreateDto)
        {
            try
            {
                await _studentService.CreateStudentAsync(studentCreateDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Student created successfully.");
        }

        [HttpPut("{neptunCode}")]
        public async Task<IActionResult> PatchStudent(string neptunCode, [FromBody] StudentPatchDto studentPatchDto)
        {
            try
            {
                await _studentService.UpdateStudentAsync(neptunCode, studentPatchDto);
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

        [HttpDelete("{neptunCode}")]
        public async Task<IActionResult> DeleteStudent(string neptunCode)
        {
            try
            {
                await _studentService.DeleteStudentAsync(neptunCode);
                return Ok("Student deleted successfully!");
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
