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
            var result = _repository.GetAllIdeas();
            return Ok(result);
        }

        /// <summary>
        /// Get paged ideas
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageCount">Page count</param>
        /// <returns>Paged ideas</returns>
        [HttpGet("ideas")]
        public ActionResult<IEnumerable<Idea>> GetIdeas(int pageNumber = 1, int pageCount = 5)
        {
            var result = _repository.GetIdeas(pageNumber, pageCount);
            return Ok(result);
        }

        /// <summary>
        /// Get shortlisted ideas
        /// </summary>
        /// <returns>Shortlisted ideas</returns>
        [HttpGet("shortlisted-ideas")]
        public ActionResult<IEnumerable<Idea>> GetShortlistedIdeas()
        {
            var result = _repository.GetShortlistedIdeas();
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
            var idea = _repository.GetIdea(id);
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
            _repository.CreateIdea(idea);
            return Ok();
        }

        /// <summary>
        /// Update idea
        /// </summary>
        /// <param name="idea">The idea</param>
        [HttpPut("update-idea")]
        public ActionResult UpdateIdea(Idea idea)
        {
            _repository.UpdateIdea(idea);
            return Ok();
        }

    }
}