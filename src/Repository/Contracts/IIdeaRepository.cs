using Domain;
using System.Collections.Generic;

namespace Repository.Contracts
{
    public interface IIdeaRepository
    {
        IEnumerable<Idea> GetAll();

        Idea Get(string id);

        void Update(Idea instance);

        void Create(Idea idea);

        ChartViewModel GetChartData();
    }
}