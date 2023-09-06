using ToDoList.Models;

namespace ToDoList.Services
{
    public class LifeSphereService : ILifeSphereService
    {
        private readonly ILifeSphereRepository register;
        public LifeSphereService(ILifeSphereRepository register)
        {
            this.register = register;
        }

        public LifeSphere? GetLifeSphereById(int id)
        {
            return register.GetLifeSphereById(id);
        }

        public void AddLifeSphere(CreateLifeSphereRequest request)
        {
            LifeSphere lifeSphere = new();
            InitializeLifeSphere(lifeSphere, request);
            register.AddLifeSphere(lifeSphere);
        }

        public bool DeleteLifeSphere(int id)
        {
            int count = register.GetLifeSpheres().Count;
            var lifeSphere = register.GetLifeSphereById(id);
            if (lifeSphere == null) return false;
            register.DeleteLifeSphere(id);
            int countAfterDeleting = register.GetLifeSpheres().Count;
            if (countAfterDeleting >= count) return false;
            return true;
        }

        public LifeSphere? UpdateLifeSphere(int id, CreateLifeSphereRequest request)
        {
            var lifeSphere = register.GetLifeSphereById(id);
            if (lifeSphere != null)
            {
                InitializeLifeSphere(lifeSphere, request);
                register.UpdateLifeSphere(id, lifeSphere);
            }
            return lifeSphere;
        }

        public List<LifeSphere> GetLifeSpheres()
        {
            return register.GetLifeSpheres();
        }

        private static LifeSphere InitializeLifeSphere(LifeSphere lifeSphere, CreateLifeSphereRequest request)
        {
            lifeSphere.Title = request.Title;
            lifeSphere.Description = request.Description;
            lifeSphere.Grade = request.Grade;
            return lifeSphere;
        }
    }
}