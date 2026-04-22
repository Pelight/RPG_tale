 using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private Player player;
    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void CurrentStateOver() => player.CallAnimationTrigger();
}
