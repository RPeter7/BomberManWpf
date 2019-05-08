using BomberMan.Data.Entities;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.UpdateServices.Interfaces
{
    public interface IUpdateService<in T> where T : EntityBase
    {
        void Update(T newEntity);

        bool SaveChanges();
    }
}
