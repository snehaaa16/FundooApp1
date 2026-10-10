using System.Security.Claims;
using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.Models;

namespace Fundoo1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FundooController : ControllerBase
{
    private IUserBL _userBL;
    private IForgotPasswordBL _forgotPasswordBL;
    private IResetPasswordBL _resetPasswordBL;

    public FundooController(IUserBL _userBL, IForgotPasswordBL _forgotPasswordBL, IResetPasswordBL _resetPasswordBL)
    {
        this._userBL = _userBL;
        this._forgotPasswordBL = _forgotPasswordBL;
        this._resetPasswordBL = _resetPasswordBL;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ResponseModel<RegistrationModel>> RegisterUser(RegistrationModel registrationModel)
    {
        RegistrationModel response= await _userBL.RegisterUserBL(registrationModel);
        ResponseModel<RegistrationModel> model = new ResponseModel<RegistrationModel>();
        model.Data = response;
        model.Success = true;
        model.Message = "User Registered Successfull";
        return model;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var result = await _userBL.LoginUserBL(model);
        var response = new ResponseModel<UserResponseModel>();

        if (result == null)
        {
            response.Success = false;
            response.Message = "Invalid email or password";
            return Unauthorized(response);
        }

        response.Success = true;
        response.Message = "Login successful";
        response.Data = result;
        return Ok(response);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        string? email = User.FindFirst(ClaimTypes.Email)?.Value;
        string? name = User.FindFirst(ClaimTypes.Name)?.Value;

        return Ok(new { userId, email, name });
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequestModel req)
    {
        var res = await _forgotPasswordBL.ForgotPasswordAsyncBL(req);

        return Ok(res);
    }

    [HttpPost]
    [Route("ResetPassword")]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequestModel req)
    {
        var res = await _resetPasswordBL.ResetPasswordAsyncBL(req);
        if (res.Success)
        {
            return Ok(res);
        }
        return BadRequest(res);
    }
}

