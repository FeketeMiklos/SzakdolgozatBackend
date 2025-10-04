using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SzakdolgozatBackend.Dtos.Lesson;
using SzakdolgozatBackend.Dtos.LessonTime;
using SzakdolgozatBackend.Dtos.Signature;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Services
{
    public interface ILessonService
    {
        Task<List<LessonGetDto>?> GetAllLessonsAsync();
        Task<LessonGetDto?> GetLessonByIdAsync(int id);
        Task<LessonGetDto> CreateLessonAsync(LessonCreateDto lessonCreateDto);
        Task<LessonGetDto> UpdateLessonAsync(int id, LessonPatchDto lessonPatchDto);
        Task DeleteLessonAsync(int id);
        Task<List<LessonGetDto>?> GetAllLessonsByTeacherAsync(int teacherId);
        Task<List<LessonTimeGetDto>?> GetLessonTimesForLessonAsync(int lessonId);
        Task<List<SignatureGetDto>?> GetSignaturesForLessonAsync(int lessonId);
    }

    public class LessonService : ILessonService
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;
        public LessonService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<LessonGetDto> CreateLessonAsync(LessonCreateDto lessonCreateDto)
        {
            if (await _dbContext.Lessons.AnyAsync(l => l.Code == lessonCreateDto.Code))
            {
                throw new Exception("Lesson with the given code already exists.");
            }

            if (!await _dbContext.Users.AnyAsync(u => u.Id == lessonCreateDto.UserId))
            {
                throw new KeyNotFoundException("User with given Id does not exist!");
            }

            if (lessonCreateDto.Name.Length > 100)
            {
                throw new Exception("Lesson name must be less than 100 characters!");
            }

            if (lessonCreateDto.Code.Length > 20)
            {
                throw new Exception("Lesson code must be less than 20 characters!");
            }

            var lesson = _mapper.Map<Lesson>(lessonCreateDto);

            await _dbContext.Lessons.AddAsync(lesson);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<LessonGetDto>(lesson);
        }

        public async Task DeleteLessonAsync(int id)
        {
            var lesson = await _dbContext.Lessons.FindAsync(id);
            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson with given Id does not exist!");
            }
            _dbContext.Remove(lesson);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LessonGetDto>?> GetAllLessonsAsync()
        {
            var lessons = await _dbContext.Lessons.ToListAsync();
            return _mapper.Map<List<LessonGetDto>?>(lessons);
        }

        public async Task<List<LessonGetDto>?> GetAllLessonsByTeacherAsync(int teacherId)
        {
            var user = await _dbContext.Users.Include(u => u.Lessons).FirstOrDefaultAsync(u=> u.Id == teacherId);
            if (user == null)
            {
                throw new KeyNotFoundException("User with given Id does not exist!");
            }
            
            return _mapper.Map<List<LessonGetDto>?>(user.Lessons);
        }

        public async Task<LessonGetDto?> GetLessonByIdAsync(int id)
        {
            var lesson = await _dbContext.Lessons.FindAsync(id);
            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson with given Id does not exist!");
            }
            return _mapper.Map<LessonGetDto>(lesson);
        }

        public async Task<List<LessonTimeGetDto>?> GetLessonTimesForLessonAsync(int lessonId)
        {
            var lesson = await _dbContext.Lessons.Include(l => l.LessonTimes).FirstOrDefaultAsync(l => l.Id == lessonId);
            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson with given Id does not exist!");
            }

            return _mapper.Map<List<LessonTimeGetDto>?>(lesson.LessonTimes);
        }

        public async Task<List<SignatureGetDto>?> GetSignaturesForLessonAsync(int lessonId)
        {
            var lesson = await _dbContext.Lessons.Include(l => l.Signatures).FirstOrDefaultAsync(l => l.Id == lessonId);
            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson with given Id does not exist!");
            }

            return _mapper.Map<List<SignatureGetDto>?>(lesson.Signatures);
        }

        public async Task<LessonGetDto> UpdateLessonAsync(int id, LessonPatchDto lessonPatchDto)
        {
            var lesson = await _dbContext.Lessons.FindAsync(id);
            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson with given Id does not exist!");
            }

            if (lessonPatchDto.Name != null && lessonPatchDto.Name.Length > 100)
            {
                throw new Exception("Lesson name must be less than 100 characters!");
            }

            _mapper.Map(lessonPatchDto, lesson);

            try
            {
                _dbContext.Lessons.Update(lesson);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return _mapper.Map<LessonGetDto>(lesson);
        }
    }
}
