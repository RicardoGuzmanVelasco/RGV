using UnityEngine;
using UnityEngine.UI;

namespace RGV.Testing.Runtime
{
    public partial class TestApi
    {
        public class Fake
        {
            public static void ClickOn<T>() where T : MonoBehaviour
            {
                Object.FindObjectOfType<T>().GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}