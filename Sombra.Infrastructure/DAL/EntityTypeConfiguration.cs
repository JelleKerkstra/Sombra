﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sombra.Infrastructure.DAL
{
    public abstract class EntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}