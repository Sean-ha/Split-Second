using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    private List<Vector3> positions;
    private static bool canMove;
    private int positionCount;

    public void SetPositions(List<Vector3> positionToCopy)
    {
        positionCount = 0;
        positions = new List<Vector3>();
        canMove = true;

        foreach(Vector3 pos in positionToCopy)
        {
            positions.Add(pos);
        }
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            if(positionCount < positions.Count)
            {
                transform.position = positions[positionCount];
                positionCount++;
            }
        }
    }

    public void RestartClone(Vector3 initialPosition)
    {
        positionCount = 0;
        transform.position = initialPosition;
    }

    public static void SetCanMove(bool value)
    {
        canMove = value;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<Button>().PushButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<Button>().UnpushButton();
        }
    }
}
