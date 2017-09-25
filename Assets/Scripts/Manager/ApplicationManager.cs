//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-08 16:24
//================================

using UnityEngine;

namespace Easy.FrameUnity.Manager
{
    public class ApplicationManager : Singleton<ApplicationManager>
    {
        public GameObject Managers;

        private void Awake()
        {
            base.GetInstance();
            this.ApplicationEnter();
        }

        private void Start()
        {
            ScenesManager.Inst.EnterScene(ScenesName.C_SceneLogin);
        }

        private void ApplicationEnter()
        {
            DontDestroyOnLoad(Managers);			
        }

        private void ApplicationPause()
        {
        }

        private void ApplicationQuit()
        {
        }
    }
}
