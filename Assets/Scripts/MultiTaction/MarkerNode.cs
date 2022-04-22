using UnityEngine;

namespace MultiTaction{
    public class MarkerNode : MonoBehaviour {
        private bool valid = true;
        private float speed = 0.02f;
        private Vector3 scaleChange;

        public int markerType;
        public bool iDetected = false;

        private void Start()
        {
            scaleChange = new Vector3(speed, speed, speed);
            transform.localScale = new Vector3(0, 0, 0);
        }
        public void Init(int markerid)
        {
            markerType = markerid;
        }
        public void UpdateMarker(int markerid)
        {
            markerType = markerid;
        }
        private void OnEnable()
        {
            transform.localScale = new Vector3(0, 0, 0);
            iDetected = true;
        }
        private void OnDisable()
        {
            iDetected = false;
        }

        private void OnApplicationQuit() // important
        {        
            valid = false;
        }
        void Update(){
            if(iDetected){
                if(transform.localScale.x < 1)
                    transform.localScale += scaleChange;
            }
            else{
                if(transform.localScale.x > 0)
                    transform.localScale -= scaleChange;
            }
        }
    }

}