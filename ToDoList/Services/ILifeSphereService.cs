using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ILifeSphereService
    {
        void AddLifeSphere(CreateLifeSphereRequest request);
        ILifeSphere? UpdateLifeSphere(int id, CreateLifeSphereRequest request);
        bool DeleteLifeSphere(int id);
        ILifeSphere? GetLifeSphereById(int id);
        List<LifeSphere> GetLifeSpheres();
    }
}