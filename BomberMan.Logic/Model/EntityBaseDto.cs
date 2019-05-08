using System;

namespace BomberMan.Logic.Model
{
    public abstract class EntityBaseDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
