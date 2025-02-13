using GenerateOTPApi.Models;
using GenerateOTPApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace GenerateOTPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : Controller
    {
        private readonly OtpService _otpService;

        public OtpController(OtpService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("generate")]
        public IActionResult GenerateOtp([FromBody] string userId)
        {
            var otp = _otpService.GenerateOtp(userId);
            return Ok(otp);
        }

        [HttpPost("validate")]
        public IActionResult ValidateOtp([FromBody] OtpValidationRequest request)
        {
            var isValid = _otpService.ValidateOtp(request.userId, request.UserOtp);

            if (isValid)
            {
                return Ok(new { message = "OTP is valid" });
            }
            return BadRequest(new { message = "Invalid OTP" });
        }

    }
}
