﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sombra.Web.Infrastructure;
using Sombra.Web.Infrastructure.Messaging;
using Sombra.Web.ViewModels;
using Sombra.Web.ViewModels.Story;

namespace Sombra.Web.Controllers
{
    public class StoryController : Controller
    {
        private readonly ICachingBus _bus;
        private readonly IMapper _mapper;

        public StoryController(ICachingBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet("verhalen")]
        public IActionResult Index(SubdomainViewModel query)
        {
            // If not subdomain then random else by charity
            return View("Index");
        }

        [HttpGet]
        [Subdomain]
        [Route("verhalen/{url}")]
        public IActionResult Detail(StoryQuery query)
        {
            return View("Detail");
        }
    }
}
