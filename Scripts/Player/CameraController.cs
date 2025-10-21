using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 camerarot;
    private const float rotSpeed = 70f;
    private Vector3 playerPos;


    private void Update()
    {
        if (ManagerObject.instance.eventManager.ThisScenePlayer) playerPos = ManagerObject.instance.eventManager.ThisScenePlayer.transform.position;
        transform.position = playerPos;
        if (Input.GetKey(KeyCode.LeftControl)) playerPos.y -= 0.5f;//1인칭 게임이기 때문에 실제 플레이어 아닌 카메라만 아래로

        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        camerarot.y += mx * rotSpeed * Time.deltaTime;
        camerarot.x -= my * rotSpeed * Time.deltaTime;
        camerarot.x = Mathf.Clamp(camerarot.x, -80f, 80f);

        transform.rotation = Quaternion.Euler(camerarot.x, camerarot.y, 0f);


    }
}
