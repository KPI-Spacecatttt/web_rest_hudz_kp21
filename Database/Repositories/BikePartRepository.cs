using web_rest_hudz_kp21.Models;

namespace web_rest_hudz_kp21.Database.Repositories
{
    public class BikePartRepository : GenericRepository<BikePart>
    {
        public BikePartRepository(ApplicationContext context) : base(context)
        { }
    }
}