using UnityEngine;

public class testControl : MonoBehaviour
{
    // Public variables for each finger joint, allowing you to assign these in Unity
    public Transform thumbJoint;
    public Transform indexJoint;
    public Transform middleJoint;
    public Transform ringJoint;
    public Transform pinkyJoint;

    // Rotation angle sliders for each finger (between -90 and 90 degrees)
    [Range(-60, 0)] public float thumbAngle = 0;
    [Range(-150, 0)] public float indexAngle = 0;
    [Range(-150, 0)] public float middleAngle = 0;
    [Range(-150, 0)] public float ringAngle = 0;
    [Range(-150, 0)] public float pinkyAngle = 0;

    // Update method to apply rotations every frame
    void Update()
    {
        // Rotating each finger joint based on the angle you set in the Inspector
        if (thumbJoint != null) thumbJoint.localRotation = Quaternion.Euler(thumbAngle, thumbAngle, thumbAngle);
        if (indexJoint != null) indexJoint.localRotation = Quaternion.Euler(0, indexAngle, 0);
        if (middleJoint != null) middleJoint.localRotation = Quaternion.Euler(0, middleAngle, 0);
        if (ringJoint != null) ringJoint.localRotation = Quaternion.Euler(0, ringAngle, 0);
        if (pinkyJoint != null) pinkyJoint.localRotation = Quaternion.Euler(0, pinkyAngle, 0);
    }
}
