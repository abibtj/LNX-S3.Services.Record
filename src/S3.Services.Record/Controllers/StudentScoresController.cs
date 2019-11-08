using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using S3.Common.Authentication;
using S3.Common.Dispatchers;
using S3.Common.Mvc;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;
using S3.Services.Record.StudentScores.Commands;
using S3.Services.Record.StudentScores.Queries;

namespace S3.Services.Record.Controllers
{
    //[JwtAuth(Roles = "superadmin, admin, teacher")]
    public class StudentScoresController : BaseController
    {
        public StudentScoresController(IBusPublisher busPublisher, IDispatcher dispatcher, ITracer tracer)
            : base(busPublisher, dispatcher, tracer) { }

        [HttpGet("browse")]
        public async Task<IActionResult> GetAllAsync(string[]? include, Guid? schoolId, Guid? studentId, Guid? classId, string? subject, string? examType,
            int? term, int? session, int page, int results, string orderBy, string sortOrder)

             => Ok(await QueryAsync(new BrowseStudentScoresQuery(include, schoolId, studentId, classId, subject, examType, term, session, page,
                 results, orderBy, sortOrder)));

        [HttpGet("get/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, [FromQuery]string[]? include)
            => Single(await QueryAsync(new GetStudentScoreQuery(id, include)));

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateStudentScoreCommand command)
            => await SendAsync(command, resource: "studentScores");

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateStudentScoreCommand command)
        => await SendAsync(command, resourceId: null, resource: "studentScores");
        //=> await SendAsync(command, resourceId: command.Id, resource: "studentScores");

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await SendAsync(new DeleteStudentScoreCommand(id), resourceId: id, resource: "studentScores");
    }
}
