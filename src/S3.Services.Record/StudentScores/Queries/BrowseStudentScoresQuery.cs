using S3.Common.Types;
using S3.Services.Record.Dto;
using System.Collections.Generic;

namespace S3.Services.Record.StudentScores.Queries
{
    public class BrowseStudentScoresQuery : PagedQueryBase, IQuery<IEnumerable<StudentScoreDto>>
    {
    }
}