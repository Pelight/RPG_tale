 using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private Entity entity;
    void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void CurrentStateOver() => entity.CallAnimationTrigger();
}
