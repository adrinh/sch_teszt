using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.GenericRepos;
using System.Data.Entity;
using Meccsek;

namespace Repository.ResultRepos
{
    public class ResultRepository : GenericEFRepository<results>, IResultRepository
    {
        public ResultRepository(DbContext context) : base(context)
        {
        }

        public List<MatchData> GetAllMatches()
        {
            var q = from result in context.Set<results>()
                    join score1 in context.Set<scores>() on result.result_score1 equals score1.score_id
                    join score2 in context.Set<scores>() on result.result_score2 equals score2.score_id
                    join team1 in context.Set<teams>() on score1.score_team equals team1.team_id
                    join team2 in context.Set<teams>() on score2.score_team equals team2.team_id
                    select new MatchData()
                    {
                        Team1 = team1.team_name,
                        Team2 = team2.team_name,
                        Goal1 = score1.score_goals.Value,
                        Goal2 = score2.score_goals.Value
                    };
            var q2 = from result in context.Set<results>()
                     select new MatchData()
                     {
                         Team1 = result.scores.teams.team_name,
                         Team2 = result.scores1.teams.team_name,
                         Goal1 = result.scores.score_goals.Value,
                         Goal2 = result.scores1.score_goals.Value
                     };
            Console.WriteLine("Q1: {0} \r\n\r\nQ2: {1}", q, q2);
            return q2.ToList();
        }

        public List<GlobalResults> GetAllResults()
        {
            //string sql = @"SELECT sub.num as NumberOfWins, team_name as TeamName
            //                FROM 
            //                (
            //                    SELECT count(1) as num, score_team 
            //                    FROM scores 
            //                    WHERE score_wins=1 
            //                    GROUP BY score_team
            //                ) sub, teams WHERE sub.score_team=team_id";
            //return context.Database.SqlQuery<GlobalResults>(sql).ToList();
            //eredmény ugyanaz
            var q = from akt in context.Set<scores>()
                    group akt by akt.teams into g
                    select new GlobalResults() { NumberOfWins = g.Count(x => x.score_wins == 1), TeamName = g.Key.team_name };
            //Console.WriteLine("SQL:{0} \r\n\r\nEF: {1}", sql, q);
            return q.ToList();
        }

        public override results GetById(int id)
        {
            return GetAll().SingleOrDefault(x => x.result_id == id);
        }
    }
}
