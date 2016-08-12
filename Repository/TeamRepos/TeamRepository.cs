using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.GenericRepos;
using System.Data.Entity;
using Meccsek;

namespace Repository.TeamRepos
{
    public class TeamRepository : GenericEFRepository<teams>, ITeamRepository
    {
        public TeamRepository(DbContext context) : base(context)
        {
        }

        public override teams GetById(int id)
        {
            return GetAll().SingleOrDefault(x => x.team_id == id);
        }

        public override void Insert(teams newEntity)
        {
            newEntity.team_id = GetTeamId();
            base.Insert(newEntity);
        }
        public int GetTeamId()
        {
            var rawQuery = context.Database.SqlQuery<int>("select next value for seq_teams");
            return rawQuery.Single();
        }
        //+ override void Insert(teams newentity) ... Insert működik, yay!
    }
}
