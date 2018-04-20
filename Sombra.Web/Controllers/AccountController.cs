using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests;
using Sombra.Web.Models;

namespace Sombra.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;


        public AccountController(IBus bus, IMapper mapper, IUserManager userManager)
        {
            _bus = bus;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(new EmailTemplateRequest());
        }

        [HttpPost]
        public IActionResult ForgotPassword(EmailTemplateRequest emailTemplateRequest)
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}