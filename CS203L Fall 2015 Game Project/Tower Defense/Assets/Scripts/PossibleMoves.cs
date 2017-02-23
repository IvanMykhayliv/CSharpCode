using UnityEngine;
using System.Collections;

public class PossibleMoves : MonoBehaviour {
    public bool NoPossibleMoves
    {
        get
        {
            return (!Up && !Down && !Left && !Right);
        }
    }
    public bool Up { get; set; }
    public bool Down { get; set; }
    public bool Left { get; set; }
    public bool Right { get; set; }
}
