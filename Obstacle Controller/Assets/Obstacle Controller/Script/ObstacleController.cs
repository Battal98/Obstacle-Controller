
/*/ Author Battal Yigit PATLAR *_* /*/

using UnityEngine;
using NaughtyAttributes;

public class ObstacleController : MonoBehaviour
{

    public enum ObstacleMovement
    {
        None, Rotate, Horizontal, Vertical
    }

    public ObstacleMovement ObsMov = ObstacleMovement.Horizontal; // For Movement

    
    [System.Serializable]
    public class HorizontalOptions
    {
        public float Speed = 2f;//Move Side    
        public bool Status = false; //Status
        public Vector2 minMaxPushValueX;
    }
    [System.Serializable]
    public class RotateOptions
    {
        public float spinSpeed = 700;
        public float direction = 1;
        [Tooltip("Give 1 whichever angle you want it to rotate.")]
        public Vector3 angle;
    }
    [System.Serializable]
    public class VerticalOptions
    {
        public float Speed = 2f;//Move Side    
        public bool Status = false; //Status
        public Vector2 minMaxPushValueX;
    }
    [BoxGroup("Vertical Options")]
    public VerticalOptions VerticalOps;

    [BoxGroup("Horizontal Options")]
    public HorizontalOptions HorizontalOps;

    [BoxGroup("Rotate Options")]
    public RotateOptions RotateOps;
    [Header("The moving object must be in the zeroth child of the prefabs. ")]
    [BoxGroup("Movement Obj")]
    public GameObject MovementObj;

    private void Start()
    {
        HorizontalOps.Status = true;
        CheckObstacle();
    }
    void Update()
    {
        XAxisMovement();
        YAxisMovement();
        RotateMovement();
    }


    #region Movements
    private void XAxisMovement()
    {
        if (ObsMov == ObstacleMovement.Horizontal)
        {
            if (MovementObj.transform.position.x > HorizontalOps.minMaxPushValueX.x)
            {
                HorizontalOps.Status = false;
            }
            if (MovementObj.transform.position.x < HorizontalOps.minMaxPushValueX.y)
            {
                HorizontalOps.Status = true;
            }
            if (HorizontalOps.Status == true)
            {
                MovementObj.transform.Translate(HorizontalOps.Speed * Time.deltaTime, 0, 0);
            }
            if (HorizontalOps.Status == false)
            {
                MovementObj.transform.Translate(-HorizontalOps.Speed * Time.deltaTime, 0, 0);
            }
        }
    }

    private void YAxisMovement()
    {
        if (ObsMov == ObstacleMovement.Vertical)
        {
            if (MovementObj.transform.position.y > VerticalOps.minMaxPushValueX.x)
            {
                VerticalOps.Status = false;
            }
            if (MovementObj.transform.position.y < VerticalOps.minMaxPushValueX.y)
            {
                VerticalOps.Status = true;
            }
            if (VerticalOps.Status == true)
            {
                MovementObj.transform.Translate(0, VerticalOps.Speed * Time.deltaTime, 0);
            }
            if (VerticalOps.Status == false)
            {
                MovementObj.transform.Translate(0, -VerticalOps.Speed * Time.deltaTime, 0);
            }
        }
    }

    private void RotateMovement()
    {
        if (ObsMov == ObstacleMovement.Rotate)
        {
            MovementObj.transform.Rotate(new Vector3(RotateOps.angle.x, RotateOps.angle.y, RotateOps.angle.z) * Time.deltaTime * RotateOps.spinSpeed * RotateOps.direction);
        }
    }

    #endregion

    private void CheckObstacle()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            if (this.gameObject.transform.GetChild(i).gameObject.GetComponent<ObstacleTypeController>().ObsSpec == this.gameObject.GetComponent<ObstacleTypeController>().ObsSpec)
            {
                this.gameObject.transform.GetChild(i).gameObject.SetActive(true);
                MovementObj = this.gameObject.transform.GetChild(i).GetChild(0).gameObject;
            }
            else
                this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
