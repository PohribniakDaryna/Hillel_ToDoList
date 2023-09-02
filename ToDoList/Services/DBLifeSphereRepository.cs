using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class DBLifeSphereRepository : ILifeSphereRepository
    {
        public ApplicationContext DbContext { get; }
        public DBLifeSphereRepository(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        public LifeSphere? GetLifeSphereById(int id)
        {
            return DbContext.Spheres.
                Include(x => x.Tasks).
                FirstOrDefault(x => x.Id == id);
        }

        public int AddLifeSphere(LifeSphere lifeSphere)
        {
            DbContext.Spheres.Add(lifeSphere);
            DbContext.SaveChanges();
            return lifeSphere.Id;
        }

        public LifeSphere DeleteLifeSphere(int id)
        {
            LifeSphere? lifeSphere = DbContext.Spheres.FirstOrDefault(x => x.Id == id);
            DbContext.Spheres.Remove(lifeSphere);
            DbContext.SaveChanges();
            return lifeSphere;
        }

        public bool UpdateLifeSphere(int id, LifeSphere newLifeSphere)
        {
            var lifeSphere = DbContext.Spheres.FirstOrDefault(x => x.Id == id);
            if (lifeSphere == null) return false;
            lifeSphere = newLifeSphere;
            DbContext.SaveChanges();
            return true;
        }
        public List<LifeSphere> GetLifeSpheres()
        {
            return DbContext.Spheres.Include(x => x.Tasks).ToList();
        }
    }
}