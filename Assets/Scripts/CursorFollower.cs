using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollower : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        var pos = Mouse.current.position.ReadValue();
        transform.position = new Vector3(pos.x, pos.y, transform.position.z) + offset;
    }
}
