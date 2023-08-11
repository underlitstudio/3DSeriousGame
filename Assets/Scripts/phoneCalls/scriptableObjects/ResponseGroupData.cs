using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ResponseGroupData", menuName = "Custom/Response Group Data")]
public class ResponseGroupData : ScriptableObject
{
    [System.Serializable]
    public class ResponseGroup
    {
        public List<string> responses = new List<string>();
        public string additionalText = ""; 
    }

    public List<ResponseGroup> responseGroups = new List<ResponseGroup>();
}
