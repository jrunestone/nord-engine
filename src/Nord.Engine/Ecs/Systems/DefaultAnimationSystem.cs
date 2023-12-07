using Arch.Core;
using Nord.Engine.Ecs.Components;

namespace Nord.Engine.Ecs.Systems;

public class DefaultAnimationSystem : SystemBase
{
    private readonly QueryDescription _query = new QueryDescription()
        .WithAll<SpriteComponent, AnimationComponent>();

    public DefaultAnimationSystem(World world) : base(world)
    {
        
    }
    
    public override void Update(float dt)
    {
        World.Query(in _query, (ref SpriteComponent sprite, ref AnimationComponent animation) =>
        {
            var currentAnimation = animation.CurrentAnimation ?? animation.Animations.First();
            var frame = currentAnimation.CurrentFrame ?? currentAnimation.Frames.First();
            
            animation.FrameCounter += dt;

            if (animation.FrameCounter >= frame.Delay)
            {
                var nextFrameIndex = currentAnimation.Frames.IndexOf(frame) + 1;
                
                frame = currentAnimation.Frames.Count > nextFrameIndex
                    ? currentAnimation.Frames[nextFrameIndex]
                    : currentAnimation.Frames.First();
                
                animation.FrameCounter = 0;
            }

            currentAnimation.CurrentFrame = frame;
            sprite.Sprite.TextureRect = frame.Coords;
        });
    }
}