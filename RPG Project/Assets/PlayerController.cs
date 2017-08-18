using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    public LayerMask movementMask;

    private Camera cam;
    private PlayerMotor motor;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
        //Ловим нажатие левой кл мыши
		if (Input.GetMouseButtonDown(0))
        {
            //Строим луч через место клика мыши в 3д мир
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //получаем координаты клика мыши в мире если клик пришелся на объект с movementMask
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Двигаемся в сторону объекта по которому нажали
                motor.MoveToPoint(hit.point);

                //Убираем фокус со всех объектов
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

            }
        }
    }
}
