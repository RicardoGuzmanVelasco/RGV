using TMPro;
using UnityEngine;

namespace RGV.TestApi.Runtime
{
    public partial class TestApi
    {
        public class Find
        {
            public static string TextOnLabelOf<T>() where T : MonoBehaviour
            {
                return Object.FindObjectOfType<T>().GetComponent<TMP_Text>().text;
            }
        }
    }
}