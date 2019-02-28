using BPOSolution.Entity;
using BPOSolution.Services;
using BPOSolution.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BPOSolution.Controllers
{
    [Produces("application/json")]
    [Route("apply")]
    public class BPOClientController : Controller
    {
        private DataContext _context;
        private IBPOClientRegistration _register;
        private ISendEmail _mail;


        public BPOClientController( IBPOClientRegistration register, ISendEmail mail)
        {
            _register = register;
            _mail = mail;
        }


        [HttpPost("submit")]
        public async Task<IActionResult> SubmitAsync([FromBody] BPOClientViewModel clientVM)
        {
            if (!ModelState.IsValid)
            {
                return Json(BadRequest(ModelState));
            }

            try
            {
                var client = await _register.Register(clientVM);
                _mail.Send(client);
                
                return Json(StatusCode(201));
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}