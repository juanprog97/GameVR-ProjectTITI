using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristic;
    private bool showController = false;
    private InputDevice targetDevice;
    public List<GameObject> controllerPrefabs;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    public GameObject handModelPrefab;
    private Animator animatorHand;
    // Start is called before the first frame update
    void Start()
    {
        tryInitialize();


    }

    void tryInitialize()
    {

        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, devices);
        foreach (var item in devices)
        {

        }
        Debug.Log(devices.Count);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("Did not found Model corresponding");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            animatorHand = spawnedHandModel.GetComponent<Animator>();

        }

    }

    void UpdateAnimatorHand()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)){
            animatorHand.SetFloat("Trigger", triggerValue);
        }
        else
        {
            animatorHand.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            animatorHand.SetFloat("Grip", gripValue);
        }
        else
        {
            animatorHand.SetFloat("Grip", 0);
        }


    }

    // Update is called once per frame
    void Update()
    {

        /* if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)           That Code is for see state of event of control
             Debug.Log("Pressing Button Primary Button");


         if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
             Debug.Log("Trigger Pressed Button" + triggerValue);


         if(targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue !=  Vector2.zero)
             Debug.Log("Primary TouchPad" + primary2DAxisValue);*/

        if (!targetDevice.isValid)
        {
            tryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateAnimatorHand();

            }
        }

       


    }
}
