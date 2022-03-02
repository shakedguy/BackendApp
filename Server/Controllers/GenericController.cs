using System.Collections.ObjectModel;
using System.Net.Mime;
using BackendApp.Client;
using BackendApp.Shared;
using BackendApp.Shared.Models;
using DataLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class GenericController : ControllerBase
    {
        private readonly IDataAccess r_dataAccess;
        //private readonly IWebHostEnvironment r_environment;
        private readonly ILogger<GenericController> r_logger;
        private readonly bool r_IsDevelopment;
        private const string query = "SELECT * FROM ";
        private const string extention = " WHERE id=@Id";
        private const string post = "INSERT INTO ";
        private const string delete = "DELETE FROM";


        public GenericController(IDataAccess i_DataAccess,
            ILogger<GenericController> i_Logger)
        {
            r_dataAccess = i_DataAccess;
            r_logger = i_Logger;
            //r_environment = i_Environment;
            //r_IsDevelopment = r_environment.IsDevelopment();
        }

        ///////////// Get ////////////

        [HttpGet("{i_Table}")]
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
                    case "division":
                        response = await r_dataAccess.LoadData<DivisionModel, dynamic>(
                            query + "division",
                            new { }, DbContext.ConnectionString);
                        break;
                    case "message":
                        response = await r_dataAccess.LoadData<MessageModel, dynamic>(MessageModel.GetAllQuery,
                            new { }, DbContext.ConnectionString);
                        break;
                    case "agent":
                        response = await r_dataAccess.LoadData<AgentModel, dynamic>(query + "agent",
                            new { }, DbContext.ConnectionString);
                        break;
                    case "dispatch":
                        response = await r_dataAccess.LoadData<DispatchModel, dynamic>(query + "dispatch",
                            new { }, DbContext.ConnectionString);
                        break;
                    default:
                        return BadRequest(null);
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                //if (r_IsDevelopment)
                //    r_logger.LogError(e.Message, e.Source, e.HelpLink);
                return NotFound(null);
            }

        }

        [HttpGet("{i_Table}/{i_Id}")]
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
                            response = await r_dataAccess.LoadItem<DivisionModel, dynamic>(
                                query + "division" + extention,
                                new { Id = i_Id }, DbContext.ConnectionString);
                            break;
                        case "messages":
                            response = await r_dataAccess.LoadItem<MessageModel, dynamic>(query + "message" + extention,
                                new { Id = i_Id }, DbContext.ConnectionString);
                            break;
                        case "agents":
                            response = await r_dataAccess.LoadItem<AgentModel, dynamic>(query + "agent" + extention,
                                new { Id = i_Id }, DbContext.ConnectionString);
                            break;
                        case "dispatches":
                            response = await r_dataAccess.LoadItem<DispatchModel, dynamic>(
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


        ///////////// Put ////////////

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(MessageModel i_Msg)
        {

            if (i_Msg.Id <= 0)
                return Unauthorized(null);
            try
            {
                await r_dataAccess.SaveData<dynamic>(MessageModel.UpdateQuery,
                    new
                    {
                        Id = i_Msg.Id,
                        Content = i_Msg.Content,
                        DivisionId = i_Msg.DivisionId,
                        Done = i_Msg.Done
                    },
                    DbContext.ConnectionString);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(null);
            }

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(DivisionModel i_Dvn)
        {

            if (i_Dvn.Id <= 0)
                return Unauthorized(null);
            try
            {

                await r_dataAccess.SaveData<dynamic>(DivisionModel.UpdateQuery,
                    new
                    {
                        Id = i_Dvn.Id,
                    },
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
