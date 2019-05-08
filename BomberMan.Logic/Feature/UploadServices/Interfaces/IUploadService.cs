using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.UploadServices.Interfaces
{
    public interface IUploadService<in T> where T : EntityBaseDto
    {
        void Upload(T newEntity);

        bool SaveChanges();
    }
}
