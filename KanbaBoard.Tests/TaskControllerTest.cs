using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanban.Web;
using Kanban.Web.Controllers;
using Moq;
using Kanban.Dal;
using Xunit;
using Kanban.Web.Models;
using System.Net;
using Kanban.Dal.Exceptions;

namespace KanbaBoard.Tests
{
    public class TaskControllerTest
    {
        [Fact]
        public void GetAllShouldReturnData()
        {
            // Act
           var mock
                = new Mock<ITaskRepository>();
            
            var data = new List<Task>();
            mock
                .Setup(s => s.GetAll())
                .Returns(data);
            // Arrange
            var controller 
                = new TaskController(mock.Object);

            var result = controller.GetAll();

            // Assert
            Assert.Equal(result,data);
        }

        [Fact]
        public void AddShouldAddDataInRepository()
        {
            // Act
            var mock
                 = new Mock<ITaskRepository>();

            // Arrange
            var controller
                = new TaskController(mock.Object);

            var result = controller.PostTask(
                new AddTask() { }
                );

            // Assert
            mock
                .Verify(m => m.Add(It.IsAny<Task>())
                , Times.Once);

            // Times.once -> verifier que method Add est appelée qu'une fois

            Assert.Equal(HttpStatusCode.Created,result.StatusCode);
        }

        [Fact]
        public void UpdateShouldUpdateDataInRepository()
        {
            // Act
            ulong id = 1;
            UpdateTask param = new UpdateTask()
            {
                description = "desc",
                name = "test",
                status = 1
            };

            var mock = new Mock<ITaskRepository>();

            var controller = new TaskController(mock.Object);

            var result = controller.PutTask(1, param);

            // Arrange
            
            // Assert
            mock.Verify(m => m.Update(It.Is<Task>(t => t.id == id)), Times.Once);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void UpdateTaskNotFound()
        {
            // Act
            ulong id = 1;
            UpdateTask param = new UpdateTask()
            {
                description = "desc",
                name = "test",
                status = 1
            };

            var mock = new Mock<ITaskRepository>();

            // Arrange
            mock.Setup(m => m.Update(It.IsAny<Task>()))
                .Throws(new TaskNotFoundException(id));

            var controller = new TaskController(mock.Object);

            var result = controller.PutTask(1, param);

            // Assert
            mock.Verify(m => m.Update(It.Is<Task>(t => t.id == id)), Times.Once);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public void DeleteShouldDeleteDataInRepository()
        {
            // Act
            ulong id = 1;

            var mock = new Mock<ITaskRepository>();

            var controller = new TaskController(mock.Object);

            var result = controller.DeleteTask(id);

            // Arrange

            // Assert
            mock.Verify(m => m.Delete(id) , Times.Once);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void DeleteTaskNotFound()
        {
            // Act
            ulong id = 1;

            var mock = new Mock<ITaskRepository>();

            var controller = new TaskController(mock.Object);

            // Arrange
            mock
                .Setup(m => m.Delete(It.IsAny<ulong>()))
                .Throws(new TaskNotFoundException(id));

            var result = controller.DeleteTask(id);
            // Assert
            mock.Verify(m => m.Delete(id), Times.Once);
                
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
