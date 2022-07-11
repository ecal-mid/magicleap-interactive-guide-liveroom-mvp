using UnityEngine;
using System;
// Load the data
using System.Text.RegularExpressions;


// TODO - include the same in the editor things
[Serializable]
public class MediaInfo
{
    public string name;
    public string filename;
    public string filetype;
    public string type;
    public string url;
    public string target;
    public string relativeTransform;
    public string language;
    public string srt;
    public int width;
    public int height;
    public bool resume;
    public float opacity;
    public float distance;

    public float Ratio
    {
        get
        {
            return (float)width / (float)height;
        }
    }

    public TransformInfo RelativeTransform
    {
        get
        {
            // todo - add some checks in here.
            if (relativeTransform == "undefined" || relativeTransform == null )
            {
                TransformInfo ti = new TransformInfo();
                ti.scale = Vector3.one;
                return ti;
            }

            //string test = "{\n    \"position\": {\n        \"x\": 0.6268335580825806,\n        \"y\": 0.0017918795347213746,\n        \"z\": 2.0917623043060304\n    },\n    \"rotation\": {\n        \"x\": 0.02159503474831581,\n        \"y\": -0.006150097586214542,\n        \"z\": 0.007756460458040237,\n        \"w\": 0.9997178316116333\n    },\n    \"scale\": {\n        \"x\": 1.0,\n        \"y\": 1.0,\n        \"z\": 1.0\n    }\n}";
            //Debug.Log(test.Length);
            //Debug.Log(relativeTransform.Length);
            // Was double escaped
            string unsecaped = Regex.Unescape(relativeTransform);
            unsecaped = unsecaped.Remove(unsecaped.Length - 1, 1).Remove(0, 1); // Remove guillmets
            return JsonUtility.FromJson<TransformInfo>(unsecaped);
            //return JsonUtility.FromJson<TransformInfo>(relativeTransform.ToString());
        }
    }

    public string recordId;

    public string addressableName
    {
        get
        {
            return System.IO.Path.GetFileNameWithoutExtension(filename);
        }
    }
}
