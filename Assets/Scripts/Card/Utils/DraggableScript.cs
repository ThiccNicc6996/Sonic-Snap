using UnityEngine;

public class DraggableScript : MonoBehaviour
{
    private float startPosX;
    private float startPosY;

    //Whether the card is being dragged by the player
    private bool isHeld = false;

    private Card cardScript;

    void Start() {
        cardScript = this.transform.gameObject.GetComponent<Card>();
    }

    void Update()
    {
        if (isHeld) {
            Vector3 mousePos = getMousePosition();

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0) && canBeMoved()) {
            Vector3 mousePos = getMousePosition();

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isHeld = true;
        }
    }

    private void OnMouseUp() {
        isHeld = false;
    }

    private Vector3 getMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private bool canBeMoved() {
        return (!cardScript.isLocked() || cardScript.isMovable());
    }
}
