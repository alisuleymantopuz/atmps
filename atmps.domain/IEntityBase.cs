using System;
namespace atmps.domain
{
    /// <summary>
    /// Entity base.
    /// </summary>
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
