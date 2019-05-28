﻿using Domain;
using System.Collections.Generic;

namespace Repository.Contracts
{
    public interface IIdeaRepository
    {
        IEnumerable<Idea> GetAllIdeas();

        IEnumerable<Idea> GetIdeas(int pageNumber = 1, int pageCount = 10);

        Idea GetIdea(string id);

        void UpdateIdea(Idea instance);

        void CreateIdea(Idea idea);

        ChartViewModel GetChartData();
    }
}