using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ILifeSphereRepository
    {
        int AddLifeSphere(LifeSphere lifeSphere);
        ILifeSphere? GetLifeSphereById(int id);
        List<LifeSphere> GetLifeSpheres();
        LifeSphere DeleteLifeSphere(int id);
        bool UpdateLifeSphere(int id, LifeSphere lifeSphere);
    }
}