using System;
using ProjectName.Application.Domain.Enums;
using ProjectName.Application.Domain.ValueObjects;

namespace ProjectName.Application.Domain.Common
{
    public abstract class Entity
    {
        public Entity()
        {
            ReferenceId = Guid.NewGuid();
            State = DataState.Active;
        }

        public long? Id { get; set; }

        public ReferenceId ReferenceId { get; set; }

        public DataState State { get; set; }
    }
}
