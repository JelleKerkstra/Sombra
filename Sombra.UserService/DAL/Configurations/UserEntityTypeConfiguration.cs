﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.UserService.DAL.Configurations
{
    public class UserEntityTypeConfiguration : EntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.UserKey).IsRequired();
            entity.HasIndex(e => e.UserKey);
        }
    }
}