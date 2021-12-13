using UnityEngine;

public class BaseVehicleInput : MonoBehaviour
{
    #region Fields

    protected float throttle = 0f;

    public int MaxFlapIncrements = 2;
    protected float brake = 0f;
    protected float ThrottleSpeed = 0.06f;
    private float _stickyThrottle;
    public KeyCode BrakeKey = KeyCode.Space;

    [SerializeField] private KeyCode _cameraKey = KeyCode.X;
    protected bool _CameraSwitch = false;

    #endregion

    #region Properties

    public float Throttle
    {
        get { return throttle; }
    }

    public float Brake
    {
        get { return brake; }
    }

    public float StickyThrottle
    {
        get { return _stickyThrottle; }
    }

    public bool CameraSwitch
    {
        get { return _CameraSwitch; }
    }

    #endregion

    #region BuiltIn Methods

    public virtual void Update()
    {
        HandleInput();
    }

    #endregion

    #region Custom Methods

    protected virtual void HandleInput()
    {
        //Main controls
        throttle = Input.GetAxis("Vertical");
        ThrottleControl();

        //Brakes
        brake = Input.GetKey(BrakeKey) ? 1f : 0f;


        _CameraSwitch = Input.GetKeyDown(_cameraKey);
    }

    protected virtual void ThrottleControl()
    {
        _stickyThrottle = _stickyThrottle + (Throttle * ThrottleSpeed
                                                      * Time.deltaTime);

        _stickyThrottle = Mathf.Clamp01(_stickyThrottle);
    }

    #endregion
}