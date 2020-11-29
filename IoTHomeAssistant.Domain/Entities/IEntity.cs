using System;

namespace IoTHomeAssistant.Domain.Entities
{
    public interface IEntity<T> where T : struct, IEquatable<T>
    {
        T Id { get; set; }
    }
}