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

        public IEnumerable<Idea> GetIdeas(int pageNumber = 1, int pageCount = 5)
        {
            var query = _db.Ideas.AsQueryable();
            var result = query
                .Where(i => !i.Shortlisted)
                .Skip(pageCount * (pageNumber - 1))
                .Take(pageCount)
                .ToList();

            return result;
        }

        public void UpdateIdea(Idea newIdea)
        {
            var idea = GetIdea(newIdea.Id);
            newIdea.Created = idea.Created;
            
            _db.Ideas.ReplaceOne(
                x => x.Id == newIdea.Id,
                newIdea,
                new UpdateOptions
                {
                    IsUpsert = true
                });
        }

        public void CreateIdea(Idea idea)
        {
            idea.Created = DateTime.Now;
            idea.Shortlisted = false;
            _db.Ideas.InsertOne(idea);
        }

        public ChartViewModel GetChartData()
        {
            var query = _db.Ideas.AsQueryable();
            var ideas = query.ToList();

            // by members
            var byMembers = new Dictionary<string, List<string>>();

            foreach (var idea in ideas)
            {
                if (!byMembers.ContainsKey(idea.Member1))
                {
                    byMembers.Add(idea.Member1, new List<string>());
                }

                byMembers[idea.Member1].Add(idea.TeamName);

                if (idea.Member2 == null)
                {
                    continue;
                }

                if (!byMembers.ContainsKey(idea.Member2))
                {
                    byMembers.Add(idea.Member2, new List<string>());
                }

                byMembers[idea.Member2].Add(idea.TeamName);
            }

            // by date
            var byDate = new Dictionary<DateTime, List<string>>();

            foreach (var idea in ideas)
            {
                if (!byDate.ContainsKey(idea.Created.Value.Date))
                {
                    byDate.Add(idea.Created.Value.Date, new List<string>());
                }

                byDate[idea.Created.Value.Date].Add(idea.TeamName);
            }

            var result = new ChartViewModel
            {
                IdeaCount = ideas.Count,
                MemberCount = ideas
                    .Select(x => x.Member1)
                    .Concat(ideas.Select(x => x.Member2))
                    .Distinct()
                    .Where(x => x != null).Count(),
                ByMembers = byMembers
                    .OrderByDescending(x => x.Value.Count)
                    .ThenBy(x => x.Key)
                    .Select(x => new IdeasByMembers
                    {
                        Member = x.Key,
                        TeamNames = x.Value
                    })
                    .ToList(),

                ByDate = byDate
                    .OrderBy(x => x.Key)
                    .Select(x => new IdeasByDate
                    {
                        Created = x.Key,
                        TeamNames = x.Value
                    })
                    .ToList(),
            };

            return result;
        }
    }
}