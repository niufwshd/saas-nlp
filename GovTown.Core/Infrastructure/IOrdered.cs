using System;

namespace GovTown
{
    public interface IOrdered
    {
        // TODO: (MC) Make Nullable!
        int Ordinal { get; }
    }
}
