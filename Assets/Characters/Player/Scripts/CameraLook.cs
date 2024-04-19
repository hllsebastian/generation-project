
using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraLook : MonoBehaviour
{
    [SerializeField]
    private float lookspeed=1;
    private CinemachineFreeLook cinema;
    private PlayerCam playerinput;
    // Start is called before the first frame update
    

    private  void Awake() {
        playerinput = new PlayerCam();
        cinema= GetComponent<CinemachineFreeLook>();
    }
    private void OnEnable() {
        playerinput.Enable();
    }
    private void OnDisable() {
        playerinput.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 delta =playerinput.Playercinem.look.ReadValue<Vector2>();
  
        cinema.m_XAxis.Value+=delta.x *200*lookspeed*Time.deltaTime;
        cinema.m_YAxis.Value+=delta.y *lookspeed*Time.deltaTime;
    }
}
