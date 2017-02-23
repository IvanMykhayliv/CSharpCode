///a script that snaps the tower to the corner prefab by Ben Melanson
using UnityEngine;
using System.Collections;

public class SnapToCorner : MonoBehaviour {

    private bool snapped = false;///<A bool for determining if a tower is snapped 

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Tower" && !snapped)
        {
            col.gameObject.GetComponent<TowerScript>().snap = true;
            col.gameObject.GetComponent<TowerScript>().snapLocation = this.gameObject.transform;
            snapped = true;

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Tower" && snapped)
        {
            col.gameObject.GetComponent<TowerScript>().snap = false;
            snapped = false;
        }
    }
}
