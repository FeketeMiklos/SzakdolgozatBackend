using Microsoft.AspNetCore.Mvc;
using SzakdolgozatBackend.Dtos.Signature;
using SzakdolgozatBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SzakdolgozatBackend.Controllers
{
    [Route("api/signature")]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly ISignatureService _signatureService;
        public SignatureController(ISignatureService signatureService)
        {
            _signatureService = signatureService;
        }

        [HttpGet]
        public async Task<List<SignatureGetDto>> GetAllSignatures()
        {
            return await _signatureService.GetAllSignaturesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SignatureGetDto>> GetSignatureById(int id)
        {
            try
            {
                var signature = await _signatureService.GetSignatureByIdAsync(id);
                return Ok(signature);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SignatureGetDto>> CreateSignature([FromBody] SignatureCreateDto signatureCreateDto)
        {
            try
            {
                await _signatureService.CreateSignatureAsync(signatureCreateDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Signature created successfully!");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSignature(int id, [FromBody] SignaturePatchDto signaturePatchDto)
        {
            try
            {
                await _signatureService.UpdateSignatureAsync(id, signaturePatchDto);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Signature updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSignature(int id)
        {
            try
            {
                await _signatureService.DeleteSignatureAsync(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Signature deleted successfully!");
        }
    }
}
