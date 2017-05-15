#if OLD_CODE

using UnityEngine;
using System.Collections;
using System;
using Flusk.Utility;

namespace Honours.LowEconomy.Data
{
    /*
    The definition for the different shapes 
    */

    [System.Serializable]
    public class DungeonShape : ScriptableObject
    {
        public int m_width;
        public int m_height;
        public bool[] m_data;

        //pre-optimisation
        [SerializeField]
        private GameObject poolObject;
        public GameObject PoolObject
        {
            get
            {
                return poolObject;
            }

#if UNITY_EDITOR
            set
            {
                if ( !Application.isPlaying )
                {
                    poolObject = value;
                }
            }
#endif
        }

        public DungeonShape(int width, int height, bool [] data )
        {
            m_width = width;
            m_height = height;
            m_data = data;
        }

        public DungeonShape()
        {
            m_width = 20;
            m_height = 20;
            m_data = new bool[m_width * m_height];

            for (int i = 0; i < m_width * m_height; i++)
            {
                m_data[i] = false;
            }
        }

        public void UpdateData( int width, int height )
        {
            m_width = width;
            m_height = height;
            m_data = new bool[m_width * m_height];
        }

        public void Generate(Transform root, GameObject prefab)
        {
            Action<int, int> generate = (int x, int y) =>
            {
                bool pointState = SelectValue(x, y);
                if ( pointState )
                {
                    Vector3 current = new Vector3(x, y);
                    Vector3 relativePosition = CalculatePosition(prefab, current, null);
                    GameObject piece = CreatePiece(root, prefab);
                    piece.transform.localPosition = relativePosition;

                    //WallController wall = piece.GetComponent<WallController>();
                    ConfigurePiece(wall, x, y);
                }
            };
            RunThroughLoop(generate);
        }

        public void Generate(Vector3 origin, GameObject prefab)
        {
            //silly repeating of code
            Action<int, int> generate = (int x, int y) =>
            {
                bool pointState = SelectValue(x, y);
                if (pointState)
                {
                    Vector3 current = new Vector3(x, y);
                    Vector3 relativePosition = CalculatePosition(prefab, current, origin);
                    GameObject piece = CreatePiece(prefab);
                    piece.transform.localPosition = relativePosition; WallController wall = piece.GetComponent<WallController>();
                    ConfigurePiece(wall, x, y);
                }
            };
            RunThroughLoop(generate);
        }

        private void ConfigurePiece (WallController wall, int x, int y )
        {
            if ( wall != null )
            {
                SpritePositions position = (SpritePositions)0;
                //left edge
                if ( x == 0 )
                {
                    position = position | SpritePositions.Left;
                }
                else if ( !SelectValue(x-1, y) )
                {
                    position = position | SpritePositions.Left;
                }
                //top
                if ( y == 0 )
                {
                    position = position | SpritePositions.Top;
                }
                else if ( !SelectValue(x, y - 1) )
                {
                    position = position | SpritePositions.Top;
                }
                //bottom
                if ( y == m_height - 1 )
                {
                    position = position | SpritePositions.Bottom;
                }
                else if ( !SelectValue(x, y +1) )
                {
                    position = position | SpritePositions.Bottom;
                }
                //right
                if ( x == m_width - 1)
                {
                    position = position | SpritePositions.Right;
                }
                else if ( !SelectValue(x+1, y) )
                {
                    position = position | SpritePositions.Right;
                }

                wall.SetBorders(position);

            }
            else
            {
                Debug.LogError("Piece was null");
            }
        }

        public DungeonShape Copy()
        {
            bool[] newData = m_data;
            int width = m_width;
            int height = m_height;
            DungeonShape newShape = new DungeonShape(width, height, newData);
            return newShape;
        }

        public void CreateBridge (int yPosition, GameObject prefab, GameObject root)
        {
            if ( yPosition >= 0 && yPosition < m_height )
            {
                int firstPoint = (int)(m_width * 0.4f);
                int lastPoint = m_width - firstPoint;
                for ( int i = firstPoint; i <= lastPoint; i++ )
                {
                    bool noWallThereAlready = !SelectValue(i, yPosition);
                    if ( noWallThereAlready )
                    {
                        Vector3 pos = CalculatePosition(prefab, root.transform, i, yPosition);
                        GameObject clone = Instantiate<GameObject>(prefab);
                        clone.name = "[CUNT]" + i.ToString();
                        clone.transform.position = pos;
                    }
                }
            }
        }

        private Vector3 CalculatePosition (GameObject piece, Vector3 current, Vector3? origin )
        {
            Bounds bounds = piece.GetBoundsSomehow();
            float _x = bounds.size.x;
            float _y = bounds.size.y;
            Vector3 size = new Vector3(_x, _y);
            size = Vector3.zero;
            float mag = current.magnitude;
            Vector3 relativePosition = new Vector3(current.x * _x, current.y * _y * -1);
            relativePosition += origin ?? Vector3.zero;
            return relativePosition;
        }

        private Vector3 CalculatePosition ( GameObject piece, Transform root, int x, int y)
        {
            Bounds bounds = piece.GetBoundsSomehow();
            float width = bounds.size.x;
            float height = bounds.size.y;
            Vector3 origin = root.position;
            Vector3 unit = new Vector3(width * x, -height * y);
            Vector3 position = origin + unit;
            return position;
        }

        private GameObject CreatePiece(Transform root, GameObject prefab)
        {
            GameObject wallPiece = CreatePiece(prefab);
            wallPiece.transform.parent = root;
            return wallPiece;
        }

        private bool SelectValue( int x, int y )
        {
            int index = x * m_height + y;
            bool state = m_data[index];
            return state;
        }

        private GameObject CreatePiece( GameObject prefab )
        {
            GameObject wallPiece = Instantiate<GameObject>(prefab);
            return wallPiece;
        }

        private void RunThroughLoop ( Action<int,int> loopAction )
        {
            int width = m_width;
            int height = m_height;
            for ( int i = 0; i < width; ++i )
            {
                for ( int j = 0; j < height; ++j )
                {
                    loopAction(i, j);
                }
            }
        }

        private void RunThroughLoopReverse(Action<int, int> loopAction)
        {
            int width = m_width - 1;
            int height = m_height - 1;
            for (int i = width; i >= 0; i-- )
            {
                for( int j = height; j >= 0; j-- )
                {
                    loopAction(i, j);
                }
            }
        }

        private void RunAsSeen ( Action<int, int> loopAction )
        {
            int width = m_width - 1;
            int height = m_height - 1;
            for (int i = width; i >= 0; i--)
            {
                for (int j = 0; j < height; j++)
                {
                    loopAction(i, j);
                }
            }
        }
    }
}
#endif