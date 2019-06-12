﻿using UnityEngine;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public static class DataDecoder
{
    static IFormatter formatter = new BinaryFormatter();

    public delegate void PositionDecoded(Vector3 position);
    public static event PositionDecoded OnPositionDecoded;

    public delegate void RotationDecoded(Vector3 rotation);
    public static event RotationDecoded OnRotationDecoded;

    public delegate void BallPositionDecoded(Vector3 ballPosition);
    public static event BallPositionDecoded OnBallPositionDecoded;

    public static int DecodeString(NetworkStream stream)
    {
        Debug.Log("Decoding");
        byte[] bytes = new byte[1024 * 2];
        int length;
        if ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
        {
            try
            {
                var data = new byte[length];
                Array.Copy(bytes, 0, data, 0, length);
                string serverMessage = Encoding.UTF8.GetString(data);
                List<string> stringList = serverMessage.Split(',').ToList();

                Vector3 position = new Vector3();
                position.x = (float)Convert.ToDouble(stringList[0]);
                position.y = (float)Convert.ToDouble(stringList[1]);
                position.z = (float)Convert.ToDouble(stringList[2]);

                Vector3 rotation = new Vector3();
                rotation.x = (float)Convert.ToDouble(stringList[3]);
                rotation.y = (float)Convert.ToDouble(stringList[4]);
                rotation.z = (float)Convert.ToDouble(stringList[5]);

                Vector3 ballPosition = new Vector3();
                ballPosition.x = (float)Convert.ToDouble(stringList[6]);
                ballPosition.y = (float)Convert.ToDouble(stringList[7]);
                ballPosition.z = (float)Convert.ToDouble(stringList[8]);

                Debug.Log("Decoded");

                if (OnPositionDecoded != null)
                {
                    OnPositionDecoded(position);
                }
                if (OnRotationDecoded != null)
                {
                    OnRotationDecoded(rotation);
                }
                if (OnBallPositionDecoded != null)
                {
                    OnBallPositionDecoded(ballPosition);
                }
            }

            catch
            {
                Debug.Log("Decoding did not work");
            }
        }
        return length;
    }
}
