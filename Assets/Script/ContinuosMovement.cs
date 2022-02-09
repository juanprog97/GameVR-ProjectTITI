using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class ContinuosMovement : MonoBehaviour
{
    public float speed = 1;
    public XRNode inputSource;
    public Vector2 inputAxis;
    public float gravity = -9.81f;
    private float fallingSpeed;
    private XRRig rig;
    public float additionalHeight = 0.2f;
    public LayerMask groundLayer;
    private CharacterController character;
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);


    }
    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headyaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headyaw *  new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction* Time.fixedDeltaTime*speed);
        //gravity
        bool isGround = CheckIfGround();
        if (isGround)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);

        
                   



    }

    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    bool CheckIfGround()
    {
        //tell if Ground
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHit;
    }
}
