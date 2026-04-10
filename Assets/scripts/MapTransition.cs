using UnityEngine;
using Cinemachine;

public class CameraDirectionalTransition : MonoBehaviour
{
    public enum Axis { Horizontal, Vertical }

    [SerializeField] private Axis axis = Axis.Horizontal;
    [SerializeField] private PolygonCollider2D roomBegin;
    [SerializeField] private PolygonCollider2D roomEnd;

    [SerializeField] private float pushDistance = 1.5f; 

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
        Transform player = collision.transform;

        PolygonCollider2D targetRoom;

        if (axis == Axis.Horizontal)
        {
            if (currentPos.x > lastPlayerPos.x)
            {
                targetRoom = roomEnd;

                
                player.position += new Vector3(pushDistance, 0f, 0f);
            }
            else
            {
                targetRoom = roomBegin;

                
                player.position += new Vector3(-pushDistance, 0f, 0f);
            }
        }
        else
        {
            if (currentPos.y > lastPlayerPos.y)
            {
                targetRoom = roomEnd;

                
                player.position += new Vector3(0f, pushDistance, 0f);
            }
            else
            {
                targetRoom = roomBegin;

               
                player.position += new Vector3(0f, -pushDistance, 0f);
            }
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            lastPlayerPos = player.transform.position;
        }
    }
}