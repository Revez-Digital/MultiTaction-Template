using UnityEngine;
using TouchScript;
using TouchScript.Pointers;

[System.Serializable]
public class ObjectMarker
{
    public int MarkerID;
    public GameObject gameObj;
}
namespace MultiTaction{

    public class MarkerController : MonoBehaviour {
            
        public GameObject marker;
        public ObjectMarker[] objectMarker;

        private int[] ListID = new int[] { 0, 1, 2};
        // private int[] ListID = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        //private int[] ListID = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 210, 220, 230, 240, 250, 260, 270, 280 };

        //   public bool flagObject = false;

        private static MarkerController _instance;

        public static MarkerController instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MarkerController>();
                }
                return _instance;
            }
        }

        void Start () {
            for(int i= objectMarker.Length - 1; i >= 0; i--)
            {
                marker.GetComponent<MarkerObjectClass>().MarkerGO.GetComponent<MarkerNode>().Init(i);
                objectMarker[i].MarkerID = ListID[i];
                objectMarker[i].gameObj = Instantiate(marker, new Vector3(0, 0, 0), Quaternion.identity);
                objectMarker[i].gameObj.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }
        }
        void Update () {
        }

        private void OnEnable()
        {
            if (TouchManager.Instance != null)
            {
                TouchManager.Instance.PointersAdded += pointersAddedHandler;
                TouchManager.Instance.PointersUpdated += pointerUpdatedHander;
                TouchManager.Instance.PointersRemoved += pointerRemoveHandler;
            }
        }

        private void OnDisable()
        {
            if (TouchManager.Instance != null)
            {
                TouchManager.Instance.PointersAdded -= pointersAddedHandler;
                TouchManager.Instance.PointersUpdated -= pointerUpdatedHander;
                TouchManager.Instance.PointersRemoved -= pointerRemoveHandler;
            }
        }

        //---------------------------------------------
        void pointersAddedHandler(object sender, PointerEventArgs e)
        {
            lock (this)
            {
                foreach (var pointer in e.Pointers)
                {
                    if (pointer.Type == Pointer.PointerType.Object)
                    {
                        ObjectPointer op = (ObjectPointer)pointer;
                        Vector2 position = op.Position;
                        getMarker(op.ObjectId);
                    }
                }
            }

        }

        void pointerUpdatedHander(object sender, PointerEventArgs e)
        {
            lock (this)
            {
                foreach (var pointer in e.Pointers)
                {
                    if (pointer.Type == Pointer.PointerType.Object)
                    {
                        ObjectPointer op = (ObjectPointer)pointer;
                        Vector2 position = op.Position;
                        moveMarker(op.ObjectId, position, op.Angle);
                    }

                }
            }
        }

        private void pointerRemoveHandler(object sender, PointerEventArgs e)
        {
            lock (this)
            {
                foreach (var pointer in e.Pointers)
                {
                    if (pointer.Type == Pointer.PointerType.Object)
                    {
                        ObjectPointer op = (ObjectPointer)pointer;
                        Vector2 position = op.Position;
                        hideMarker(op.ObjectId);
                    }
                }
            }
        }

        //---------------------------------------------

        public void getMarker(int id)
        {
            foreach (ObjectMarker o in objectMarker)
            {
                if (o.MarkerID == id)
                {
                //    flagObject = true;
                    o.gameObj.transform.SetAsLastSibling();
                    o.gameObj.GetComponent<MarkerObjectClass>().OnShow(true);
                }
            }

        }

        public void moveMarker(int id, Vector2 pos, float angle)
        {
            foreach (ObjectMarker o in objectMarker)
            {
                if (o.MarkerID == id)
                {
                    o.gameObj.transform.position = new Vector3(pos.x, pos.y, 1);
                    o.gameObj.GetComponent<MarkerObjectClass>().onRotateAngle(angle);
                }
            }

        }

        public void hideMarker(int id)
        {
            foreach (ObjectMarker o in objectMarker)
            {
                if (o.MarkerID == id)
                {
                    o.gameObj.GetComponent<MarkerObjectClass>().OnShow(false);
                }
            }
        }

    }

}