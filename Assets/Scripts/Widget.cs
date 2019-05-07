using VRTour.Serialize;
using UnityEngine;
using UnityEngine.UI;

namespace VRTour
{
    public class Widget : MonoBehaviour
    {
        [SerializeField]
        private InputField xText;
        [SerializeField]
        private InputField yText;
        [SerializeField]
        private InputField zText;
        [SerializeField]
        private InputField xRText;
        [SerializeField]
        private InputField yRText;
        [SerializeField]
        private InputField zRText;

        private GameObject nodeObj;


        private string question;
        private Vector3 position;
        private Vector3 rotation;
        private string x;
        private string y;
        private string z;
        private string xR;
        private string yR;
        private string zR;

        //private static readonly char[] seperators = { ' ', ',', '.' };

        // Update is called once per frame
        void LateUpdate()
        {
            if (nodeObj != null && position != nodeObj.transform.localPosition)
            {
                position = nodeObj.transform.localPosition;
                rotation = nodeObj.transform.rotation.eulerAngles;
                xText.text = position.x.ToString();
                yText.text = position.y.ToString();
                zText.text = position.z.ToString();

                xRText.text = rotation.x.ToString();
                yRText.text = rotation.y.ToString();
                zRText.text = rotation.z.ToString();
            }
        }

        public void SetWidgetObj(GameObject wid)
        {
            nodeObj = wid;
            position = wid.transform.localPosition;
            rotation = wid.transform.rotation.eulerAngles;
            xText.text = position.x.ToString();
            yText.text = position.y.ToString();
            zText.text = position.z.ToString();

            xRText.text = rotation.x.ToString();
            yRText.text = rotation.y.ToString();
            zRText.text = rotation.z.ToString();
        }

        //UnityEvent code

        public void UpdateX(string x0)
        {
            x = x0;
            float.TryParse(x, out position.x);
            nodeObj.transform.localPosition = position;
        }
        public void UpdateY(string y0)
        {
            y = y0;
            float.TryParse(y, out position.y);
            nodeObj.transform.localPosition = position;
        }
        public void UpdateZ(string z0)
        {
            z = z0;
            float.TryParse(z, out position.z);
            nodeObj.transform.localPosition = position;
        }

        public void UpdateXRot(string x0)
        {
            xR = x0;
            float.TryParse(xR, out rotation.x);
            nodeObj.transform.rotation = Quaternion.Euler(rotation);
        }
        public void UpdateYRot(string y0)
        {
            yR = y0;
            float.TryParse(yR, out rotation.y);
            nodeObj.transform.rotation = Quaternion.Euler(rotation);
        }
        public void UpdatezRot(string z0)
        {
            zR = z0;
            float.TryParse(zR, out rotation.z);
            nodeObj.transform.rotation = Quaternion.Euler(rotation);
        }

        public void UpdateLabel(string q)
        {
            question = q;
        }

        public Node Finalize(int id)
        {
            return new Node
            {
                nodeId = id,
                label = question,
                position = position,
                rotation = rotation
            };
        }

    }

}

