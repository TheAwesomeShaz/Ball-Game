using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    public bool isDead;
    public bool isBlue;
    int blueScore = 5;
    int yellowScore = 10;

    public Material playerMat;
    Material cubeMat;

    // Start is called before the first frame update
    void Start()
    {
        cubeMat = GetComponent<MeshRenderer>().material;

        isDead = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent <Player>())
        {
            //change color of cube to purple
            cubeMat.color = playerMat.color;

            if (!isDead && isBlue)
            {
                GameController.instance.AddScore(blueScore,gameObject.tag);

            }
            else if (!isDead && !isBlue)
            {
                GameController.instance.AddScore(yellowScore,gameObject.tag);
            }
            else if (isDead)
            {
                //DO NOTHING
            }
            isDead = true;
        }

    }
}
