using BackendApp.Shared;
using BackendApp.Shared.Models;
using DataLibrary;
using Microsoft.AspNetCore.Mvc;


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
        private const string delete = "DELETE FROM";


        public DataTablesController(IDataAccess dataAccess) => m_dataAccess = dataAccess;


        ///////////// Get ////////////

        [HttpGet("genericApi/{i_Table}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<IModel>>> Get(string i_Table)
        {
            try
            {
                IEnumerable<IModel>? response;
                switch (i_Table)
                {
                    case "divisions":
                        response = await m_dataAccess.LoadData<DivisionModel, dynamic>(
                            query + "division",
                            new { }, DbContext.ConnectionString);
                        break;
                    case "messages":
                        response = await m_dataAccess.LoadData<MessageModel, dynamic>(query + "message",
                            new { }, DbContext.ConnectionString);
                        break;
                    case "agents":
                        response = await m_dataAccess.LoadData<AgentModel, dynamic>(query + "agent",
                            new { }, DbContext.ConnectionString);
                        break;
                    case "dispatches":
                        response = await m_dataAccess.LoadData<DispatchModel, dynamic>(query + "dispatch",
                            new { }, DbContext.ConnectionString);
                        break;
                    default:
                        return BadRequest(null);
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound(null);
            }

        }

        [HttpGet("genericApi/{i_Table}/{i_Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IModel>> Get(string i_Table, int i_Id)
        {
            if (i_Id <= 0)
                return BadRequest(new MessageModel());
            else
            {
                try
                {
                    IModel response;
                    switch (i_Table)
                    {
                        case "divisions":
                            response = await m_dataAccess.LoadItem<DivisionModel, dynamic>(
                                query + "division" + extention,
                                new { Id = i_Id }, DbContext.ConnectionString);
                            break;
                        case "messages":
                            response = await m_dataAccess.LoadItem<MessageModel, dynamic>(query + "message" + extention,
                                new { Id = i_Id }, DbContext.ConnectionString);
                            break;
                        case "agents":
                            response = await m_dataAccess.LoadItem<AgentModel, dynamic>(query + "agent" + extention,
                                new { Id = i_Id }, DbContext.ConnectionString);
                            break;
                        case "dispatches":
                            response = await m_dataAccess.LoadItem<DispatchModel, dynamic>(
                                query + "dispatch" + extention,
                                new { Id = i_Id }, DbContext.ConnectionString);
                            break;
                        default:
                            return BadRequest(null);
                    }

                    return Ok(response);
                }
                catch (Exception e)
                {
                    return NotFound(null);
                }
            }
        }


        ///////////// Post ////////////

        [HttpPost("genericApi/messages")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostMessage(MessageModel i_Msg)
        {
            if (i_Msg.Id <= 0)
                return Unauthorized(null);
            try
            {
                await m_dataAccess.SaveData<dynamic>(
                    post + "message (id, content, divisionId, done) VALUES (@Id, @Content, @DivisionId, @Done)",
                    new { Id = i_Msg.Id, Content = i_Msg.Content, DivisionId = i_Msg.DivisionId, Done = i_Msg.Done },
                    DbContext.ConnectionString);
                return Created("", i_Msg);
            }
            catch (Exception)
            {
                return BadRequest(null);
            }

        }

        [HttpPost("genericApi/divisions")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostDivision(DivisionModel i_Dvn)
        {
            if (i_Dvn.Id <= 0)
                return Unauthorized(null);
            try
            {
                await m_dataAccess.SaveData<dynamic>(post + "division (id) VALUES (@Id)",
                    new { Id = i_Dvn.Id }, DbContext.ConnectionString);
                return Created("", i_Dvn);
            }
            catch (Exception e)
            {
                return BadRequest(null);
            }

        }

        [HttpPost("genericApi/agents")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAgent(AgentModel i_Agent)
        {
            if (i_Agent.Id <= 0)
                return Unauthorized(null);
            try
            {
                await m_dataAccess.SaveData<dynamic>(post + "agent (id) VALUES (@Id)",
                    new { Id = i_Agent.Id }, DbContext.ConnectionString);
                return Created("", i_Agent);
            }
            catch (Exception)
            {
                return BadRequest(null);
            }
        }


        [HttpPost("genericApi/dispatches")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostDispatcher(DispatchModel i_Dispatch)
        {
            if (i_Dispatch.Id <= 0)
                return Unauthorized(null);
            try
            {
                await m_dataAccess.SaveData<dynamic>(
                    post + "dispatch (id, messageId, agentId) VALUES (@Id, @MessageId, @AgentId)",
                    new { Id = i_Dispatch.Id, MessageId = i_Dispatch.MessageId, AgentId = i_Dispatch.AgentId },
                    DbContext.ConnectionString);
                return Created("", i_Dispatch);
            }
            catch (Exception)
            {
                return BadRequest(null);
            }
        }


        ///////////// Delete ////////////

        [HttpDelete("genericApi/{i_Table}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string i_Table)
        {
            try
            {
                await m_dataAccess.SaveData<dynamic>($"{delete} {i_Table}",
                    new { }, DbContext.ConnectionString);
                return Ok(null);
            }
            catch (Exception e)
            {
                return BadRequest(null);
            }
        }

        [HttpDelete("genericApi/{i_Table}/{i_Id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string i_Table, int i_Id)
        {
            if (i_Id <= 0)
                return Unauthorized(null);
            try
            {
                string query = $"{delete} {i_Table} WHERE id = @Id";
                await m_dataAccess.SaveData<dynamic>(query,
                    new { Id = i_Id }, DbContext.ConnectionString);
                return Ok(null);
            }
            catch (Exception e)
            {
                return BadRequest(null);
            }
        }


        ///////////// Put ////////////

        [HttpPut("genericApi/messages")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMessage(MessageModel i_Msg)
        {
            if (i_Msg.Id <= 0)
                return Unauthorized(null);
            try
            {
                await m_dataAccess.SaveData<dynamic>(MessageModel.UpdateQuery,
                    new { Id = i_Msg.Id, Content = i_Msg.Content, DivisionId = i_Msg.DivisionId, Done = i_Msg.Done }, DbContext.ConnectionString);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(null);
            }

        }

        [HttpPut("genericApi/divisions")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateDivision(DivisionModel i_Div)
        {
            if (i_Div.Id <= 0)
                return Unauthorized(null);
            try
            {
                await m_dataAccess.SaveData<dynamic>(DivisionModel.UpdateQuery,
                    new { Id = i_Div.Id },
                    DbContext.ConnectionString);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(null);
            }

        }

        [HttpPut("genericApi/agents")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAgent(AgentModel i_Agn)
        {
            if (i_Agn.Id <= 0)
                return Unauthorized(null);
            try
            {
                await m_dataAccess.SaveData<dynamic>(AgentModel.UpdateQuery,
                    new { Id = i_Agn.Id },
                    DbContext.ConnectionString);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(null);
            }

        }

        [HttpPut("genericApi/dispatches")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateDispatche(DispatchModel i_Dis)
        {
            if (i_Dis.Id <= 0)
                return Unauthorized(null);
            try
            {
                await m_dataAccess.SaveData<dynamic>(DispatchModel.UpdateQuery,
                    new { Id = i_Dis.Id, MessageId = i_Dis.MessageId, AgentId = i_Dis.AgentId },
                    DbContext.ConnectionString);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(null);
            }

        }

    }
}
