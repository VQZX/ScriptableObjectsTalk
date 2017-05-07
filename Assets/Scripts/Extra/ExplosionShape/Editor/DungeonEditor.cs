using UnityEditor;
using UnityEngine;
//using Honours.LowEconomy.Data;
using System;
using System.Collections.Generic;
//using Honours.Helpers;
using UnityObject = UnityEngine.Object;
#if OLD_COLD
namespace Honours.LowEconomy.Editing
{
    [CustomEditor(typeof(DungeonShape)), CanEditMultipleObjects]
    public class DungeonEditor : Editor
    {
        public delegate void ButtonDraw(ref Rect pos);

        private DungeonShape referenceShape;
        
        public struct ActionExtra
        {
            public Action<Rect> action;
            public ButtonDraw OnDraw;
            public string name;

            public ActionExtra (Action<Rect> action, string _name )
            {
                this.action = action;
                this.name = _name;
                this.OnDraw = (ref Rect r) => { };
            }
        }

        public struct DungeonEditorData
        {
            public Rect finalRect;
            public DungeonShape dungeon;
            public float helperButtonOffset;
        }

        public List<ActionExtra> buttonActions = new List<ActionExtra>();

        private Vector2 defaultTypesSize = new Vector2(60, 25);

        private DungeonEditorData data;
        //consider converting to states
        private bool editing = true;

        // Creating New Explosions
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

            DungeonShape dungeon = CreateInstance<DungeonShape>();
            AssetDatabase.CreateAsset(dungeon, path);
            AssetDatabase.SaveAssets();
        }

        // Editing Dungeons
        public override void OnInspectorGUI()
        {
            data.finalRect = new Rect(20, 50, Screen.width, 20);
            ReferenceObject( data.finalRect );  
            HelperButtons();
            DisplayToggles(ref data.dungeon, ref data.finalRect);
            data.finalRect.y -= 120;
            //LabelScript(data);

            EditorUtility.SetDirty(data.dungeon);
        }

        private void ReferenceObject(Rect position)
        {
            GameObject __pool = data.dungeon.PoolObject;
            __pool = (GameObject)EditorGUI.ObjectField(position, "Pool Prefab", __pool, typeof(GameObject), false);
            data.dungeon.PoolObject = __pool;
            data.finalRect.y += 20;
        }

        private static void LabelScript(DungeonEditorData data)
        {
            System.Type t = data.dungeon.GetType();
            string label = t.ToString();
            Rect labelPosition = data.finalRect;
            labelPosition.y += 20;
            EditorGUI.LabelField(labelPosition, label);
        }

#region Displaying toggles
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

        private static int CalculateCurrentIndex( DungeonShape shape, int x, int y)
        {
            int index = x * shape.m_height + y;
            return index;
        }

