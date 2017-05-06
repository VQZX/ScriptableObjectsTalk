using UnityEngine;
using System.Collections;
using Flusk.Helpers;
using UnityEngine.SceneManagement;

namespace Flusk.Controllers
{
    public class LoadLevel : PersistentSingleton<LoadLevel>
    {
        public enum AsyncLoadExecution
        {
            ImmediateSwitch = 0,
            WaitToSwitch = 1
        }

        protected AsyncLoadExecution loadExecution;

        protected float loadProgress;
        protected AsyncOperation asyncLoad;
        public AsyncOperation AsyncLoadData
        {
            get
            {
                return asyncLoad;
            }
        }
        protected bool isAsyncLoad = false;

        public delegate void LoadLevelEvent(AsyncOperation async);
        public static event LoadLevelEvent OnLoading;
        public static event LoadLevelEvent OnComplete;

        public void SwitchToActivatedScene ()
        {
            if (asyncLoad != null)
            {
                if ( asyncLoad.isDone )
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
        }

        protected virtual void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        protected virtual void LoadAsync(string scene, bool immediateSwitch = true)
        {
            loadExecution = (immediateSwitch) ? AsyncLoadExecution.ImmediateSwitch : AsyncLoadExecution.WaitToSwitch;
            StartCoroutine(LoadCo(scene));
        }

        protected IEnumerator LoadCo ( string scene)
        {
            asyncLoad = SceneManager.LoadSceneAsync(scene);
            asyncLoad.allowSceneActivation = false;
            isAsyncLoad = true;
            yield return asyncLoad;
            Debug.Log("Load Complete");
        }

        protected virtual void Update()
        {
            if (isAsyncLoad)
            {
                Debug.Log(asyncLoad.progress);
                if (!asyncLoad.isDone)
                {
                    if (OnLoading != null)
                    {
                        OnLoading(asyncLoad);
                    }
                }
                else
                {
                    if (OnComplete != null)
                    {
                        OnComplete(asyncLoad);
                    }
                    isAsyncLoad = false;
                }
            }
        }
    }
}
