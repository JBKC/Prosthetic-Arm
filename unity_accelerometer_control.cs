using System;
using System.IO.Ports;
using UnityEngine;

public class GyroArduino : MonoBehaviour
{
    private SerialPort serialPort; 

    private string portName = "/dev/cu.usbmodem14201"; 
    private int baudRate = 115200;
    
    // Change thumb angle as a test (assign bone_finger_1_2)
    public Transform thumbJoint;

    private float ax, ay, az;

    // Maximum accelerometer values in the range of ±11
    private const float MAX_VALUE = 11f;
    
    // The angle range for the thumb joint (-60 to 0 degrees)
    private const float MIN_ANGLE = -60f;
    private const float MAX_ANGLE = 0f;

    void Start()
    {
        Debug.Log("Attempting to open serial port: " + portName);

        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
            Debug.Log("Serial Port Opened Successfully!");
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to open the serial port: " + ex.Message);
        }

        serialPort.ReadTimeout = 100;  // Timeout for reading from the serial port
    }

    void Update()
    {
        // Ensure serialPort is initialized and open before attempting to read
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
    
                string data = serialPort.ReadLine();            
                // Print raw data
                Debug.Log("Raw Data: " + data);
                // Split data into x,y,z
                string[] values = data.Split(',');

                if (values.Length == 3)
                {
                    // Parse the accelerometer values (ax, ay, az)
                    ax = float.Parse(values[0]);
                    ay = float.Parse(values[1]);
                    az = float.Parse(values[2]);

                    Debug.Log($"Received: ax = {ax}, ay = {ay}, az = {az}");

                    // Map accelerometer value from the range -11 to 11 to the range 0 to 60
                    float thumbRotationX = Mathf.Clamp(MapRange(ax, -MAX_VALUE, MAX_VALUE, MIN_ANGLE, MAX_ANGLE), MIN_ANGLE, MAX_ANGLE);
                    float thumbRotationY = Mathf.Clamp(MapRange(ay, -MAX_VALUE, MAX_VALUE, MIN_ANGLE, MAX_ANGLE), MIN_ANGLE, MAX_ANGLE);
                    float thumbRotationZ = Mathf.Clamp(MapRange(az, -MAX_VALUE, MAX_VALUE, MIN_ANGLE, MAX_ANGLE), MIN_ANGLE, MAX_ANGLE);

                    if (thumbJoint != null)
                    {
                        // Apply the scaled rotation
                        thumbJoint.localRotation = Quaternion.Euler(thumbRotationX, thumbRotationY, thumbRotationZ);
                    }
                }
            }
            catch (TimeoutException)
            {
            }
            catch (FormatException)
            {
                Debug.LogWarning("Malformed data received from serial port.");
            }
        }
        else
        {
            Debug.LogError("Serial port is not open or is null.");
        }
    }

    // Helper function to map a value from one range to another
    private float MapRange(float input, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return ((input - inputMin) / (inputMax - inputMin)) * (outputMax - outputMin) + outputMin;
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Serial Port Closed.");
        }
    }
}
