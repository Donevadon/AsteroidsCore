using System;
using CoreEngine.Core.Models;

namespace CoreEngine.Core
{
    public interface IFragmentsFactory
    {
        IObject GetSmallAsteroid(FragmentAsteroidModel model);
        event Action<IObject> ObjectCreated;
    }
}