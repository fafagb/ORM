using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Invoke(Action action);
    }
}
