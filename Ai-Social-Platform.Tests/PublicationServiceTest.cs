using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using AI_Social_Platform.Data;
using AI_Social_Platform.Services.Data;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;

using static AI_Social_Platform.Common.ExceptionMessages.PublicationExceptionMessages;

namespace Ai_Social_Platform.Tests
{
    [TestFixture]
    internal class PublicationServiceTest
    {
        private DbContextOptions<ASPDbContext> options;
        private ASPDbContext dbContext;
        private PublicationService publicationService;
        private HttpContextAccessor httpContextAccessor;

        [SetUp]
        public void Setup()
        {
            this.options = new DbContextOptionsBuilder<ASPDbContext>()
                .UseInMemoryDatabase("ASPInMemory" + Guid.NewGuid())
                .Options;

            dbContext = new ASPDbContext(options);

            this.dbContext.Database.EnsureCreated();

            DatabaseSeeder.SeedDatabase(dbContext);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "user@user.com")
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = httpContext
            };

            publicationService = new PublicationService(dbContext, httpContextAccessor);
        }

        [TearDown]
        public void OneTimeTearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task GetPublicationsAsync_ReturnsPublications()
        {
            // Act
            var result = await publicationService.GetPublicationsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<PublicationDto>>(result);

            // Assuming some publications were seeded during setup
            Assert.IsTrue(result.Any());
        }

        [Test]
        public async Task GetPublicationAsync_ValidId_ReturnsPublicationDto()
        {
            // Arrange - Assuming there's a publication with a known ID in the seeded data
            var knownPublicationId = dbContext.Publications.First().Id;

            // Act
            var result = await publicationService.GetPublicationAsync(knownPublicationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<PublicationDto>(result);
            Assert.That(result.Id, Is.EqualTo(knownPublicationId));
        }

        [Test]
        public async Task GetPublicationAsync_InvalidId_ThrowsNullReferenceException()
        {
            // Arrange - Assuming there's an invalid publication ID
            var invalidPublicationId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.GetPublicationAsync(invalidPublicationId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(PublicationNotFound));
        }

        [Test]
        public async Task GetPublicationAsync_ValidId_ReturnsPublicationDtoWithSameIdAndContent()
        {
            //Arrange - Assuming there's a publication with a known ID in the seeded data
            var knownPublication = dbContext.Publications.First();

            // Act
            var result = await publicationService.GetPublicationAsync(knownPublication.Id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<PublicationDto>(result);
            Assert.That(result.Id, Is.EqualTo(knownPublication.Id));
            Assert.That(result.Content, Is.EqualTo(knownPublication.Content));
            Assert.That(result.DateCreated, Is.EqualTo(knownPublication.DateCreated));
            Assert.That(result.AuthorId, Is.EqualTo(knownPublication.AuthorId));
        }

        [Test]
        public async Task CreatePublicationAsync_ValidDto_CreatesPublication()
        {
            // Arrange
            var dto = new PublicationFormDto()
            {
                Content = "This is a test publication"
            };
            var countBefore = dbContext.Publications.Count();

            // Act
            await publicationService.CreatePublicationAsync(dto);

            // Assert
            Assert.That(dbContext.Publications.Count(), Is.EqualTo(countBefore + 1));
        }

        [Test]
        public async Task CreatePublicationAsync_ValidDto_CreatesPublicationWithCorrectContent()
        {
            // Arrange
            var dto = new PublicationFormDto()
            {
                Content = "This is a test publication"
            };

            // Act
            await publicationService.CreatePublicationAsync(dto);

            // Assert
            Assert.That(dbContext.Publications.Last().Content, Is.EqualTo(dto.Content));
        }

        [Test]
        public async Task CreatePublicationAsync_ValidDto_CreatesPublicationWithCorrectAuthorId()
        {
            // Arrange
            var dto = new PublicationFormDto()
            {
                Content = "This is a test publication"
            };
            var userEmail = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userId = dbContext.Users.First(u => u.Email == userEmail).Id;

            // Act
            await publicationService.CreatePublicationAsync(dto);

            // Assert
            Assert.That(dbContext.Publications.Last().AuthorId, Is.EqualTo(userId));
        }

        [Test]
        public async Task UpdatePublicationAsync_ValidDtoAndId_UpdatesPublication()
        {
            // Arrange
            var dto = new PublicationFormDto()
            {
                Content = "This is a test publication"
            };
            var publication = dbContext.Publications.First(p => p.AuthorId.ToString() == "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            // Act
            await publicationService.UpdatePublicationAsync(dto, publication.Id);

            // Assert
            Assert.That(publication.Content, Is.EqualTo(dto.Content));
        }
    }
}
