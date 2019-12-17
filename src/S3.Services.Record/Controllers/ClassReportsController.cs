using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using S3.Common.Dispatchers;
using S3.Common.RabbitMq;
using S3.Services.Record.ClassReports.Queries;

namespace S3.Services.Record.Controllers
{
    //[JwtAuth(Roles = "superadmin, admin, teacher")]
    public class ClassReportsController : BaseController
    {
        public ClassReportsController(IBusPublisher busPublisher, IDispatcher dispatcher, ITracer tracer)
            : base(busPublisher, dispatcher, tracer) { }

        [HttpGet("get")]
        public async Task<IActionResult> GetAsync(Guid? schoolId, Guid? studentId, Guid? classId, int session)
            => Single(await QueryAsync(new GetClassReportQuery(schoolId, studentId, classId, session)));
    }
}
