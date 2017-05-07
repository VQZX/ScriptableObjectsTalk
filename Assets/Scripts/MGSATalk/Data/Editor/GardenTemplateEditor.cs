using UnityEditor;
using UnityEngine;

namespace MGSATalk.Data.Editor
{
    [CustomEditor(typeof(GardenTemplateEditor)), CanEditMultipleObjects]
    public class GardenTemplateEditor : UnityEditor.Editor
    {

        private string directory = "";
        private GardenTemplate referenceShape;


        [MenuItem("Flusk/Dungeon")]
        static void CreatePuzzle()
        {
            string directory = "Assets/Low Economy - yes/Data/Shapes/";

            string path = EditorUtility.SaveFilePanel("Create Dungeon Shape", directory, "dungeon.asset", "asset");
            if (path == "")
            {
                return;
            }

            path = FileUtil.GetProjectRelativePath(path);

            GardenTemplate dungeon = ScriptableObject.CreateInstance<GardenTemplate>();
            AssetDatabase.CreateAsset(dungeon, path);
            AssetDatabase.SaveAssets();
        }

        public void OnInspectorGUI()
        {
            /*
            data.finalRect = new Rect(20, 50, Screen.width, 20);
            ReferenceObject(data.finalRect);
            HelperButtons();
            DisplayToggles(ref data.dungeon, ref data.finalRect);
            data.finalRect.y -= 120;
            //LabelScript(data);
            
            EditorUtility.SetDirty(data.dungeon);
            */
        }
        /*
        public override bool HasPreviewGUI()
        {
            return true;
        }

        public override void OnPreviewGUI(Rect tarRect, GUIStyle background)
        {
            DungeonShape exShape = data.dungeon;
            // Get Size
            float blockSize = Mathf.Min(tarRect.width / exShape.m_width, tarRect.height / exShape.m_height);
            float offX = (tarRect.width - blockSize * exShape.m_width) / 2 + tarRect.x;
            float offY = (tarRect.height - blockSize * exShape.m_height) / 2 + tarRect.y;

            // Get Max
            //int maxExplode = Mathf.Max(exShape.m_data);
            //float maxExDiv = 1.0f / (float)maxExplode;

            // Draw Blocks
            try
            {
                for (int x = 0; x < exShape.m_width; ++x)
                {
                    for (int y = 0; y < exShape.m_height; ++y)
                    {
                        //Debug.LogFormat("({0}, {1})", x, y);
                        int index = CalculateCurrentIndex(exShape, x, y);
                        float alpha = 0;
                        try
                        {
                            alpha = exShape.m_data[index] ? 1 : 0;
                        }
                        catch (System.Exception ex)
                        {
                            if (index >= exShape.m_data.Length)
                            {
                                int calc = exShape.m_height * exShape.m_width;
                                string output = string.Format("{0} is too big for the array with a length of {1}. The width is {2}, the height is {3}. making the percieved value to be {4}", index, exShape.m_data.Length, exShape.m_width, exShape.m_height, calc);
                                string output2 = string.Format("the current loop values are x at {0}, and y at {1}", x, y);
                                Debug.Log(output);
                                Debug.Log(output2);
                            }
                            throw ex;
                        }

                        Rect layout = new Rect(offX + x * blockSize + 1, offY + y * blockSize + 1, blockSize - 2, blockSize - 2);
                        EditorGUI.DrawRect(layout, new Color(0, 0, 0, alpha));
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        */
        private void ReferenceObject(Rect position)
        {
            /*
            GameObject __pool = data.dungeon.PoolObject;
            __pool = (GameObject)EditorGUI.ObjectField(position, "Pool Prefab", __pool, typeof(GameObject), false);
            data.dungeon.PoolObject = __pool;
            data.finalRect.y += 20;
            */
        }
        /*TOGGLES
        #region TOGGLES

        private static int xOffset = 50;
        private static int yOffset = 50;
        private static void DisplayToggles(ref DungeonShape dungeon, ref Rect finalRect)
        {
            if (Event.current.type == EventType.Layout)
            {
                return;
            }
            Rect position = new Rect(xOffset,
                finalRect.y + yOffset + 50, // Accounts for Header
                Screen.width,
                Screen.height - 32);

            Rect usedRect = InspectDungeonShape(position, ref dungeon);
            position.y += usedRect.height;
            finalRect = position;
        }

        private static int DisplayWidth(Rect position, DungeonShape dungeon)
        {
            // Size
            int newWidth = EditorGUI.IntField(new Rect(position.x,
                    position.y,
                    position.width * 0.5f,
                    EditorGUIUtility.singleLineHeight),
                "Width",
                dungeon.m_width);
            return newWidth;
        }

        private static int DisplayHeight(Rect position, DungeonShape dungeon)
        {
            int newHeight = EditorGUI.IntField(new Rect(position.x + position.width * 0.5f,
                    position.y,
                    position.width * 0.5f,
                    EditorGUIUtility.singleLineHeight),
                "Height",
                dungeon.m_height);
            return newHeight;
        }

        // Editing Single Explosion
        public static Rect InspectDungeonShape(Rect position, ref DungeonShape dungeon)
        {
            GUI.changed = false;
            Rect saveOrig = position;
            Rect sizes = position;
            sizes.x -= xOffset;
            sizes.y -= yOffset;
            int newWidth = DisplayWidth(sizes, dungeon);
            int newHeight = DisplayHeight(sizes, dungeon);

            position.y += EditorGUIUtility.singleLineHeight;

            // Resize
            if ((newWidth != dungeon.m_width) || (newHeight != dungeon.m_height))
            {
                bool[] newData1 = new bool[newWidth * newHeight];
                int xMin = Mathf.Min(newWidth, dungeon.m_width);
                int yMin = Mathf.Min(newHeight, dungeon.m_height);
                for (int x = 0; x < xMin; x++)
                {
                    for (int y = 0; y < yMin; y++)
                    {
                        int index = CalculateCurrentIndex(dungeon, x, y);
                        newData1[index] = dungeon.m_data[index];
                    }
                }

                dungeon.m_width = newWidth;
                dungeon.m_height = newHeight;
                dungeon.m_data = newData1;
            }

            // Setup Block Size and Font
            float xWidth = 20;
            float margin = xWidth * 0.8f;
            GUIStyle myFontStyle = new GUIStyle(EditorStyles.textField);
            myFontStyle.fontSize = Mathf.FloorToInt(xWidth * 0.7f);
            float half = xWidth * 0.5f;
            Rect invertXButtons = new Rect(position.x, sizes.y + yOffset * 0.5f, half, half);
            Rect invertYButtons = new Rect(position.x - xOffset * 0.5f, position.y, half, half);

            // Edit Blocks
            for (int x = 0; x < dungeon.m_width; x++)
            {
                bool currentColumInvert = GUI.Button(invertXButtons, "*");
                if (currentColumInvert)
                {
                    InvertColumnSingle(x, ref dungeon);
                }
                invertXButtons.x += margin;

                for (int y = 0; y < dungeon.m_height; y++)
                {
                    Rect layout = new Rect(position.x + margin * x, position.y + margin * y, xWidth, xWidth);
                    CreateToggles(x, y, layout, ref dungeon);
                }
            }
            for (int z = 0; z < dungeon.m_height; z++)
            {
                bool currentRowInvert = GUI.Button(invertYButtons, "*");
                if (currentRowInvert)
                {
                    InvertRowSingle(z, ref dungeon);
                }
                invertYButtons.y += margin;
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(dungeon);
            }

            return new Rect(saveOrig.x, saveOrig.y, saveOrig.width, EditorGUIUtility.singleLineHeight + (dungeon.m_height * xWidth));
        }

        private static void CreateToggles(int x, int y, Rect layout, ref DungeonShape dungeon)
        {
            int index = CalculateCurrentIndex(dungeon, x, y);
            dungeon.m_data[index] = EditorGUI.Toggle(layout, dungeon.m_data[index]);
        }

        private static int CalculateCurrentIndex(DungeonShape shape, int x, int y)
        {
            int index = x * shape.m_height + y;
            return index;
        }

        private static void InvertColumn(int column, ref DungeonShape dungeon)
        {
            
        }

        private static void InvertColumnSingle(int column, ref DungeonShape shape)
        {
            int height = shape.m_height;
            for (int i = 0; i < height; i++)
            {
                int index = column * height + i;
                shape.m_data[index] = !shape.m_data[index];
            }
            EditorUtility.SetDirty(shape);
        }

        private static void InvertRow(int row, DungeonShape dungeon)
        {
            
        }

        private static void InvertRowSingle(int row, ref DungeonShape dungeon)
        {
            int width = dungeon.m_width;

            for (int i = 0; i < width; i++)
            {
                int index = i * width + row;
                dungeon.m_data[index] = !dungeon.m_data[index];
            }
            EditorUtility.SetDirty(dungeon);
        }

        #endregion
        */
        /*
        public override Texture2D RenderStaticPreview(string assetPath, UnityObject[] subAssets, int TexWidth, int TexHeight)
        {
            // Debug.Log("New Static Preview"); - Use this to see how often it gets called

            DungeonShape exShape = target as DungeonShape;
            Texture2D staticPreview = new Texture2D(TexWidth, TexHeight);

            // Get Size
            int blockSize = Mathf.FloorToInt(Mathf.Min(TexWidth / exShape.m_width, TexHeight / exShape.m_height));
            int offX = (TexWidth - blockSize * exShape.m_width) / 2;
            int offY = (TexHeight - blockSize * exShape.m_height) / 2;

            // Blank Slate
            Color blankCol = new Color(0, 0, 0, 0);
            Color[] colBlock = new Color[TexWidth * TexHeight];
            for (int i = 0; i < colBlock.Length; ++i)
            {
                colBlock[i] = blankCol;
            }
            staticPreview.SetPixels(0, 0, TexWidth, TexHeight, colBlock);

            // Draw Blocks
            for (int x = 0; x < exShape.m_width; ++x)
            {
                for (int y = 0; y < exShape.m_height; ++y)
                {
                    int subX = offX + x * blockSize;
                    int subY = TexHeight - (offY + y * blockSize) - blockSize;
                    //////////////////////////////////////////////
                    int index = x * exShape.m_width + y;
                    float alpha = exShape.m_data[index] ? 1 : 0;
                    ///////////////////////////////////////////
                    Color blockColour = new Color(0, 0, 0, alpha);
                    for (int px = 0; px < blockSize; ++px)
                    {
                        for (int py = 0; py < blockSize; ++py)
                        {
                            staticPreview.SetPixel(subX + px, subY + py, blockColour);
                        }
                    }
                }
            }

            staticPreview.Apply();
            return staticPreview;
        }
        */

    }
}
