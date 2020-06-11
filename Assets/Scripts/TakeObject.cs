using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class TakeObject : MonoBehaviour
{
    private Transform targetRotation;

    //public Vector3 offset;

    public float zoom = 0.25f;
    //public float zoomMax = 10; 
    //public float zoomMin = 3;
    float X, Y;

    public Vector3 startPos;
    public Quaternion startRot;
    bool IsTake = false;

    [Header("Скорость вращения")]
    public float sensetivity = 3;

    [Header("Рука")]
    public Transform arm;

    [Header("Клавиша взаимодействия")]
    public KeyCode myKey = KeyCode.Mouse0;

    [Header("Индикатор")]
    public Image myImage;

    void OnMouseOver()
    {
        if (Input.GetKeyDown(myKey))
        {
            //transform.position = arm.position;
            transform.rotation = arm.rotation;
            IsTake = true;
            transform.SetParent(targetRotation);
        }
    }
    void Start()
    {
        startPos = GetComponent<Transform>().position;
        startRot = GetComponent<Transform>().rotation;
        targetRotation = arm;
        //offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
    }

    void Update()
    {
        //Притянуть
        if (IsTake)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                targetRotation.position, Time.deltaTime * 10);

            //transform.rotation = Quaternion.Lerp(transform.rotation,
            //    targetRotation.rotation, Time.deltaTime * 10);
            //transform.localEulerAngles = new Vector3(0, 0, targetRotation.position.y);
            //transform.eulerAngles = new Vector3(0, 0, targetRotation.position.y);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,
               startPos, Time.deltaTime * 10);
        }
        //Зум
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            targetRotation.transform.Translate(0, 0, -zoom);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            targetRotation.transform.Translate(0, 0, zoom);
        }
        //offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

        //Отпустить
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            transform.rotation = startRot;
            IsTake = false;
            transform.parent = null;
            X = 0;
            Y = 0;
            //offset.z = 0;
        }
        //Осмотр
        if (Input.GetMouseButton(0) && IsTake)
        {
            X += -Input.GetAxis("Mouse X") * sensetivity;
            Y += Input.GetAxis("Mouse Y") * sensetivity;
            transform.localEulerAngles = new Vector3(Y, X, 0);
        }
    }
}