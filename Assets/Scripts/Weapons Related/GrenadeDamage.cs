using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeDamage : MonoBehaviour
{
    [SerializeField] private float grenadeDmg;
    // Start is called before the first frame update

    public void CalculateAreaDamage(Collider collider, Vector3 lastPos)
    {
        RaycastHit hitInfoMid;
        RaycastHit hitInfoUp;
        RaycastHit hitInfoDown;
        RaycastHit hitInfoLeft;
        RaycastHit hitInfoRight;

        Vector3 posMid = collider.gameObject.transform.position;
        Vector3 posUp = new Vector3(posMid.x, posMid.y + 4.75f, posMid.z);
        Vector3 posDown = new Vector3(posMid.x, posMid.y - 4.75f, posMid.z);
        Vector3 posLeft = new Vector3(posMid.x - 2f, posMid.y, posMid.z);
        Vector3 posRight = new Vector3(posMid.x + 2f, posMid.y, posMid.z);

        float distanceMid = Vector3.Distance(lastPos, posMid);
        float distanceUpper = Vector3.Distance(lastPos, posUp);
        float distanceDown = Vector3.Distance(lastPos, posDown);
        float distanceLeft = Vector3.Distance(lastPos, posLeft);
        float distanceRight = Vector3.Distance(lastPos, posRight);

        bool mid = Physics.Linecast(lastPos, posMid, out hitInfoMid);
        bool up = Physics.Linecast(lastPos, posUp, out hitInfoUp);
        bool down = Physics.Linecast(lastPos, posDown, out hitInfoDown);
        bool right = Physics.Linecast(lastPos, posRight, out hitInfoRight);
        bool left = Physics.Linecast(lastPos, posLeft, out hitInfoLeft);

        RaycastHit[] raycasts = new RaycastHit[] { hitInfoMid, hitInfoUp, hitInfoDown, hitInfoRight, hitInfoLeft };
        float[] distances = new float[] { distanceMid, distanceUpper, distanceDown, distanceRight, distanceLeft };

        //Debug.Log(listrc);
        //Debug.Log(listds);
        //Debug.Log(collider.gameObject.name);
        for (int i = 0; i < raycasts.Length; i++)
        {

            if (distances[i] > 15 || raycasts[i].collider.gameObject.layer != collider.gameObject.layer)
            {
                //Debug.Log($"Miss, distance: {distances[i]}, objname: {raycasts[i].collider.gameObject.name}");

            }
            else
            {
                collider.GetComponent<Entity>().Health -= grenadeDmg / 5f;
                //Debug.Log($"Hit, distance: {distances[i]}, objname: {raycasts[i].collider.gameObject.name}");
            }
        }


        //Vector3[] hitInfos = new Vector3[] { hitInfoMid.point, hitInfoUp.point, hitInfoDown.point, hitInfoLeft.point, hitInfoRight.point };
        //Vector3[] currentPos = new Vector3[] { posMid, posUp, posDown, posLeft, posRight };
        //objectsPos.Add(currentPos);
        //Debug.Log("----------------------");
    }
}
