using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPosition;
    private float halfCameraWidth;
    [SerializeField] private ParallaxLayer[] bgLayer;

    void Awake()
    {
        mainCamera = Camera.main;
        halfCameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        InitLayers();
    }
    void FixedUpdate()
    {
        float currentCameraPosX = mainCamera.transform.position.x;
        float distanceToMove = currentCameraPosX - lastCameraPosition;
        lastCameraPosition = currentCameraPosX;

        float leftCameraEdge = currentCameraPosX - halfCameraWidth;
        float rightCameraEdge = currentCameraPosX + halfCameraWidth;

        foreach(ParallaxLayer layer in bgLayer)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(leftCameraEdge,rightCameraEdge);
        }
    }
    void InitLayers()
    {
        foreach(ParallaxLayer layer in bgLayer)
            layer.CalculateImageWidth();
    }
}
