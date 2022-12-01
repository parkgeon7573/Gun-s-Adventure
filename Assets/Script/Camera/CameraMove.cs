using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    [SerializeField] //카메라방향
    Vector3 m_Cameradir; 
    [SerializeField]  // 최종방향
    Vector3 finalDir;  
    [SerializeField]  //따라가야할 오브젝트 정보
    Transform objectTofollow;
    [SerializeField]  //카메라정보
    Transform realCamera;
    [SerializeField]  //따라갈속도
    float followSpeed = 100.0f;
    [SerializeField]  //마우스 감도
    float sensitivity = 100.0f;
    [SerializeField]  //각도제한    
    float clampAngle = 70.0f;
    [SerializeField]  //카메라와 플레이어 최소거리
    float minDistance;
    [SerializeField]  //카메라와 플레이어 최대거리
    float maxDistance;
    [SerializeField]  //최종거리
    float finalDistance;
    [SerializeField] // 카메라 부드러움정도
    float smoothness = 10.0f;
    float rotX;  //마우스 인풋받을변수
    float rotY;


    void MouseInput()
    {
        rotX += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    void moveCamera()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);
        finalDir = transform.TransformPoint(m_Cameradir * maxDistance);

        RaycastHit hit;
        //Debug.DrawRay(transform.position, finalDir, Color.red, 100.0f) ;
        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else finalDistance = maxDistance;
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, m_Cameradir * finalDistance, Time.deltaTime * smoothness);
    } 


    
    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        m_Cameradir = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }



    // Update is called once per frame
    void Update()
    {
        MouseInput();
        moveCamera();
    }
}
