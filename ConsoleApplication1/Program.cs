using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.ResultRepos;
using Repository.TeamRepos;
using Meccsek;

namespace ConsoleApplication1
{
    class Program
    {
        static ITeamRepository teamRepo;
        static IResultRepository resultRepo;
        static Program()
        {
            Database1Entities nhl = new Database1Entities();
            teamRepo = new TeamRepository(nhl);
            resultRepo = new ResultRepository(nhl);
        }
        static void Main(string[] args)
        {
            foreach (var akt in teamRepo.GetAll())
            {
                Console.WriteLine("{0} = {1}", akt.team_id, akt.team_name);
            }
            Console.WriteLine();
            teams Detroit = new teams();
            Detroit.team_name = "Detroit Red Wings";
            teamRepo.Insert(Detroit);
            foreach (var akt in teamRepo.GetAll())
            {
                Console.WriteLine("{0} = {1}", akt.team_id, akt.team_name);
            }
            Console.ReadLine();

            foreach (var akt in resultRepo.GetAllResults())
            {
                Console.WriteLine("{0}: {1} wins", akt.TeamName, akt.NumberOfWins);
            }
            foreach (var akt in resultRepo.GetAllMatches())
            {
                Console.WriteLine("{0} : {1} \t\t\t {2} : {3} ", akt.Team1, akt.Team2, akt.Goal1, akt.Goal2);
            }
            Console.ReadLine();
        }
    }
}
