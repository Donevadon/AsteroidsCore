using CoreEngine.Behaviors.Controllers;
using CoreEngine.Core.Models;

namespace CoreEngine.Entities.Objects.ControlledObjects.Enemy;

public class PursuerAlien : Alien
{
    public PursuerAlien(AlienModel model) : base(model, new PursueTarget(model.Target))
    {
        
    }
}