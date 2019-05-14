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

        public Idea Get(string id)
        {
            var query = _db.Ideas.AsQueryable();
            var result = query.FirstOrDefault(i => i.Id == id);
            return result;
        }

        public IEnumerable<Idea> GetAll()
        {
            var query = _db.Ideas.AsQueryable();
            var result = query.ToList();
            return result;
        }

        public void Update(Idea newIdea)
        {
            _db.Ideas.ReplaceOne(
                x => x.Id == newIdea.Id,
                newIdea,
                new UpdateOptions
                {
                    IsUpsert = true
                });
        }

        public void Create(Idea idea)
        {
            idea.Created = DateTime.Now;
            _db.Ideas.InsertOne(idea);
        }
    }
}