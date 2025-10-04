using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SzakdolgozatBackend.Dtos.Lesson;
using SzakdolgozatBackend.Dtos.Signature;
using SzakdolgozatBackend.Dtos.Student;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Services
{
    public interface IStudentService
    {
        Task<List<StudentGetDto>> GetAllStudentsAsync();
        Task<StudentGetDto?> GetStudentByNeptunCodeAsync(string neptunCode);
        Task<StudentGetDto> CreateStudentAsync(StudentCreateDto studentCreateDto);
        Task<StudentGetDto> UpdateStudentAsync(string neptunCode, StudentPatchDto studentPatchDto);
        Task DeleteStudentAsync(string neptunCode);
        Task<List<SignatureGetDto>?> GetSignaturesByStudentAsync(string neptunCode);
        Task<Student> StudentExists(string neptunCode);
    }

    public class StudentService : IStudentService
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;
        public StudentService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<StudentGetDto> CreateStudentAsync(StudentCreateDto studentCreateDto)
        {
            var existingStudent = await _dbContext.Students
                .AnyAsync(s => s.NeptunCode == studentCreateDto.NeptunCode);

            if (existingStudent)
            {
                throw new Exception("Student with this Neptune code already exists!");
            }

            if (studentCreateDto.NeptunCode.Length != 6)
            {
                throw new Exception("Neptun code must be exactly 6 characters long!");
            }

            if (studentCreateDto.Name.Length > 100)
            {
                throw new Exception("Student name must be 100 characters or less!");
            }

            var student = _mapper.Map<Student>(studentCreateDto);
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<StudentGetDto>(student);
        }

        public async Task DeleteStudentAsync(string neptunCode)
        {
            var student = await StudentExists(neptunCode);
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<StudentGetDto>> GetAllStudentsAsync()
        {
            var students = await _dbContext.Students.ToListAsync();
            return _mapper.Map<List<StudentGetDto>>(students);
        }

        public async Task<List<SignatureGetDto>?> GetSignaturesByStudentAsync(string neptunCode)
        {
            var student = await StudentExists(neptunCode);
            return _mapper.Map<List<SignatureGetDto>?>(student.Signatures);
        }

        public async Task<StudentGetDto?> GetStudentByNeptunCodeAsync(string neptunCode)
        {
            var student = await StudentExists(neptunCode);
            return _mapper.Map<StudentGetDto>(student);
        }

        public async Task<StudentGetDto> UpdateStudentAsync(string neptunCode, StudentPatchDto studentPatchDto)
        {
            var student = await StudentExists(neptunCode);

            if (studentPatchDto.Name != null && studentPatchDto.Name.Length > 100)
            {
                throw new Exception("Student name must be 100 characters or less!");
            }

            _mapper.Map(studentPatchDto, student);

            try
            {
                _dbContext.Students.Update(student);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return _mapper.Map<StudentGetDto>(student);
        }

        public async Task<Student> StudentExists(string neptunCode)
        {
            Student student = await _dbContext.Students.FindAsync(neptunCode);
            if (student == null)
            {
                throw new KeyNotFoundException("Student with given Neptun code does not exist!");
            }
            return student;
        }
    }
}
