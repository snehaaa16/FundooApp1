using MessagingBuisnessLayer.Interface;
using MessagingModelLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessagingController.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MessagingController: ControllerBase
    {
		private readonly IEmailServiceBL _emailBL;

		public MessagingController(IEmailServiceBL _emailBL)
		{
			this._emailBL = _emailBL;
		}

		[HttpPost]
		[Route("send")]
		public async Task<IActionResult> SendEmailAsync(EmailRequestModel req)
		{
			var res = await _emailBL.SendEmailAsync(req);

			if (res.Success)
			{
				return Ok(res);
			}
			return BadRequest(res);
		}
	}
}

