using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;

namespace Api.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeasController : ControllerBase
    {
        private IIdeaRepository _repository;

        public IdeasController(IIdeaRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all the ideas
        /// </summary>
        /// <returns>All the ideas</returns>
        [HttpGet("all")]
        public ActionResult<IEnumerable<Idea>> GetAllIdeas()
        {
            var result = _repository.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Get idea by ID
        /// </summary>
        /// <param name="id">Id of the idea</param>
        /// <returns>The idea</returns>
        [HttpGet("idea")]
        public ActionResult<Idea> GetIdea(string id)
        {
            var idea = _repository.Get(id);
            return Ok(idea);
        }

        /// <summary>
        /// Create new idea
        /// </summary>
        /// <param name="idea">The idea</param>
        /// <returns>Status code</returns>
        [HttpPost("new")]
        public ActionResult CreateIdea(Idea idea)
        {
            _repository.Create(idea);
            return Ok();
        }

        /// <summary>
        /// Update idea
        /// </summary>
        /// <param name="idea">The idea</param>
        [HttpPut("update-idea")]
        public ActionResult UpdateIdea(Idea idea)
        {
            _repository.Update(idea);
            return Ok();
        }

        /// <summary>
        /// Chart data
        /// </summary>
        /// <returns>Chart data</returns>
        [HttpGet("chart")]
        public ActionResult<ChartViewModel> GetChartData()
        {
            var result = _repository.GetChartData();
            return Ok(result);
        }
    }
}