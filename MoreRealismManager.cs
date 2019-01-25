using System.Collections;
using System.Collections.Generic;
using MoreRealism.Windows;
using UnityEngine;

namespace MoreRealism
{
    class MoreRealismManager : MonoBehaviour
    {
        private static MoreRealismManager sInstance;
        public static MoreRealismManager Instance
        {
            get
            {
                if (sInstance == null)
                    sInstance = new MoreRealismManager();
                return sInstance;
            }
        }

        private GameObject _controllerGO;

        public GameObject controllerGO {  get { return _controllerGO; } }
        public MoreRealismController controller
        {
            get
            {
                if (_controllerGO == null)
                    return null;
                else
                    return _controllerGO.GetComponent<MoreRealismController>();
            }
        }

        private MoreRealismManager()
        {
            _controllerGO = new GameObject();
            _controllerGO.AddComponent<MoreRealismController>();
            AssetManager.Instance.registerObject(_controllerGO);
        }

        public void RemoveController()
        {
            if (_controllerGO != null) { 
                Object.Destroy(_controllerGO);
                _controllerGO = null;
            }
        }

        public void SetController(GameObject contrGO)
        {
            if (contrGO.GetComponent<MoreRealismController>().getId() != controller.getId())
            {
                this.RemoveController();
                _controllerGO = contrGO;
                controller.Load();
            }
        }
    }
}
