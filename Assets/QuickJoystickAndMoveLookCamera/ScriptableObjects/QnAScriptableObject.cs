using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New QnA", menuName = "Question System/QnA")]
public class QnAScriptableObject : ScriptableObject
{
     [System.Serializable]
    public class QnAPair
    {
        
        public string question;
        
        
        public string response;
    }

    public List<QnAPair> qnaPairs = new List<QnAPair>();
}
