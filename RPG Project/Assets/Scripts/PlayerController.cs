using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    public Interactable focus;
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
        if (EventSystem.current.IsPointerOverGameObject())
            return; //Предотвращает отдачу команд сквозь графический интерфейс
                    //Всё, что ниже не будет работать если мышь находится над графическим интерфейсом
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
                RemoveFocus();
            }
        }

        //Нажата правая кл мыши.
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем можно ли взаимодействовать с объектом

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
                
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }
}
