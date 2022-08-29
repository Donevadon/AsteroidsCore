using CoreEngine.Entities.Objects.ControlledObjects.Enemy;

namespace CoreEngine.Entities.Objects.ControlledObjects;

public interface IObjectMotionController : IMotionController
{
    public void SetPursuer(IControlledObject obj);
}