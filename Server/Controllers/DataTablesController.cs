using BackendApp.Shared;
using BackendApp.Shared.Models;
using DataLibrary;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendApp.Server.Controllers
{
    [Route("")]
    [ApiController]
    public class DataTablesController : ControllerBase
    {
        private IDataAccess m_dataAccess;
        private const string query = "SELECT * FROM ";
        private const string extention = " WHERE id=@Id";
        private const string post = "INSERT INTO ";


        public DataTablesController(IDataAccess dataAccess)
        {

            m_dataAccess = dataAccess;
        }
        // GET: api/<DataTablesController>

        [HttpGet("messages")]
        public async Task<IEnumerable<MessageModel>> GetMessages() =>
            await m_dataAccess.LoadData<MessageModel, dynamic>(query + "message",
                new { }, DbContext.ConnectionString);

        [HttpGet("messages/{id}")]
        public async Task<MessageModel> GetMessage(string i_Id) =>
            await m_dataAccess.LoadItem<MessageModel, dynamic>(query + "message" + extention,
                     new { Id = int.Parse(i_Id) }, DbContext.ConnectionString);

        [HttpPost("messages")]
        public async Task PostMessages(MessageModel i_Msg) =>
            await m_dataAccess.SaveData<dynamic>(
                post + "message (id, content, divisionId, done) VALUES (@Id, @Content, @DivisionId, @Done)",
                new { Id = i_Msg.Id, Content = i_Msg.Content, DivisionId = i_Msg.DivisionId, Done = i_Msg.Done },
                DbContext.ConnectionString);

        [HttpGet("divisions")]
        public async Task<IEnumerable<DivisionModel>> GetDivisions() =>
            await m_dataAccess.LoadData<DivisionModel, dynamic>(query + "division",
                new { }, DbContext.ConnectionString);

        [HttpGet("divisions/{id}")]
        public async Task<IEnumerable<DivisionModel>> GetDivisions(int i_Id) =>
            await m_dataAccess.LoadData<DivisionModel, dynamic>(query + "division" + extention,
                new { Id = i_Id }, DbContext.ConnectionString);

        [HttpPost("divisions")]
        public async Task PostDivisions(DivisionModel i_Dvn) =>
            await m_dataAccess.SaveData<dynamic>(post + "division (id) VALUES (@Id)",
                new { Id = i_Dvn.Id }, DbContext.ConnectionString);

        [HttpGet("agents")]
        public async Task<IEnumerable<AgentModel>> GetAgents() =>
            await m_dataAccess.LoadData<AgentModel, dynamic>(query + "agent",
                new { }, DbContext.ConnectionString);

        [HttpGet("agents/{id}")]
        public async Task<IEnumerable<AgentModel>> GetAgents(int i_Id) =>
            await m_dataAccess.LoadData<AgentModel, dynamic>(query + "agent" + extention,
                new { Id = i_Id }, DbContext.ConnectionString);

        [HttpPost("agents")]
        public async Task PostAgents(AgentModel i_Agent) =>
            await m_dataAccess.SaveData<dynamic>(post + "agent (id) VALUES (@Id)",
                new { Id = i_Agent.Id }, DbContext.ConnectionString);

        [HttpGet("dispatches")]
        public async Task<IEnumerable<DispatchModel>> GetADispatches() =>
            await m_dataAccess.LoadData<DispatchModel, dynamic>(query + "dispatch",
                new { }, DbContext.ConnectionString);

        [HttpGet("dispatches/{id}")]
        public async Task<IEnumerable<DispatchModel>> GetADispatches(int i_Id) =>
            await m_dataAccess.LoadData<DispatchModel, dynamic>(query + "dispatch" + extention,
                new { Id = i_Id }, DbContext.ConnectionString);

        [HttpPost("dispatches")]
        public async Task PostDispatches(DispatchModel i_Dispatch) =>
            await m_dataAccess.SaveData<dynamic>(post + "dispatch (id, messageId, agentId) VALUES (@Id, @MessageId, @AgentId)",
                new { Id = i_Dispatch.Id, MessageId = i_Dispatch.MessageId, AgentId = i_Dispatch.AgentId }, DbContext.ConnectionString);
    }
}
