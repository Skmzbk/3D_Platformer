using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowYRotation : MonoBehaviour
{
    [Header("Minimap rotations")]
    public Transform Player;
    public float playerOffset = 10f;
    public bool rotacion;

    private void Update()
    {
        if(Player != null)
        {
            //Follow player from above
            transform.position = new Vector3(Player.position.x, Player.position.y + playerOffset,Player.position.z);
            //rotate map with player
            if (rotacion)
            {
                transform.rotation = Quaternion.Euler(90f, Player.eulerAngles.y, 0f);
            }
        }
    }
}
