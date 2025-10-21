using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private const float Playerspeed = 2.5f;
    private CharacterController charCtrl;
    private Coroutine stepsoundcoroutine;
    CameraController camera;

    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        camera = UnityEngine.Camera.main.GetComponent<CameraController>();
    }

    void FixedUpdate()
    {

        getCameraAngle();
        crouch();

        Vector3 dir = getDirection();

        stepsound(dir.sqrMagnitude);
        move(dir * Playerspeed);
        
    }

    private void getCameraAngle()
    {
        var camRot = camera.camerarot;
        transform.eulerAngles = new Vector3(0, camRot.y, 0);
    }

    Vector3 getDirection()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        if (dir.sqrMagnitude > 1f)
            dir.Normalize();

        return dir = transform.TransformDirection(dir);
    }

    void crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            gameObject.tag = "Crouching";
        else
            gameObject.tag = "Player";
    }

    private void move(Vector3 vector)
    {
        charCtrl.SimpleMove(vector);
    }

    void stepsound(float playercurrentspeed)
    {
        if (playercurrentspeed > 0.01f)
        {
            if (stepsoundcoroutine == null) stepsoundcoroutine = StartCoroutine(stepsoundplay());
        }
        else
        {
            if (stepsoundcoroutine != null) StopCoroutine(stepsoundcoroutine);
            stepsoundcoroutine = null;
        }
    }

    IEnumerator stepsoundplay()
    {
        ManagerObject.instance.eventManager.OnPlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.stepsound].Result, 0.2f, false);
        yield return new WaitForSeconds(0.5f);
        stepsoundcoroutine = null;

    }
}
