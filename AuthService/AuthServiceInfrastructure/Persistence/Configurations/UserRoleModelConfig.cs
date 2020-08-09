using AuthServiceDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServiceInfrastructure.Persistence.Configurations
{
    public class UserRoleModelConfig : IEntityTypeConfiguration<UserRoleModel>
    {
        public void Configure(EntityTypeBuilder<UserRoleModel> builder)
        {
            builder.ToTable("ncd_user_role");
            //Create CK
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            // Create Relation One -> Many for UserRoleModel.Role -> Role.UserRoleModel
            // with RoleId as ForeignKey
            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Create Relation One -> Many for UserRoleModel.User -> User.UserRoles
            // with UserId as ForeignKey
            builder.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
