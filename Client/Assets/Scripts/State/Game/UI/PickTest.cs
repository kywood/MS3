using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Defines;

public class PickTest : MonoBehaviour
{

    public GameObject Target;
    public GameObject ShootBody;
    public GameObject Line = null;
    public GameObject LineReverse = null;


    public float Radius = 0.0f;
    //public Player Player;

    public GameObject ShootObject;

    public GameObject HitMark;

    bool bMousePress = false;
    float currentDeltaTime = 0.0f;

    Vector3 curPosition;


    public void SetHitMarkSize(float diameter)
    {
        HitMark.transform.localScale = new Vector3(diameter, diameter, 1);
    }

    eDir GetXDir(float x1, float x2)
    {
        int compare = x1.CompareTo(x2);

        if (compare == 1)
        {
            return eDir.Left;
        }
        else if (compare == -1)
        {
            return eDir.Right;
        }
        else
        {
            return eDir.None;
        }
        return eDir.None;
    }

    public void RePlaceLimitPosition(ref Vector3 vPos)
    {
        Vector2 vStart = CMath.ConvertV3toV2(ShootBody.transform.position);
        Vector2 vEnd = CMath.ConvertV3toV2(vPos);

        float fDistance = Util.Distance(vStart, vEnd);
        float fAngle = CMath.GetAngle(vStart, vEnd);

        float LimitAngle = 0.0f;

        if (fAngle > Defines.G_SHOOT_LIMIT_ANGLE && fAngle < 180.0f - Defines.G_SHOOT_LIMIT_ANGLE)
        {
        }
        else
        {
            eDir xDir = GetXDir(ShootBody.transform.position.x, vPos.x);

            if (xDir == eDir.Left)
            {
                LimitAngle = 180.0f - Defines.G_SHOOT_LIMIT_ANGLE;
            }
            else
            {
                LimitAngle = Defines.G_SHOOT_LIMIT_ANGLE;
            }
            float RadianValue = LimitAngle * Mathf.Deg2Rad;

            //vPos = new Vector3(Mathf.Cos(RadianValue), Mathf.Sin(RadianValue), 0.0f) * fDistance + transform.position;


            vPos = new Vector3(Mathf.Cos(RadianValue), Mathf.Sin(RadianValue), 0.0f).normalized * fDistance;
        }
    }
    bool HitChecker(out Vector3 HitPoint)
    {
        HitPoint = new Vector3();


        Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray2D ray = new Ray2D(wp, Vector2.zero);

        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.name.Equals("GameArea"))
            {
                HitPoint = hit.point;
                return true;
            }
        }

        //if(hit.collider != null)
        //{
        //    if (hit.collider.name.Equals("GameArea"))
        //    {
        //        HitPoint = hit.point;
        //        return true;
        //    }
        //}





        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        //RaycastHit[] Hits = Physics.RaycastAll(ray, Mathf.Infinity);
        //for (int i = 0; i < Hits.Length; i++)
        //{
        //    if (Hits[i].collider.name.Equals("GameArea"))
        //    {
        //        HitPoint = Hits[i].point;
        //        Debug.Log("In Aera");
        //        return true;
        //    }
        //}
        return false;
    }


    private void Update()
    {



        if (Input.GetMouseButton(0))
        {


            Vector3 curPosition;
            if (HitChecker(out curPosition))
            {
                bMousePress = true;
            }
            else
            {
                //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                return;
            }

            bMousePress = true;

            curPosition.z = 0.0f;

            //Pick pick = Player.Pick.GetComponent<Pick>();
            RePlaceLimitPosition(ref curPosition);


            Target.transform.position = curPosition;


            //Target.transform.localPosition = curPosition;

            //bMousePress = true;

            //Vector3 wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //wPos.z = 0;
            //Target.transform.position = wPos;

            //float dis = Util.Distance(Target.transform.position, ShootBody.transform.position);
            //float angle = CMath.GetAngle(Target.transform.position, ShootBody.transform.position);

            currentDeltaTime += Time.deltaTime;

            if (currentDeltaTime >= (1.0f / 4.0f))
            {

                //Debug.Log($"currentDeltaTime : {currentDeltaTime}");
                currentDeltaTime = 0;
            }
            //Time.deltaTime
            //Debug.Log(" Dis : " + dis + " Angle : " + angle);
        }
        else
        {
            if (bMousePress)
            {
                //GetPlayer().SetPlayerState(PlayerStateManager.E_PLAYER_STATE.RUN);
                ////                GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.RUN);
                //Shoot(
                //    GetPlayer().LocalScale
                //    );



            }

            bMousePress = false;
        }
    }

    void LateUpdate()
    {
        Line.transform.position = ShootBody.transform.position;

        //LineReverse.SetActive(false);

        Vector2 vStart = CMath.ConvertV3toV2(ShootBody.transform.position);
        Vector2 vEnd = CMath.ConvertV3toV2(Target.transform.position);

        float _fShootAngle = 0.0f;
        _fShootAngle = CMath.GetAngle(vStart, vEnd);

        Line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _fShootAngle));

        ShootObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _fShootAngle - 90.0f));

        Vector2 OriSize = Line.GetComponent<RectTransform>().sizeDelta;

        Vector3 vShootBodyPos = new Vector3(ShootBody.transform.position.x, ShootBody.transform.position.y, 0.0f);
        Vector3 vTargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y, 0.0f);

        OriSize.x = 1000.0f;
        Line.GetComponent<RectTransform>().sizeDelta = new Vector2(OriSize.x, OriSize.y);
        Vector3 Dir = (vTargetPos - vShootBodyPos).normalized;
        //Vector3 revDir = ( new Vector3( vTargetPos.x , -vTargetPos.y, vTargetPos.z) - vShootBodyPos).normalized;
        Vector3 revDir = new Vector3(-Dir.x, Dir.y, Dir.z).normalized;
        Vector3 layEndPos = Dir * 1000.0f;

        int layerMask = 1 << LayerMask.NameToLayer("GAME_RAY");

        float radius = Radius;

        RaycastHit2D hit2d = Physics2D.CircleCast(vShootBodyPos, radius, Dir, Mathf.Infinity, layerMask);


        if (hit2d)
        {
            Vector2 a = revDir * (radius / 2.0f);
            HitMark.transform.position = hit2d.point + (Vector2)(revDir * (radius / 2.0f));
            LineReverse.transform.position = hit2d.point;
            LineReverse.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180.0f - _fShootAngle));

        }

    }

}
