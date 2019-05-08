using System;

namespace BomberMan.Data.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
