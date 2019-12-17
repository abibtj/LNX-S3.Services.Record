using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using S3.Services.Record;
using S3.Services.Record.Domain;
using System;
using System.Collections.Generic;

namespace S3.Services.Record.Utility
{
    public class RecordDbInitialiser : IRecordDbInitialiser
    {
        private readonly RecordDbContext _context;

        public RecordDbInitialiser(RecordDbContext context)
            => _context = context;

        public void Initialise()
        {
            _context.Database.EnsureDeleted(); //***ToDo... remove this line later (Disasterous for production!!!)
            _context.Database.Migrate();
            _context.Database.EnsureCreated();

            SeedRule();
        }

        private void SeedRule()
        {
            var rules = new List<Rule>
            {
                new Rule
                {
                    Name = "Default Rule",
                    CAPercentage = 20,
                    ClassActivitiesPercentage = 20,
                    FirstExamPercentage = 20,
                    HomeworkPercentage = 20,
                    SecondExamPercentage = 20,
                    A_DistinctionCutoff = 85,
                    B_VeryGoodCutoff = 70,
                    C_CreditCutoff = 55,
                    P_PassCutoff = 50,
                    IsDefault = true,
                    SchoolId = Guid.NewGuid()
                }
            };

            _context.Rules.AddRange(rules);
            _context.SaveChanges();
        }
    }
}