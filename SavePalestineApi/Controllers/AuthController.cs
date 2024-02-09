using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavePalestineApi.Models;
using SavePalestineApi.Services;
using Stripe;

namespace SavePalestineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(LoginUser user, string role)
        {
            if (await _authService.RegisterUser(user, role))
            {

                await _authService.Login(user);
                var tokenString = new { accessToken= _authService.GenerateTokenString(user) };
                return Ok(tokenString);
                
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await _authService.Login(user))
            {
                var tokenString = new { accessToken = _authService.GenerateTokenString(user) };
                return Ok(tokenString);
            }
            return BadRequest();
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentIntentCreateRequest request)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = request.Currency,
            });

            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }
    }
}
