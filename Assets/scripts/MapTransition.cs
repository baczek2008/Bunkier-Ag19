using UnityEngine;
using Cinemachine;

public class CameraDirectionalTransition : MonoBehaviour
{
    public enum Axis { Horizontal, Vertical }

    [SerializeField] private Axis axis = Axis.Horizontal;
    [SerializeField] private PolygonCollider2D roomBegin;
    [SerializeField] private PolygonCollider2D roomEnd;

    private CinemachineConfiner confiner;
    private Vector3 lastPlayerPos;
    private bool canSwitch = true;

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || !canSwitch)
            return;

        Vector3 currentPos = collision.transform.position;

        PolygonCollider2D targetRoom;

        if (axis == Axis.Horizontal)
        {
            // sprawdzamy kierunek ruchu X
            if (currentPos.x > lastPlayerPos.x)
                targetRoom = roomEnd;   // idzie w prawo
            else
                targetRoom = roomBegin; // idzie w lewo
        }
        else
        {
            // sprawdzamy kierunek ruchu Y
            if (currentPos.y > lastPlayerPos.y)
                targetRoom = roomEnd;   // idzie w górê
            else
                targetRoom = roomBegin; // idzie w dó³
        }

        confiner.m_BoundingShape2D = targetRoom;
        confiner.InvalidatePathCache();

        canSwitch = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSwitch = true;
        }
    }

    private void Update()
    {
        // zapisujemy ostatni¹ pozycjê gracza
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            lastPlayerPos = player.transform.position;
        }
    }
}