using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Repository.GenericRepos;
using Meccsek;

namespace Repository.ResultRepos
{
    public interface IResultRepository : IRepository<results>
    {
        List<MatchData> GetAllMatches(); // team1+team2+goal1+goal2
        List<GlobalResults> GetAllResults(); // teamname + numberOfWins
    }
}
