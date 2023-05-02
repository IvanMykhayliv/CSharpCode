using UnityEngine;

public class TransformTracker : MonoBehaviour
{
    public bool bCanTrackRot;
    public GameObject trackee;
    public Vector3 offset;

    public GameObject target;

    void Update()
    {
        TrackPosition();

        if (bCanTrackRot)
        {
            TrackTargetBlindly();
        }
    }

    public void TrackPosition()
    {
        transform.position = trackee.transform.position + offset;
    }

    //Blindly tracks target with an additional Vector offset
    public void TrackTargetBlindly()
    {
        transform.LookAt(target.transform.position + offset);
    }
}