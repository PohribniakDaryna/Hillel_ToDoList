using ToDoList.Models;

namespace ToDoList.Services
{
    public class LifeSphereRegister : ILifeSphereRegister
    {
        private readonly List<LifeSphere> lifeSpheres = new();
        public void AddLifeSphere(LifeSphere lifeSphere)
        {
            lifeSpheres.Add(lifeSphere);
        }

        public bool DeleteLifeSphere(int id, string title)
        {
            int count = lifeSpheres.Count;
            var lifeSphere = lifeSpheres.FirstOrDefault(x => x.Id == id && x.Title == title);
            if (lifeSphere != null)
            {
                lifeSpheres.Remove(lifeSphere);
                int countAfterDeleting = lifeSpheres.Count;
                if (countAfterDeleting < count)
                    return true;
            }
            return false;
        }

        public ILifeSphere? GetLifeSphereById(int id)
        {
            return lifeSpheres.FirstOrDefault(x => x.Id == id);
        }

        public List<LifeSphere> GetLifeSpheres()
        {
            return lifeSpheres;
        }

        public ILifeSphere? UpdateLifeSphere(int id, CreateLifeSphereRequest request)
        {
            var lifeSphere = lifeSpheres.FirstOrDefault(x => x.Id == id);
            if (lifeSphere != null)
            {
                lifeSphere.Title = request.Title;
                lifeSphere.Description = request.Description;
                lifeSphere.Grade = request.Grade;
            }
            return lifeSphere;
        }
    }
}