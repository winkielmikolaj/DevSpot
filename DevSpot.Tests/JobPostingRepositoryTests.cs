using DevSpot.Data;
using DevSpot.Models;
using DevSpot.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSpot.Tests
{
    public class JobPostingRepositoryTests
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

        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {
            //db context
            var db = CreateDbContext();

            //job posting repository
            var repository = new JobPostingRepository(db);

            //job posting
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Location",
                UserId = "TestUserId"
            };

            //execute
            await repository.AddAsync(jobPosting);

            //check if we have result
            var result = db.JobPostings.SingleOrDefault(x => x.Title == "Test Title");

            //assert - output if our unit test is passing or not
            Assert.NotNull(result);
            Assert.Equal("Test Title", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPosting()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Location",
                UserId = "TestUserId"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            var result = await repository.GetByIdAsync(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal("Test Title", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => repository.GetByIdAsync(999)
                );
        }
    }
}
