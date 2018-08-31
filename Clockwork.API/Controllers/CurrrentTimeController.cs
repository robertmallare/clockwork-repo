using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Clockwork.API.Common;
using System.Linq;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class CurrentTimeController : Controller
    {
        // GET api/currenttime
        [HttpGet]
        public async Task<IActionResult> Get(string timezoneId, bool didUserRequest = false, int page = 1, int pageSize = 10, string sort = "desc")
        {
            PaginatedList<CurrentTimeQuery> currentTimeQueriesData;
            var currentTimeQueries = new List<CurrentTimeQuery>();

            TimeZoneInfo targetTimezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId ?? TimeZoneInfo.Local.Id);

            var utcTime = DateTime.UtcNow;
            var serverTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, targetTimezone);
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var returnVal = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = serverTime
            };

            using (var db = new ClockworkContext())
            {
                if (didUserRequest)
                {
                    db.CurrentTimeQueries.Add(returnVal);
                    var count = db.SaveChanges();

                    Console.WriteLine("{0} records saved to database", count);
                    Console.WriteLine();
                }

                var tempData = db.CurrentTimeQueries.AsNoTracking();

                switch (sort)
                {
                    case "desc":
                        tempData = tempData.OrderByDescending(r => r.CurrentTimeQueryId);
                        break;
                    case "asc":
                        tempData = tempData.OrderBy(r => r.CurrentTimeQueryId);
                        break;
                    default:
                        break;
                }

                currentTimeQueriesData = await PaginatedList<CurrentTimeQuery>.CreateAsync(tempData, page, pageSize);
            }

            return Ok(new
            {
                timeRequested = returnVal,
                currentTimeQueries = currentTimeQueriesData,
                pageIndex = currentTimeQueriesData.PageIndex,
                totalPages = currentTimeQueriesData.TotalPages,
                hasNext = currentTimeQueriesData.HasNextPage,
                hasPrevious = currentTimeQueriesData.HasPreviousPage
            });
        }
    }
}
