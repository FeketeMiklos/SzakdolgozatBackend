

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SzakdolgozatBackend.Dtos.Signature;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Services
{
    public interface ISignatureService
    {
        Task<List<SignatureGetDto>> GetAllSignaturesAsync();
        Task<SignatureGetDto?> GetSignatureByIdAsync(int id);
        Task<SignatureGetDto> CreateSignatureAsync(SignatureCreateDto signatureCreateDto);
        Task<SignatureGetDto> UpdateSignatureAsync(int id, SignaturePatchDto signaturePatchDto);
        Task DeleteSignatureAsync(int id);
    }
    public class SignatureService : ISignatureService
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;
        public SignatureService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SignatureGetDto> CreateSignatureAsync(SignatureCreateDto signatureCreateDto)
        {
            if (!await _dbContext.Students.AnyAsync(s => s.NeptunCode == signatureCreateDto.StudentNeptunCode))
            {
                throw new KeyNotFoundException("Student with given Neptun code does not exist!");
            }

            if (!await _dbContext.Lessons.AnyAsync(l => l.Id == signatureCreateDto.LessonId))
            {
                throw new KeyNotFoundException("Lesson with given Id does not exist!");
            }

            var signature = _mapper.Map<Signature>(signatureCreateDto);
            await _dbContext.Signatures.AddAsync(signature);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SignatureGetDto>(signature);
        }

        public async Task DeleteSignatureAsync(int id)
        {
            var signature = _dbContext.Signatures.FindAsync(id);
            if (signature == null)
            {
                throw new KeyNotFoundException("Signature with given Id does not exist!");
            }
            _dbContext.Signatures.Remove(signature.Result);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<SignatureGetDto>> GetAllSignaturesAsync()
        {
            var signatures = await _dbContext.Signatures.ToListAsync();
            return _mapper.Map<List<SignatureGetDto>>(signatures);
        }

        public async Task<SignatureGetDto?> GetSignatureByIdAsync(int id)
        {
            var signature = await _dbContext.Signatures.FindAsync(id);
            if (signature == null)
            {
                throw new KeyNotFoundException("Signature with given Id does not exist!");
            }
            return _mapper.Map<SignatureGetDto>(signature);
        }

        public async Task<SignatureGetDto> UpdateSignatureAsync(int id, SignaturePatchDto signaturePatchDto)
        {
            var signature = await _dbContext.Signatures.FindAsync(id);
            if (signature == null)
            {
                throw new KeyNotFoundException("Signature with given Id does not exist!");
            }

            if (signaturePatchDto.LessonId != null && !await _dbContext.Lessons.AnyAsync(l => l.Id == signaturePatchDto.LessonId))
            {
                throw new KeyNotFoundException("Lesson with given Id does not exist!");
            }

            if (signaturePatchDto.StudentNeptunCode != null && !await _dbContext.Students.AnyAsync(s => s.NeptunCode == signaturePatchDto.StudentNeptunCode))
            {
                throw new KeyNotFoundException("Lesson with given Id does not exist!");
            }

            _mapper.Map(signaturePatchDto, signature);
            signature.StudentNeptunCode = signaturePatchDto.StudentNeptunCode ?? signature.StudentNeptunCode;
            signature.LessonId = signaturePatchDto.LessonId ?? signature.LessonId;

            try
            {
                _dbContext.Signatures.Update(signature);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return _mapper.Map<SignatureGetDto>(signature);
        }
    }
}
