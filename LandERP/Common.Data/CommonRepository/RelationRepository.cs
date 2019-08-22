﻿using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IRelationRepository : IRepository<Relation>
    {

    }
    public class RelationRepository : RepositoryBaseCodeFirst<Relation, CommonDbContext>, IRelationRepository
    {
        public RelationRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
