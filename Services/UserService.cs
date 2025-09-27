using AutoMapper;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SzakdolgozatBackend.Dtos.User;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Services
{
    public interface IUserService
    {
        Task<UserGetDto?> GetUserByIdAsync(int id);
        Task<List<UserGetDto>> GetAllUsersAsync();
        Task<UserGetDto> RegisterUserAsync(UserCreateDto createDto);
        Task<string> LoginAsync(UserLoginDto UserDTO);
        Task<UserGetDto> UpdateUserDataAsync(int id, UserPatchDto updateDto);
        Task DeleteUserAsync(int id);
        Task ChangePasswordAsync(int id, PasswordChangeDto passwordChangeDto);
        Task ForgottenPasswordChangeAsync(ForgottenPasswordDto forgottenPasswordChangeDto);
    }

    public class UserService : IUserService
    {
        private AppDbContext _dbContext;
        private IMapper _mapper;

        public UserService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserGetDto> RegisterUserAsync(UserCreateDto userCreateDto)
        {
            User? userWithGivenEmail = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userCreateDto.Email);

            if (userWithGivenEmail != null)
            {
                throw new Exception("User with this email already exists!");
            }

            if (userCreateDto.Name.Length > 100)
            {
                throw new ArgumentOutOfRangeException("Name must be less than 100 characters!");
            }

            if (userCreateDto.Password.Length < 8)
            {
                throw new Exception("Password must be at least 8 characters long!");
            }

            var user = _mapper.Map<User>(userCreateDto);
            user.PasswordHash = Argon2.Hash(userCreateDto.Password);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserGetDto>(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException("User with given Id does not exist!");
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(int id, PasswordChangeDto passwordChangeDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException("User with given Id does not exist!");
            }

            if (!Argon2.Verify(user.PasswordHash, passwordChangeDto.OldPassword))
            {
                throw new Exception("Old password is incorrect!");
            }

            if (passwordChangeDto.NewPassword.Length < 8)
            {
                throw new Exception("New password must be at least 8 characters long!");
            }

            user.PasswordHash = Argon2.Hash(passwordChangeDto.NewPassword);
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ForgottenPasswordChangeAsync(ForgottenPasswordDto forgottenPasswordChangeDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == forgottenPasswordChangeDto.Email);
            if (user == null)
            {
                throw new KeyNotFoundException("User with given this email does not exist!");
            }

            if (forgottenPasswordChangeDto.NewPassword.Length < 8)
            {
                throw new Exception("Password must be at least 8 characters long!");
            }

            user.PasswordHash = Argon2.Hash(forgottenPasswordChangeDto.NewPassword);
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserGetDto>> GetAllUsersAsync()
        {
            var users = await _dbContext.Users.ToListAsync();
            return _mapper.Map<List<UserGetDto>>(users);
        }

        public async Task<UserGetDto?> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException("User with given Id does not exist!");
            }
            return _mapper.Map<UserGetDto>(user);
        }

        public async Task<string> LoginAsync(UserLoginDto userDTO)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            if (user == null)
            {
                throw new KeyNotFoundException("User with given this email does not exist!");
            }

            if (!Argon2.Verify(user.PasswordHash, userDTO.Password))
            {
                throw new Exception("Password is incorrect!");
            }

            //change when JWT is added
            return "Login successful";
        }

        public async Task<UserGetDto> UpdateUserDataAsync(int id, UserPatchDto updateDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException("User with given Id does not exist!");
            }

            if (updateDto.Name != null)
            {
                if (updateDto.Name.Length > 100)
                {
                    throw new ArgumentOutOfRangeException("Name must be less than 100 characters!");
                }
            }

            if (updateDto.Email != null)
            {
                if (await _dbContext.Users.AnyAsync(u => u.Email == updateDto.Email))
                {
                    throw new Exception("User with given this email already exists!");
                }
            }

            _mapper.Map(updateDto, user);

            try
            {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _mapper.Map<UserGetDto>(user);
        }
    }
}
