using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class CustomHierarchy : MonoBehaviour
{
    const float MAX_ICON_SIZE = 16;

    static CustomHierarchy()
    {
        Initialize();
    }

    private static HierarchyRulesSO hierarchyRules;

    public static void Initialize()
    {
        if (hierarchyRules == null)
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(HierarchyRulesSO), null);
            if (guids.Length > 0)
                hierarchyRules = AssetDatabase.LoadAssetAtPath<HierarchyRulesSO>(
                    AssetDatabase.GUIDToAssetPath(guids[0]));
        }

        if (hierarchyRules == null)
            return;

        EditorApplication.hierarchyWindowItemOnGUI -= HierarchyWindowItemOnGUI;
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    public static void HierarchyWindowItemOnGUI(int instanceId, Rect selectionRect)
    {
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
        if (gameObject == null)
            return;

        foreach (var rules in hierarchyRules.CustomHierarchyLines_)
        {
            switch (rules.hierarchyTypos)
            {
                case HierarchyRulesSO.HierarchyTypos.StartWith:
                    if (gameObject.name.StartsWith(rules.typo))
                    {
                        DrawCustoms(gameObject, selectionRect, rules);
                    }
                    break;
                case HierarchyRulesSO.HierarchyTypos.Contains:
                    if (gameObject.name.Contains(rules.typo))
                    {
                        DrawCustoms(gameObject, selectionRect, rules);
                    }
                    break;
                case HierarchyRulesSO.HierarchyTypos.Tag:
                    switch (gameObject.tag)
                    {
                        case "Tilemap":
                            DrawCustoms(gameObject, selectionRect, rules);
                            return;
                        default:
                            break;
                    }
                    break;
                case HierarchyRulesSO.HierarchyTypos.Layer:
                    switch (LayerMask.LayerToName(gameObject.layer))
                    {
                        case "Player":
                            DrawCustoms(gameObject, selectionRect, rules);
                            return;
                        default:
                            break;
                    }
                    break;
                case HierarchyRulesSO.HierarchyTypos.Component:
                    switch (rules.typo)
                    {
                        case "BoxCollider2D":
                            if (gameObject.TryGetComponent(out Collider2D collider2D))
                            {
                                DrawCustoms(gameObject, selectionRect, rules);
                            }
                            return;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private static void DrawCustoms(GameObject _obj, Rect _selectionRect, HierarchyRulesSO.CustomHierarchyLines rules)
    {
        // Define colors and name
        var selected = Selection.Contains(_obj);
        var bgColor = selected ? rules.bgColor : rules.bgColor;
        var fontColor = _obj.activeSelf ? rules.fontColor : rules.fontColor - new Color(0, 0, 0, 0.8f);
        var name = rules.upperCase ? _obj.name.ToUpper() : _obj.name;

        // Define style and content
        GUIStyle guiStyle = new GUIStyle()
        {
            normal = new GUIStyleState() { textColor = fontColor },
            fontStyle = rules.fontStyle,
            alignment = rules.alignment
        };

        GUIContent guiContent = new GUIContent(rules.icon && rules.alignment != TextAnchor.MiddleCenter ? "      " + name : name);

        if (bgColor.a <= 1f)
        {
            float grey = 56f / 255f;
            EditorGUI.DrawRect(_selectionRect,
                selected ? new Color(44f / 255f, 93f / 255f, 135f / 255f) : new Color(grey, grey, grey));
        }

        // Draw bgColor rect
        EditorGUI.DrawRect(_selectionRect, bgColor);
        // Draw label
        EditorGUI.LabelField(_selectionRect, guiContent, guiStyle);

        if (rules.icon)
        {
            DrawIcons(_obj, _selectionRect, rules.icon);
        }
    }

    static void DrawIcons(GameObject _obj, Rect _selectionRect, Texture _icon)
    {
        _icon ??= Resources.FindObjectsOfTypeAll<Texture>()[0];

        #region keep that handle icon

        /*Texture[] textures = Resources.FindObjectsOfTypeAll<Texture>();

        var icon = GameManager.instance.GetComponent<HierarchyRules>().Dzq;*/

        //var icon = textures[Random.Range(0, textures.Length + 1)];
        //var icon = AssetDatabase.LoadAssetAtPath("Assets/Art/Pokemons/Back/1.png", typeof(Texture2D)) as Texture2D;

        //GUI.DrawTexture(_selectionRect, icon);

        //GUI.Label(_selectionRect, dzd);

        /*// get the starting x position based on the direction
        float x = selectionRect.x;
        if (hierarchyIcon.direction == HierarchyIcon.Direction.LeftToRight)
            x += MAX_ICON_SIZE;
        else
            x += (selectionRect.width - width) - hierarchyIcon.position * MAX_ICON_SIZE;*/
        #endregion

        float width = Mathf.Min(_icon.width, MAX_ICON_SIZE);
        float height = Mathf.Min(_icon.height, MAX_ICON_SIZE);

        float x = _selectionRect.x;

        // draw the icon
        Rect rect = new Rect(x, _selectionRect.y, width, height);
        GUI.DrawTexture(rect, _icon);

        EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

        if (GUI.Button(rect, new GUIContent(string.Empty, ""), EditorStyles.label))
        {
            Selection.activeObject = hierarchyRules;
            EditorUtility.FocusProjectWindow();
            EditorGUIUtility.PingObject(hierarchyRules);
        }

        #region keep that pop up

        /*// and draw a button for change the icon and display the tooltip
        if (GUI.Button(rect, new GUIContent(string.Empty, ""), EditorStyles.label))
            PopupWindow.Show(rect, new IconWindow());*/

        /*HierarchyIcon[] hierarchyIcons = gameObject.GetComponents<HierarchyIcon>();
        foreach (HierarchyIcon hierarchyIcon in hierarchyIcons)
        {
            Texture2D icon = hierarchyIcon.icon ? hierarchyIcon.icon : TextureHelper.NO_ICON;
            float width = Mathf.Min(icon.width, MAX_ICON_SIZE);
            float height = Mathf.Min(icon.height, MAX_ICON_SIZE);

            // get the starting x position based on the direction
            float x = selectionRect.x;
            if (hierarchyIcon.direction == HierarchyIcon.Direction.LeftToRight)
                x += hierarchyIcon.position * MAX_ICON_SIZE;
            else
                x += (selectionRect.width - width) - hierarchyIcon.position * MAX_ICON_SIZE;

            // draw the icon
            Rect rect = new Rect(x, selectionRect.y, width, height);
            GUI.DrawTexture(rect, icon);

            // set link cursor
            EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

            // and draw a button for change the icon and display the tooltip
            if (GUI.Button(rect, new GUIContent(string.Empty, hierarchyIcon.tooltip), EditorStyles.label))
                PopupWindow.Show(rect, new PickIconWindow(hierarchyIcon));
        }*/
        #endregion
    }
}

public class IconWindow : PopupWindowContent
{
    readonly SerializedProperty m_iconProperty;
    Vector2 m_scrollPosition;
    int m_myUndoGroup;

    static GUIContent[] s_IconContents;
    static readonly GUIStyle BUTTON_STYLE;
    static string s_searchWord = "";
    static bool s_loadAllIcons;

    const int BUTTON_STYLE_PADDING = 2;
    const float BUTTON_SIZE = 16 + BUTTON_STYLE_PADDING * 2;
    const float COLUMNS = 12;
    const int HEADER_HEIGHT = 22;
    const int BODY_HEIGHT = 200;
    const int BOTTOM_HEIGHT = 26;

    static IconWindow()
    {
        BUTTON_STYLE = new GUIStyle
        {
            fixedWidth = BUTTON_SIZE,
            fixedHeight = BUTTON_SIZE,
            alignment = TextAnchor.MiddleCenter,
            //onNormal = { background = TextureHelper.BUTTON_ON },
            padding = new RectOffset(
                BUTTON_STYLE_PADDING,
                BUTTON_STYLE_PADDING,
                BUTTON_STYLE_PADDING,
                BUTTON_STYLE_PADDING
            )
        };
    }

    public override void OnGUI(Rect rect)
    {
        throw new System.NotImplementedException();
    }
}