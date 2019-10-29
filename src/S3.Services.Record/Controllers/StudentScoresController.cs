//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using OpenTracing;
//using S3.Common.Authentication;
//using S3.Common.Dispatchers;
//using S3.Common.Mvc;
//using S3.Common.RabbitMq;
//using S3.Common.Types;
//using S3.Services.Record.Domain;
//using S3.Services.Record.Dto;
//using S3.Services.Record.StudentScores.Commands;
//using S3.Services.Record.StudentScores.Queries;

//namespace S3.Services.Record.Controllers
//{
//    //[JwtAuth(Roles = "superadmin, admin, teacher")]
//    public class StudentScoresController : BaseController
//    {
//        public StudentScoresController(IBusPublisher busPublisher, IDispatcher dispatcher, ITracer tracer) 
//            : base(busPublisher, dispatcher, tracer) { }

//        [HttpGet("browse")]
//        public async Task<IActionResult> GetAllAsync([FromQuery] BrowseStudentScoresQuery query)
//            => Ok( await QueryAsync(query));

//        [HttpGet("get/{id:guid}")]
//        public async Task<IActionResult> GetByIdAsync(Guid id)
//            => Single(await QueryAsync(new GetStudentScoreQuery(id)));

//        [HttpPost("create")]
//        public async Task<IActionResult> Create(CreateStudentScoreCommand command)
//            => await SendAsync(command, resource: "studentScore");

//        [HttpPut("update")]
//        public async Task<IActionResult> Update(UpdateStudentScoreCommand command)
//            => await SendAsync(command, resourceId: command.Id, resource: "studentScore");

//        [HttpDelete("delete/{id:guid}")]
//        public async Task<IActionResult> Delete(Guid id) 
//            => await SendAsync(new DeleteStudentScoreCommand(id), resourceId: id, resource: "studentScore");
//    }
//}
