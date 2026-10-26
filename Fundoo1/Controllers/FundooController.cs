using System.Reflection;
using System.Security.Claims;
using BuisnessLayer.Interface;
using BuisnessLayer.Users.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using MediatR;
using ModelLayer.Models;

namespace Fundoo1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FundooController : ControllerBase
{
    private readonly ISender _sender;
    public FundooController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ResponseModel<UserResponseModel>> RegisterUser(RegistrationModel registrationModel)
    {
        CreateUserCommand command = new CreateUserCommand(registrationModel);
        UserResponseModel response = await _sender.Send(command);

        ResponseModel<UserResponseModel> model = new ResponseModel<UserResponseModel>();
        model.Data = response;
        model.Success = true;
        model.Message = "User Registered Successfull";
        return model;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        LoginCommand command = new LoginCommand(model);
        var result = await _sender.Send(command);
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
        var res = await _sender.Send(new ForgotPasswordCommand(req));

        return Ok(res);
    }

    [HttpPost]
    [Route("ResetPassword")]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequestModel req)
    {
        var res = await _sender.Send(new ResetPasswordCommand(req));
        if (res.Success)
        {
            return Ok(res);
        }
        return BadRequest(res);
    }
}

