using AuthServiceDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServiceInfrastructure.Persistence.Configurations
{
    public class RolesModelConfig : IEntityTypeConfiguration<RolesModel>
    {
        public void Configure(EntityTypeBuilder<RolesModel> builder)
        {
            builder.ToTable("ncd_role");
        }
    }
}
