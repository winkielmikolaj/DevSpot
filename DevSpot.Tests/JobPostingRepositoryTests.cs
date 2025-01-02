using DevSpot.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSpot.Tests
{
    internal class JobPostingRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;


        public JobPostingRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("JobPostingDb")
                .Options;
        }

        private ApplicationDbContext CreateDbContext()
        {
            return new ApplicationDbContext(_options);
        }
    }
}
