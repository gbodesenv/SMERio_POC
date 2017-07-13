using System;
namespace Rio.SME.Domain.Contracts.Entities
{
    public interface IEntity : ICloneable
    {
        int Id { get; set; }
        bool Excluido { get; set; }

        void Validate();
    }
}
