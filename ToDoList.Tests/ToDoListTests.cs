using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.CodeCoverage;
using Moq;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Tests
{
    public class ToDoListTests
    {
        [Fact]
        public void GetTask_Success()
        {
            //Arrange
            LifeSphere lifeSphere = new LifeSphere
            {
                Title = "Test",
                Description = "Test",
                Tasks = new List<TaskItem>
                {
                    new TaskItem
                    {
                        Title = "Test",
                        DeadLine = DateTime.Now,
                        Description = "Test"
                    }
                }
            };

            var lifeSphereRepository = new Mock<ILifeSphereRepository>();
            lifeSphereRepository
                .Setup(x => x.GetLifeSpheres())
                .Returns(new List<LifeSphere> { lifeSphere });

            var service = new LifeSphereService(lifeSphereRepository.Object);

            //Act
            var lifeSpheres = service.GetLifeSpheres();

            //Assert
            lifeSphereRepository.Verify(
                lifeSphereRepository => lifeSphereRepository.GetLifeSpheres(),
                Times.Once());
            Assert.Single(lifeSpheres);
            Assert.Same(lifeSpheres.Single(), lifeSphere);
        }
    }
}