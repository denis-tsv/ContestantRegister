﻿namespace ContestantRegister.Controllers._Common.Queries
{
    public class GetEntityByIdForDeleteQuery<TEntity> : EntityIdBaseQuery<TEntity>
    {
        public string[] IncludeProperties { get; set; }
    }
}
