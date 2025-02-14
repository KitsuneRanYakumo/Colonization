using UnityEngine;

public class CollectorBaseDetector : MonoBehaviour
{
    public bool TryDetectCollectorBase(out CollectorBase collectorBase)
    {
        collectorBase = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray);

        foreach (RaycastHit raycastHit in raycastHits)
        {
            if (raycastHit.transform.TryGetComponent(out collectorBase))
            {
                return true;
            }
        }

        return false;
    }
}