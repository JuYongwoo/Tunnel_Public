using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public bool moveon = false;


    void Update()
    {

        if (moveon)
        {
            if (this.transform.position.x > -11)
                this.transform.position -= new Vector3(Time.deltaTime, 0, 0);
            else
                moveon = false;

        }

    }
}
