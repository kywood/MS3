using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
   
    public Player player;

    public Vector3 AdJustArea()
    {

        Walls walls = player.Walls.GetComponent<Walls>();

        GameObject worldObject = walls.WT;

        Vector3 wposT = new Vector3(walls.WT.transform.localPosition.x,
            walls.WT.transform.localPosition.y - walls.WT.GetComponent<BoxCollider2D>().size.y / 2.0f, 0);

        Vector3 wposB = new Vector3(walls.WB.transform.localPosition.x,
            walls.WB.transform.localPosition.y + walls.WB.GetComponent<BoxCollider2D>().size.y / 2.0f, 0);

        Vector3 wposL = new Vector3(walls.WL.transform.localPosition.x + walls.WL.GetComponent<BoxCollider2D>().size.x / 2.0f,
            walls.WL.transform.localPosition.y, 0);

        Vector3 wposR = new Vector3(walls.WR.transform.localPosition.x - walls.WR.GetComponent<BoxCollider2D>().size.x / 2.0f,
            walls.WR.transform.localPosition.y, 0);


        float width = wposR.x - wposL.x;
        float height = wposB.y - wposT.y;

        transform.localScale = new Vector3(width, height, 1);
        transform.localPosition = new Vector3(0.0f, wposT.y + (height / 2.0f), 1);

        return transform.localPosition;
    }


}
