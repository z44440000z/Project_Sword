using UnityEngine;
using System.Collections;

public class FloatingTextController : MonoBehaviour {
    private static FloatingText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupText)
            popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(popupText);
        // Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.2f, .2f), location.position.y + Random.Range(-.2f, .2f)));
        // Vector2 screenPosition = new Vector2(location.localPosition.x + Random.Range(-.2f, .2f), location.localPosition.y + Random.Range(-.2f, .2f));
        instance.transform.SetParent(location, false);
        instance.transform.localPosition = Vector2.zero;
        instance.SetText(text);
    }
}
