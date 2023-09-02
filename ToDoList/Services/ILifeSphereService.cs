using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ILifeSphereService
    {
        void AddLifeSphere(CreateLifeSphereRequest request);
        LifeSphere? UpdateLifeSphere(int id, CreateLifeSphereRequest request);
        bool DeleteLifeSphere(int id);
        LifeSphere? GetLifeSphereById(int id);
        List<LifeSphere> GetLifeSpheres();
    }
}