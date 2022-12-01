using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    [SerializeField] //ī�޶����
    Vector3 m_Cameradir; 
    [SerializeField]  // ��������
    Vector3 finalDir;  
    [SerializeField]  //���󰡾��� ������Ʈ ����
    Transform objectTofollow;
    [SerializeField]  //ī�޶�����
    Transform realCamera;
    [SerializeField]  //���󰥼ӵ�
    float followSpeed = 100.0f;
    [SerializeField]  //���콺 ����
    float sensitivity = 100.0f;
    [SerializeField]  //��������    
    float clampAngle = 70.0f;
    [SerializeField]  //ī�޶�� �÷��̾� �ּҰŸ�
    float minDistance;
    [SerializeField]  //ī�޶�� �÷��̾� �ִ�Ÿ�
    float maxDistance;
    [SerializeField]  //�����Ÿ�
    float finalDistance;
    [SerializeField] // ī�޶� �ε巯������
    float smoothness = 10.0f;
    float rotX;  //���콺 ��ǲ��������
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
