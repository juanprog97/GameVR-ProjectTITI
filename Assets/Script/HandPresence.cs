using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristic;
    public bool showController = true;
    private InputDevice targetDevice;
    public List<GameObject> controllerPrefabs;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    public GameObject handModelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, devices);
        foreach (var item in devices)
        {

        }
        Debug.Log(devices.Count);
        if(devices.Count > 0)
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
                spawnedController = Instantiate(controllerPrefabs[0] , transform);
            }
            spawnedHandModel = Instantiate(handModelPrefab, transform);

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

        if (showController)
        {
            spawnedHandModel.SetActive(false);
            spawnedController.SetActive(true);        
        }
        else
        {
            spawnedHandModel.SetActive(true);
            spawnedController.SetActive(false);

        }


    }
}
