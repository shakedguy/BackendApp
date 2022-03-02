using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using BackendApp.Server.Controllers;
using BackendApp.Shared;
using BackendApp.Shared.Models;
using DataLibrary;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Xunit;
using OkResult = System.Web.Http.Results.OkResult;

namespace TestProject.Server
{
    public class ControllerTestings
    {
        private const int c_NumOfDummy = 5;
        private readonly string[] c_routes = new string[] { "messages", "divisions", "agents", "dispatches" };

        private readonly IEnumerable<MessageModel> m_FakeMessages =
            A.CollectionOfDummy<MessageModel>(c_NumOfDummy).AsEnumerable();

        private readonly IEnumerable<DivisionModel> m_FakeDivisionss =
            A.CollectionOfDummy<DivisionModel>(c_NumOfDummy).AsEnumerable();

        private readonly IEnumerable<AgentModel> m_FakeAgents =
            A.CollectionOfDummy<AgentModel>(c_NumOfDummy).AsEnumerable();

        private readonly IEnumerable<DispatchModel> m_FakeDispatches =
            A.CollectionOfDummy<DispatchModel>(c_NumOfDummy).AsEnumerable();


        [Fact]
        public async Task GetAllTest()
        {
            var dataStore = A.Fake<IDataAccess>();
            var controller = new DataTablesController(dataStore);

            var result = await controller.Get("messages");
            Assert.Equal((result.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Get("divisions");
            Assert.Equal((result.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Get("agents");
            Assert.Equal((result.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Get("dispatches");
            Assert.Equal((result.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Get("BadRequest");
            Assert.Equal((result.Result as NotFoundObjectResult).StatusCode, StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetTest()
        {
            var dataStore = A.Fake<IDataAccess>();
            var controller = new DataTablesController(dataStore);

            var result = await controller.Get("messages", 1);
            Assert.Equal((result.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Get("divisions", 1);
            Assert.Equal((result.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Get("agents", 1);
            Assert.Equal((result.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Get("dispatches", 1);
            Assert.Equal((result.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Get("messages", 0);
            Assert.Equal((result.Result as BadRequestObjectResult).StatusCode, StatusCodes.Status400BadRequest);

            result = await controller.Get("BadRequest", 1);
            Assert.Equal((result.Result as NotFoundObjectResult).StatusCode, StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task PostTest()
        {
            var dataStore = A.Fake<IDataAccess>();
            var controller = new DataTablesController(dataStore);

            var message = A.Fake<MessageModel>();
            var division = A.Fake<DivisionModel>();
            var agent = A.Fake<AgentModel>();
            var dispatch = A.Fake<DispatchModel>();

            var result = await controller.PostMessage(message);
            Assert.Equal((result as UnauthorizedObjectResult)!.StatusCode, StatusCodes.Status401Unauthorized);

            result = await controller.PostDivision(division);
            Assert.Equal((result as UnauthorizedObjectResult)!.StatusCode, StatusCodes.Status401Unauthorized);

            result = await controller.PostAgent(agent);
            Assert.Equal((result as UnauthorizedObjectResult)!.StatusCode, StatusCodes.Status401Unauthorized);

            result = await controller.PostDispatcher(dispatch);
            Assert.Equal((result as UnauthorizedObjectResult)!.StatusCode, StatusCodes.Status401Unauthorized);

            message.Id = division.Id = agent.Id = dispatch.Id = 1;

            result = await controller.PostMessage(message);
            Assert.Equal((result as CreatedResult)!.StatusCode, StatusCodes.Status201Created);

            result = await controller.PostDivision(division);
            Assert.Equal((result as CreatedResult)!.StatusCode, StatusCodes.Status201Created);

            result = await controller.PostAgent(agent);
            Assert.Equal((result as CreatedResult)!.StatusCode, StatusCodes.Status201Created);

            result = await controller.PostDispatcher(dispatch);
            Assert.Equal((result as CreatedResult)!.StatusCode, StatusCodes.Status201Created);

        }

        [Fact]
        public async Task DeleteAllTest()
        {
            var dataStore = A.Fake<IDataAccess>();
            var controller = new DataTablesController(dataStore);

            var result = await controller.Delete("message");
            Assert.Equal((result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Delete("divisions");
            Assert.Equal((result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Delete("agents");
            Assert.Equal((result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Delete("dispatches");
            Assert.Equal((result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

        }

        [Fact]
        public async Task DeleteTest()
        {
            var dataStore = A.Fake<IDataAccess>();
            var controller = new DataTablesController(dataStore);

            var result = await controller.Delete("message", 1);
            Assert.Equal((result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Delete("division", 1);
            Assert.Equal((result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Delete("agent", 1);
            Assert.Equal((result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Delete("dispatche", 1);
            Assert.Equal((result as OkObjectResult).StatusCode, StatusCodes.Status200OK);

            result = await controller.Delete("message", 0);
            Assert.Equal((result as UnauthorizedObjectResult).StatusCode, StatusCodes.Status401Unauthorized);

            result = await controller.Delete("divisions", 0);
            Assert.Equal((result as UnauthorizedObjectResult).StatusCode, StatusCodes.Status401Unauthorized);

            result = await controller.Delete("agents", 0);
            Assert.Equal((result as UnauthorizedObjectResult).StatusCode, StatusCodes.Status401Unauthorized);

            result = await controller.Delete("dispatches", 0);
            Assert.Equal((result as UnauthorizedObjectResult).StatusCode, StatusCodes.Status401Unauthorized);

        }
    }
}
