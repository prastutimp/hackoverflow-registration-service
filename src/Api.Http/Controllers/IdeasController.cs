using System;
using System.Collections.Generic;
using System.Net;
using Domain;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Repository.Contracts;

namespace Api.Http.Controllers
{
    [Route("idea")]
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
            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Prastutimp09@gmail.com"));
            //message.To.Add(new MailboxAddress("Prastutimp09@gmail.com"));
            //message.Subject = "New Requirement";
            //message.Body = new TextPart("Plain")
            //{
            //    Text = "this is the requirement",
            //};

            //using (var client = new SmtpClient())
            //{
            //    client.Connect("smtp-mail.outlook.com",587,false);
            //    client.AuthenticationMechanisms.Remove("XOAUTH2");
            //   // var credentials = new NetworkCredential() { UserName="Prastutigowda@gmail.com", Password="Prastuti11"};
            //    client.Authenticate("Prastutispare@outlook.com", "Prastuti@123");
            //    client.Send(message);
            //}
            
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
            SendEmail(idea);
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
            SendEmail(idea, true);
            return Ok();
        }

        [HttpDelete("delete-idea/{id}")]
        public ActionResult DeleteIdea(string id)
        {
            _repository.DeleteIdea(id);
            return Ok("Deleted");
        }

        private void SendEmail(Idea idea, bool isUpdate=false)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Prastuti Prasanna", "prastutimp09@gmail.com"));
            message.To.Add(new MailboxAddress("Prastuti MP", "prastutimp@eurofins.com"));
            message.Subject = isUpdate?"Your client has updated the requirement":"You have a new requirement from a client";

            var builder = new BodyBuilder();

            string body = $@"<div>
            Hi Prastuti, <br/> Here is the Requirement details.
            <br/>
            ID: {idea.Id}    <br/>
            Name: {idea.Name}<br/>
            Description: {idea.Description} <br/>
            Benefits: {idea.Benefits} <br/>
            User: Demouser <br/>    
            Instance Name: FR-FOOD
            </div>";
            builder.HtmlBody = body;
            message.Body = builder.ToMessageBody();

            try
            {
                var client = new SmtpClient();

                client.Connect("smtp.gmail.com", 587, false); //smtp.gmail.com
                client.Authenticate("prastutimp09@gmail.com", "Prastuti@123");
                client.Send(message);
                client.Disconnect(true);

                Console.WriteLine("Send Mail Success.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Send Mail Failed : " + e.Message);
            }

        }
    }
}