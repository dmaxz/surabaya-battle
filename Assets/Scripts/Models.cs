using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Models
{
    // Start is called before the first frame update
    #region - Player -
    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("View Settings")]
        public float viewXSensitivity;
        public float viewYSensitivity;
        
        public bool viewXInverted;
        public bool viewYInverted;

        [Header("Movement")]
        public float walkingForwardSpeed;
        public float walkingBackwardSpeed;
        public float walkingStrafeSpeed;

        [Header("Jumping")]
        public float jumpingHeight;
        public float jumpingFalloff;

        [Header("Movement Smoothing")]
        public float movementSmoothing;
    }
    #endregion
}
