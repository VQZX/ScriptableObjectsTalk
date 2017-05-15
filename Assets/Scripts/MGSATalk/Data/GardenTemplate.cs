using System;
using UnityEngine;

namespace MGSATalk.Data
{
    //HERE BE DRAGONS!
    /// <summary>
    /// Garden Template for creating a map
    /// </summary>
    ///
    /// CreateAssetMenu
    /// Attribute to make it easier to create this specific type of object
    /// filename: The default file name for the asset
    /// menuName: The order within the create menu
    /// order: positioning within the create menu
    [CreateAssetMenu(fileName = "Garden.asset", menuName = "MGSATalk/Garden/Garden", order = 1)]
    public class GardenTemplate : ScriptableObject, IGardenData
    {

        /// <summary>
        /// Easy access to GardenTemplate
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }

        /// <summary>
        /// Maps are square for the moment,
        /// so a a regular width and height
        /// </summary>
        public int Width { get; set; }
        public int Height { get; set; }
        public int Count { get { return Width * Height; } }

        //the data that holds the positions of the garden pieces
        private bool[] data;

        public GardenTemplate(int width, int height, bool[] data)
        {

        }

        public GardenTemplate()
        {

        }

        /// <summary>
        /// A function to help with the two-dimensional array
        /// </summary>
        /// <param name="template"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int GetCorrectIndex(GardenTemplate template, int x, int y)
        {
            return x * template.Width + y;
        }

        public void SetNewData(bool[] _data)
        {
            data = _data;
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
                    piece.transform.localPosition = relativePosition;
                }
            };
            RunThroughLoop(generate);
        }

        public void UpdateData(int width, int height)
        {
            Width = width;
            Height = height;
            data = new bool[Width * Height];
        }

        public GardenTemplate Copy()
        {
            bool[] newData = data;
            int width = Width;
            int height = Height;
            GardenTemplate newShape = new GardenTemplate(width, height, newData);
            return newShape;
        }

        private Vector3 CalculatePosition(GameObject piece, Transform root, int x, int y)
        {
            Bounds bounds = piece.GetComponent<SpriteRenderer>().bounds;
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

        private GameObject CreatePiece(GameObject prefab)
        {
            GameObject wallPiece = Instantiate<GameObject>(prefab);
            return wallPiece;
        }

        private void RunThroughLoop(Action<int, int> loopAction)
        {
            int width = Width;
            int height = Height;
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    loopAction(i, j);
                }
            }
        }

        private bool SelectValue(int x, int y)
        {
            int index = x * Height + y;
            bool state = data[index];
            return state;
        }

        private Vector3 CalculatePosition(GameObject piece, Vector3 current, Vector3? origin)
        {
            Bounds bounds = piece.GetComponent<SpriteRenderer>().bounds;
            float _x = bounds.size.x;
            float _y = bounds.size.y;
            Vector3 size = new Vector3(_x, _y);
            size = Vector3.zero;
            float mag = current.magnitude;
            Vector3 relativePosition = new Vector3(current.x * _x, current.y * _y * -1);
            relativePosition += origin ?? Vector3.zero;
            return relativePosition;
        }

    }
}
