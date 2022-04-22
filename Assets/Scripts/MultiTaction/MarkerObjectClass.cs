using UnityEngine;
using System;

namespace MultiTaction{
    [System.Serializable]
    public class MarkerObjectClass : MonoBehaviour {
        private Boolean onMarker;
        public GameObject MarkerGO;

        public enum RotationAxis { Forward, Back, Up, Down, Left, Right };
        public RotationAxis RotateAround = RotationAxis.Back;

        void Awake()
        {
            MarkerGO.SetActive(true);
            this.enabled = true;
        }

        void Start () {
            OnShow(false);
        }

        void Update () {

        }

        public void OnShow(Boolean value)
        {
            MarkerGO.SetActive(value);
        }

        public float RotationMultiplier = 1;

        public void onRotateAngle(float angle)
        {
        //   float m_Angle;
            float m_AngleDegrees;

        //   m_Angle = angle * RotationMultiplier;
            m_AngleDegrees = angle / (float)Math.PI * 180.0f * RotationMultiplier;

            Quaternion rotation = Quaternion.identity;

            switch (this.RotateAround)
            {
                case RotationAxis.Forward:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.forward);
                    break;
                case RotationAxis.Back:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.back);
                    break;
                case RotationAxis.Up:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.up);
                    break;
                case RotationAxis.Down:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.down);
                    break;
                case RotationAxis.Left:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.left);
                    break;
                case RotationAxis.Right:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.right);
                    break;
            }
            this.transform.localRotation = rotation;
        }
    }

}