using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ILifeSphereRegister
    {
        void AddLifeSphere(LifeSphere lifeSphere);
        bool DeleteLifeSphere(int id, string title);
        ILifeSphere? GetLifeSphereById(int id);
        List<LifeSphere> GetLifeSpheres();
        ILifeSphere? UpdateLifeSphere(int id, CreateLifeSphereRequest request);
    }
}