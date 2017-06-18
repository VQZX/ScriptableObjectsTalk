using UnityEditor;
using UnityEngine;

namespace MGSATalk.Data.Editor
{
    [CustomEditor(typeof(GardenTemplate))]
    public class GardenTemplateEditor : UnityEditor.Editor
    {

        //private string directory = "";
        private GardenTemplate referenceShape;

        public override void OnInspectorGUI()
        {
            referenceShape = (GardenTemplate)target;
            Rect aRect = new Rect();
            DisplayToggles(ref referenceShape, ref aRect);
            EditorUtility.SetDirty(referenceShape);
        }

        
        private static int xOffset = 50;
        private static int yOffset = 50;
        private void DisplayToggles(ref GardenTemplate dungeon, ref Rect finalRect)
        {
            if (Event.current.type == EventType.Layout)
            {
                return;
            }
            Rect position = new Rect(xOffset,
                finalRect.y + yOffset + 50, // Accounts for Header
                Screen.width,
                Screen.height - 32);

            Rect usedRect = InspectGardenTemplate(position, ref dungeon);
            position.y += usedRect.height;
            finalRect = position;
        }

        private int DisplayWidth(Rect position, GardenTemplate dungeon)
        {
            // Size
            int newWidth = EditorGUI.IntField(new Rect(position.x,
                    position.y,
                    position.width * 0.5f,
                    EditorGUIUtility.singleLineHeight),
                    "Width",
                    dungeon.Width);
            return newWidth;
        }

        private int DisplayHeight(Rect position, GardenTemplate dungeon)
        {
            int newHeight = EditorGUI.IntField(new Rect(position.x + position.width * 0.5f,
                    position.y,
                    position.width * 0.5f,
                    EditorGUIUtility.singleLineHeight),
                    "Height",
                    dungeon.Height);
            return newHeight;
        }

        public  Rect InspectGardenTemplate(Rect position, ref GardenTemplate dungeon)
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
            if ((newWidth != dungeon.Width) || (newHeight != dungeon.Height))
            {
                bool[] newData1 = new bool[newWidth * newHeight];
                int xMin = Mathf.Min(newWidth, dungeon.Width);
                int yMin = Mathf.Min(newHeight, dungeon.Height);
                for (int x = 0; x < xMin; x++)
                {
                    for (int y = 0; y < yMin; y++)
                    {
                        int index = CalculateCurrentIndex(dungeon, x, y);
                        if (index >= dungeon.Count || index >= newData1.Length)
                        {
                            break;
                        }
                        newData1[index] = dungeon[index];
                    }
                }

                dungeon.Width = newWidth;
                dungeon.Height = newHeight;
                dungeon.SetNewData(newData1);
            }

            // Setup Block Size and Font
            float xWidth = 20;
            float margin = xWidth * 0.8f;
            GUIStyle myFontStyle = new GUIStyle(EditorStyles.textField);
            myFontStyle.fontSize = Mathf.FloorToInt(xWidth * 0.7f);
            //float half = xWidth * 0.5f;
            //Rect invertXButtons = new Rect(position.x, sizes.y + yOffset * 0.5f, half, half);
            //Rect invertYButtons = new Rect(position.x - xOffset * 0.5f, position.y, half, half);

            // Edit Blocks
            for (int x = 0; x < dungeon.Width; x++)
            {
                for (int y = 0; y < dungeon.Height; y++)
                {
                    Rect layout = new Rect(position.x + margin * x, position.y + margin * y, xWidth, xWidth);
                    CreateToggles(x, y, layout, ref dungeon);
                }
            }

            EditorUtility.SetDirty(dungeon);

            return new Rect(saveOrig.x, saveOrig.y, saveOrig.width, EditorGUIUtility.singleLineHeight + (dungeon.Height * xWidth));
        }

        private void CreateToggles(int x, int y, Rect layout, ref GardenTemplate dungeon)
        {
            int index = CalculateCurrentIndex(dungeon, x, y);
            dungeon[index] = EditorGUI.Toggle(layout, dungeon[index]);
        }

        private int CalculateCurrentIndex(GardenTemplate shape, int x, int y)
        {
            return GardenTemplate.GetCorrectIndex(shape, x, y);
        }
    }
}