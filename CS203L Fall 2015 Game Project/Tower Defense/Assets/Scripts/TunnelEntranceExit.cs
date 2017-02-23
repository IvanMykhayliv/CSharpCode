///A script for handeling when the enemies are underground and surfaced
using UnityEngine;
using System.Collections;

/// <summary>
/// an enum for the entrance and exit of the tunnel
/// </summary>
public enum TunnelType
{
    Exit = 0,
    Entrance
}

public class TunnelEntranceExit : MonoBehaviour {
    public bool reverseDirection;
    public TunnelType tunnelType;
}
