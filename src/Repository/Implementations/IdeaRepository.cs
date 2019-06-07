using System.Linq;
using System.Collections.Generic;
using Domain;
using Repository.Contracts;
using MongoDB.Driver;
using System;

namespace Repository.Implementations
{
    public class IdeaRepository : IIdeaRepository
    {
        private IdeasContext _db;

        public IdeaRepository(IdeasContext db)
        {
            _db = db;
        }

        public Idea GetIdea(string id)
        {
            var query = _db.Ideas.AsQueryable();
            var result = query.FirstOrDefault(i => i.Id == id);
            return result;
        }

        public IEnumerable<Idea> GetAllIdeas()
        {
            var query = _db.Ideas.AsQueryable();
            var result = query.ToList();
            return result;
        }

        public IEnumerable<Idea> GetShortlistedIdeas()
        {
            var query = _db.Ideas.AsQueryable();

            var result = query
                .Where(i => i.Shortlisted)
                .ToList();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public IEnumerable<Idea> GetIdeas(int pageNumber = 1, int pageCount = 5)
        {
            var query = _db.Ideas.AsQueryable();
            var result = query
                .Skip(pageCount * (pageNumber - 1))
                .Take(pageCount)
                .ToList();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idea"></param>
        public void UpdateIdea(Idea idea)
        {
             idea = GetIdea(idea.Id);
            idea.ModifiedOn = DateTime.UtcNow; 
            _db.Ideas.ReplaceOne(
                x => x.Id == idea.Id,
                idea,
                new UpdateOptions
                {
                    IsUpsert = true
                });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idea"></param>
        public void CreateIdea(Idea idea)
        {
            idea.CreatedOn = idea.ModifiedOn = DateTime.UtcNow;
            idea.Shortlisted = true;
            _db.Ideas.InsertOne(idea);
        }
    }
}