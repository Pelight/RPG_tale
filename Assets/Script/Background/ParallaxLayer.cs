using UnityEngine;
[System.Serializable]
public class ParallaxLayer 
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float offset = 10;
    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMove) => background.position += Vector3.right * (distanceToMove * parallaxMultiplier);

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imgRightEdge = (background.position.x + imageHalfWidth) - offset;
        float imgLeftEdge = (background.position.x - imageHalfWidth) + offset;
        
        if(imgRightEdge < cameraLeftEdge )
            background.position += Vector3.right * imageFullWidth;
        else if(imgLeftEdge > cameraRightEdge)
            background.position += Vector3.right * -imageFullWidth;
    }

}
