using UnityEngine;
using System.Collections;

namespace Flusk.Helpers
{
	public class PersistentSingleton<T> : MonoBehaviour
	{
		protected static PersistentSingleton<T> instance;
		public static PersistentSingleton<T> Instance
		{
			get
			{
                return (instance as PersistentSingleton<T> );
			}
		}

		private void Awake ()
		{
			if ( instance == null )
			{
				instance = this;
			}
			else if ( instance != this )
			{
                Debug.LogFormat("Destroy {0}", gameObject);
                Destroy( gameObject );
			}
			DontDestroyOnLoad( gameObject );
            System.Type current = typeof(T);
            string format = string.Format("{0} Persistenct Singleton has been assigned", current );
            Debug.Log(format);
        }
	}
}
