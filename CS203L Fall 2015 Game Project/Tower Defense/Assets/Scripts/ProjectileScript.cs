using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour 
{
    public int AttackPower = 0;
    public float speed;
    private Rigidbody2D rigi;
    private TowerScript Tower;
    public float delay;
    private bool yorn = true;
    private bool yorn2 = true;

    void Update()
    {
        rigi = GetComponent<Rigidbody2D>();
        rigi.velocity = transform.right * -speed;// * GameController.EnemySpeed; makes arrows not move when enemies stop moving why is this here?
        if (yorn)
        {
            StartCoroutine(WaitForDelay());
        }
        else
        {
            if (yorn2)
            {
                StartCoroutine(WaitAndDestroy());
                yorn2 = false;
            }
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(delay * (1 / GameController.EnemySpeed));
       
        Destroy (gameObject);
    }

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(0.1f * (1 / GameController.EnemySpeed));
        yorn = false;
    }
}
