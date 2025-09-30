using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SzakdolgozatBackend.Dtos.LessonTime;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Services
{
    public interface ILessonTimeService
    {
        Task<List<LessonTimeGetDto>> GetAllLessonTimesAsync();
        Task<LessonTimeGetDto?> GetLessonTimeByIdAsync(int id);
        Task<LessonTimeGetDto> CreateLessonTimeAsync(LessonTimeCreateDto lessonTimeCreateDto);
        Task<LessonTimeGetDto> UpdateLessonTimeAsync(int id, LessonTimePatchDto lessonTimePatchDto);
        Task DeleteLessonTimeAsync(int id);
    }
    public class LessonTimeService : ILessonTimeService
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;
        public LessonTimeService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<LessonTimeGetDto> CreateLessonTimeAsync(LessonTimeCreateDto lessonTimeCreateDto)
        {
            if (lessonTimeCreateDto.EndTime < lessonTimeCreateDto.StartTime)
            {
                throw new Exception("End time cannot be before the start time!");
            }

            if (lessonTimeCreateDto.Date < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Cannot add date before today!");
            }

            if (lessonTimeCreateDto.StartTime < TimeOnly.FromDateTime(DateTime.Now) && lessonTimeCreateDto.Date == DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Cannot add start time in the past!");
            }

            if (lessonTimeCreateDto.EndTime < TimeOnly.FromDateTime(DateTime.Now) && lessonTimeCreateDto.Date == DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Cannot add end time in the past!");
            }

            var lesson = await _dbContext.Lessons.FindAsync(lessonTimeCreateDto.LessonId);
            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson with given Id does not exist!");
            }

            var lessonTime = _mapper.Map<LessonTime>(lessonTimeCreateDto);
            await _dbContext.LessonTimes.AddAsync(lessonTime);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<LessonTimeGetDto>(lessonTime);
        }

        public async Task DeleteLessonTimeAsync(int id)
        {
            var lessonTime = await _dbContext.LessonTimes.FindAsync(id);
            if (lessonTime == null)
            {
                throw new KeyNotFoundException("Lesson time with given Id does not exist!");
            }
            _dbContext.LessonTimes.Remove(lessonTime);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LessonTimeGetDto>> GetAllLessonTimesAsync()
        {
            return _mapper.Map<List<LessonTimeGetDto>>(await _dbContext.LessonTimes.ToListAsync());
        }

        public async Task<LessonTimeGetDto?> GetLessonTimeByIdAsync(int id)
        {
            var lessonTime = await _dbContext.LessonTimes.FindAsync(id);
            if (lessonTime == null)
            {
                throw new KeyNotFoundException("Lesson time with given Id does not exist!");
            }
            return _mapper.Map<LessonTimeGetDto>(lessonTime);
        }

        public async Task<LessonTimeGetDto> UpdateLessonTimeAsync(int id, LessonTimePatchDto lessonTimePatchDto)
        {
            var lessonTime = await _dbContext.LessonTimes.FindAsync(id);

            if (lessonTime == null)
            {
                throw new KeyNotFoundException("Lesson time with given Id does not exist!");
            }

            // Validation fot start and end time
            if (lessonTimePatchDto.StartTime != null && lessonTimePatchDto.EndTime != null && lessonTimePatchDto.EndTime < lessonTimePatchDto.StartTime)
            {
                throw new Exception("End time cannot be before the start time!");
            }

            if (lessonTimePatchDto.StartTime != null && lessonTime.EndTime < lessonTimePatchDto.StartTime)
            {
                throw new Exception("End time cannot be before the start time!");
            }

            if (lessonTimePatchDto.EndTime != null && lessonTimePatchDto.EndTime < lessonTime.StartTime)
            {
                throw new Exception("End time cannot be before the start time!");
            }

            //validation for date and start&end times
            if (lessonTimePatchDto.Date != null && lessonTimePatchDto.Date < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Cannot add date before today!");
            }

            if (lessonTimePatchDto.StartTime != null && lessonTimePatchDto.StartTime < TimeOnly.FromDateTime(DateTime.Now) && lessonTimePatchDto.Date == DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Cannot add start time in the past!");
            }

            if (lessonTimePatchDto.EndTime != null && lessonTimePatchDto.EndTime < TimeOnly.FromDateTime(DateTime.Now) && lessonTimePatchDto.Date == DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Cannot add end time in the past!");
            }

            if (lessonTimePatchDto.LessonId != null)
            {
                var lesson = await _dbContext.Lessons.FindAsync(lessonTimePatchDto.LessonId);
                if (lesson == null)
                {
                    throw new KeyNotFoundException("Lesson with given Id does not exist!");
                }
            }

            _mapper.Map(lessonTimePatchDto, lessonTime);
            try
            {
                _dbContext.LessonTimes.Update(lessonTime);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return _mapper.Map<LessonTimeGetDto>(lessonTime);
        }
    }
}
