﻿using Newtonsoft.Json;
using System;
using System.Text;
using UnityEngine;

class JSONSerializator : ISerializator
{
    private static JSONSerializator instance;

    private JSONSerializator()
    {

    }

    public byte[] Serialize(object obj)
    {
        return Encoding.UTF8.GetBytes($"{obj.GetType().ToString()},{JsonConvert.SerializeObject(obj)}");
    }

    public bool TryDeserialize(byte[] message, out object obj)
    {
        try
        {
            var json = Encoding.UTF8.GetString(message);
            var jsonsplit = json.Split(new char[] { ',' }, 2);
            var type = Type.GetType(jsonsplit[0]);
            obj = JsonConvert.DeserializeObject(jsonsplit[1], type);
            return true;
        }
        catch
        {
            Debug.Log("ERROR");
            obj = null;
            return false;
        }

    }

    public static JSONSerializator GetSerializator()
    {
        if (instance == null)
            instance = new JSONSerializator();
        return instance;
    }
}