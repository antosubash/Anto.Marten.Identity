using Marten;
using Marten.Identity;
using Marten.Schema;
using MartenIdentitySample.Data;

namespace MartenIdentitySample
{
    public class InitialData : IInitialData
    {
        public static void SeedData(IDocumentStore store)
        {
            using var session = store.LightweightSession();
            
        }

        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            var role = new ApplicationRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            var session = store.LightweightSession();

            var claim = new IdentityClaim
            {
                Type = "role",
                Value = "Admin"
            };

            var can_manage_users = new IdentityClaim
            {
                Type = "permission",
                Value = "can_manage_users"
            };

            role.Claims ??= new List<IdentityClaim>();
            role.Claims.Add(claim);
            role.Claims.Add(can_manage_users);
            session.Store(role);
            await session.SaveChangesAsync(cancellation);
        }
    }
}
                