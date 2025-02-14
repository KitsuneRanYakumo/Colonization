using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool TryDetectGround(out Vector3 touchPoint)
    {
        touchPoint = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.GetComponent<Ground>())
            {
                touchPoint = raycastHit.point;
                return true;
            }
        }

        return false;
    }
}