using Data_Access.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Invitation_Website.Controllers
{
    public class InvitationController : Controller
    {
        private readonly IPersonService _personService;

        public InvitationController(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<IActionResult> Index(string hash)
        {
            var person = await _personService.GetPersonByHash(hash);
            if (person == null) return Redirect("~/");

            return View(person);
        }

        [HttpGet]
        public IActionResult Invitation(string hash)
        {
            return View(hash);
        }
    }
}