        private static void InvertColumn(int column, ref DungeonShape dungeon)
        {
            /*
            int height = dungeon.m_height;
            bool[,] data = dungeon.m_data;
            for ( int i = 0; i < height; i++ )
            {
                data[column, i] = !data[column, i];
            }
            dungeon.m_data = data;
            EditorUtility.SetDirty(dungeon);
            */
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
            /*
            int width = dungeon.m_width;
            bool[,] data = dungeon.m_data;
            for (int i = 0; i < width; i++)
            {
                data[i, row] = !data[i, row];
            }
            dungeon.m_data = data;
            EditorUtility.SetDirty(dungeon);
            */
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

        private void HelperButtons()
        {
            Rect aboveToggles = data.finalRect;
            Invert(ref aboveToggles);
            Clear(ref aboveToggles);
            Fill(ref aboveToggles);
            SaveButton(ref aboveToggles);
            data.finalRect = aboveToggles;
            float y = data.finalRect.y;
            y -= 20;
            data.finalRect.y = y;
            SpecialTypes();
        }

        private void ConfigButtons ()
        {
            buttonActions.Clear();

            ActionExtra x0 = new ActionExtra();
            x0.OnDraw = HundredBoxButton;
            buttonActions.Add(x0);

            ActionExtra x1 = new ActionExtra();
            x1.OnDraw = RandomiseBoxButton;
            buttonActions.Add(x1);

            ActionExtra x2 = new ActionExtra();
            x2.OnDraw = BoxButton;
            buttonActions.Add(x2);

            ActionExtra x3 = new ActionExtra();
            x3.OnDraw = RandomHundredButton;
            buttonActions.Add(x3);

            ActionExtra x4 = new ActionExtra();
            x4.OnDraw = ReferenceButton;
            buttonActions.Add(x4);
        }

        private void SpecialTypes ()
        {
            Rect aboveToggles = data.finalRect;
            aboveToggles.size = defaultTypesSize;
            ConfigButtons();
            for ( int i = 0; i < buttonActions.Count; i++ )
            {
                NewButtonRect(ref aboveToggles);
                buttonActions[i].OnDraw(ref aboveToggles);
            }
        }

#region Special Types functions
        private void HundredBoxButton (ref Rect pos)
        {
            bool pressed = GUI.Button(pos, "100 Box");
            if ( pressed )
            {
                Hundred();
            }
        }

        private void Hundred()
        {
            SetAll(false);
            int width = 100;
            int height = 100;
            data.dungeon.UpdateData(width, height);

            int i;

            //top row
            for (i = 0; i < width; i++ )
            {
                int index = CalculateCurrentIndex(data.dungeon, i, 0);
                data.dungeon.m_data[index] = true;
                index = CalculateCurrentIndex(data.dungeon, i, height - 1);
                data.dungeon.m_data[index] = true;
            }

            for ( i = 0; i < height; i++ )
            {
                int index = CalculateCurrentIndex(data.dungeon, 0, i);
                data.dungeon.m_data[index] = true;
                index = CalculateCurrentIndex(data.dungeon, width - 1, i);
                data.dungeon.m_data[index] = true;
            }
        }

        private void RandomiseBoxButton ( ref Rect pos )
        {
            bool pressed = GUI.Button(pos, "Randomise");
            if ( pressed )
            {
                RandomiseBox();
            }
        }

        private void RandomiseBox ()
        {
            GaussianRandom();
        }

        private void RegularRandom ()
        {
            int width = data.dungeon.m_width - 1;
            int height = data.dungeon.m_height - 1;
            for (int i = 1; i < width; i++)
            {
                for (int j = 1; j < height; j++)
                {
                    int index = CalculateCurrentIndex(data.dungeon, i, j);
                    bool result = MathUtility.RandomBool(0.1f);
                    data.dungeon.m_data[index] = result;
                }
            }
        }

        private void GaussianRandom ()
        {
            int width = data.dungeon.m_width - 1;
            int height = data.dungeon.m_height;
            int middleX = (int)(width * 0.5f);
            float mean = 0;
            float sd = MathUtility.CalculateSDForSimpleSets(width, out mean);
            string log = "Results";
            for ( int i = 1; i < width; i++ )
            {
                for ( int j = 0; j < height; j++ )
                {
                    int index = CalculateCurrentIndex(data.dungeon, i, j);
                    int deviationSection = MathUtility.CalculateDeviation(sd, mean, i);
                    float result = MathUtility.GaussianRandomness();
                    result = MathUtility.Map(-1, 1, 0, 1, result);
                    float distance = Mathf.Abs(middleX - i);
                    float position = MathUtility.Map(0, width, 0, 1, distance);
                    bool succces = Mathf.Abs( result ) <= position;
                    data.dungeon.m_data[index] = succces;
                    log = string.Format("{0}\n{1} - {2} ({3})", log, result, position, i );
                }
            }
            Debug.Log(log);
        }

        private int FindMiddle (DungeonShape shape)
        {
            int width = shape.m_width;
            return (int)(width * 0.5f);
        }

        private bool GaussianCheck (int currentIndex, DungeonShape shape )
        {
            int middleX = FindMiddle(shape);
            float result = MathUtility.GaussianRandomness();
            result = MathUtility.Map(-1, 1, 0, 1, result);
            float distance = Mathf.Abs(middleX - currentIndex);
            float position = MathUtility.Map(0, shape.m_width, 0, 1, distance);
            bool succces = Mathf.Abs(result) <= position;
            return succces;
        }

        private void BoxButton (ref Rect pos)
        {
            bool pressed = GUI.Button(pos, "Box");
            if ( pressed )
            {
                Box();
            }
        }

        private void Box ()
        {
            int width = data.dungeon.m_width;
            int height = data.dungeon.m_height;
            int i = 0;
            for (i = 0; i < width; i++)
            {
                int index = CalculateCurrentIndex(data.dungeon, i, 0);
                data.dungeon.m_data[index] = true;
                index = CalculateCurrentIndex(data.dungeon, i, height - 1);
                data.dungeon.m_data[index] = true;
            }

            for (i = 0; i < height; i++)
            {
                int index = CalculateCurrentIndex(data.dungeon, 0, i);
                data.dungeon.m_data[index] = true;
                index = CalculateCurrentIndex(data.dungeon, width - 1, i);
                data.dungeon.m_data[index] = true;
            }
        }

        private void RandomHundredButton (ref Rect pos)
        {
            bool pressed = GUI.Button(pos, "Big Random");
            if ( pressed )
            {
                RandomHundred();
            }
        }

        private void RandomHundred ()
        {
            Hundred();
            RandomiseBox();
        }

        private void ReferenceButton (ref Rect pos)
        {
            bool pressed = GUI.Button(pos, "Reference");
            if ( pressed )
            {
                ReferenceGeneration();
            }
        }

        private void ReferenceGeneration ()
        {
            if ( referenceShape != null )
            {
                int width = data.dungeon.m_width;
                int height = data.dungeon.m_height;
                float willChangeValue = 0.9f;
                DungeonShape newShape = data.dungeon.Copy();
                for ( int i = 0; i < width; i++ )
                {
                    for ( int j = 0; j < height; j++ )
                    {
                        int currentIndex = CalculateCurrentIndex(newShape, i, j);
                        bool result = GaussianCheck(currentIndex, newShape);
                        bool previousIndex = data.dungeon.m_data[currentIndex];
                        newShape.m_data[currentIndex] = MathUtility.RandomeSelect<bool>(previousIndex, result);
                    }
                }
            }
            else
            {
                Debug.LogError("Reference Shape does not exist");
            }
        }

#endregion

        private void NewButtonRect( ref Rect aboveToggles )
        {
            float xWidth = 60;
            float yWidth = 20;
            float widthOffset = 10;
            float tryX = aboveToggles.x + aboveToggles.width + widthOffset;
            float tryY = aboveToggles.y;
            if ( tryX >= Screen.width - xWidth )
            {
                tryX = xWidth;
                tryY = aboveToggles.y + yWidth;
            }
            aboveToggles = new Rect(tryX, tryY, xWidth, yWidth);
        }

        private void Invert(ref Rect aboveToggles)
        {
            NewButtonRect(ref aboveToggles);
            bool invert = GUI.Button(aboveToggles, "Invert");
            DungeonShape dungeon = data.dungeon;
            if (invert)
            {
                for (int x = 0; x < dungeon.m_width; x++)
                {
                    for (int y = 0; y < dungeon.m_height; y++)
                    {
                        int index = x * dungeon.m_width + y;
                        index = CalculateCurrentIndex(dungeon, x, y);
                        dungeon.m_data[index] = !dungeon.m_data[index];
                    }
                }
                EditorUtility.SetDirty(dungeon);
            }
            //aboveToggles.y += data.helperButtonOffset;
        }

        private void Clear(ref Rect position)
        {
            NewButtonRect(ref position);
            bool clear = GUI.Button(position, "Clear");
            if (clear)
            {
                SetAll(false);
            }
            //position.y += data.helperButtonOffset;
        }

        private void Fill(ref Rect position)
        {
            NewButtonRect(ref position);
            bool clear = GUI.Button(position, "Fill");
            if (clear)
            {
                SetAll(true);
            }
            //position.y += data.helperButtonOffset;
        }

        private void SetAll(bool state)
        {
            DungeonShape shape = data.dungeon;
            bool[] values = shape.m_data;
            int height = shape.m_height;
            int width = shape.m_width;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int index = i * width + j;
                    values[index] = state;
                }
            }
            shape.m_data = values;
            data.dungeon = shape;
            EditorUtility.SetDirty(data.dungeon);
        }

        private void SaveButton(ref Rect position)
        {
            NewButtonRect(ref position);
            bool save = GUI.Button(position, "Save");
            position.y += data.helperButtonOffset;
            if (save)
            {
                Save();
            }
        }

        private void Save()
        {
            AssetDatabase.SaveAssets();
        }

        // Preview Explosion
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

        // For Static Thumbnails
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

        private void OnEnable()
        {
            data.dungeon = target as DungeonShape;
            data.helperButtonOffset = 20;
            
        }
    }
}
#endif