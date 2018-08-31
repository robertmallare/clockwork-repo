using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clockwork.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class TimezonesController : Controller
    {
        // GET: api/timezones
        [HttpGet]
        public IActionResult Get()
        {
            List<CustomTimezoneInfo> customTimezoneInfos = new List<CustomTimezoneInfo>();

            var localTimezone = new CustomTimezoneInfo()
            {
                Id = TimeZoneInfo.Local.Id,
                DisplayName = string.Format("{0} ({1})", TimeZoneInfo.Local.Id, TimeZoneInfo.Local.DisplayName)
            };

            foreach (var item in TimeZoneInfo.GetSystemTimeZones())
            {
                customTimezoneInfos.Add(new CustomTimezoneInfo()
                {
                    Id = item.Id,
                    DisplayName = string.Format("{0} ({1})", item.Id, item.DisplayName)
                });
            }

            return Ok(new
            {
                currentTimezone = localTimezone,
                timezoneInfos = customTimezoneInfos
            });
        }
    }
}
