
/*/ Author Battal Yigit PATLAR *_* /*/

using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;

public class ObstacleTypeController : MonoBehaviour
{
    public enum ObstacleSpecies
    {
        None, Wall, Barrier, Saw, Smasher, Punch
    }
    [Tooltip("Use if a special action is to be taken when the player hits the obstacle.")]
    public ObstacleSpecies ObsSpec = ObstacleSpecies.None;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Hits The Player");
            switch (ObsSpec)
            {
                case ObstacleSpecies.None:
                    break;
                case ObstacleSpecies.Wall:

                    #region Wall Jobs
                    PushBack(other.gameObject.transform.parent.gameObject, -2f, 0.3f);
                    #endregion

                    break;
                case ObstacleSpecies.Barrier:
                    break;
                case ObstacleSpecies.Saw:

                    #region Saw Jobs

                    PushBack(other.gameObject.transform.parent.gameObject, -1f, 0.3f);
                    //and fail
                    #endregion

                    break;
                case ObstacleSpecies.Smasher:
                    break;
                case ObstacleSpecies.Punch:
                    break;
                default:
                    break;
            }
        }
    }

    private void PushBack(GameObject _triggerObj, float _pushBackValue, float _duration)
    {
        _triggerObj.transform.DOMoveZ(_triggerObj.transform.position.z + _pushBackValue, _duration);
    }
}
