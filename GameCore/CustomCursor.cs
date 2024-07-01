using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D _customCursor;
    [SerializeField] private Texture2D _approvedCursor;
    [SerializeField] private Texture2D _notApprovedCursor;

    private void Start()
    {
        Cursor.SetCursor(_customCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeCursorToAproved()
    {
        Cursor.SetCursor(_approvedCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeCursorToNotAproved()
    {
        Cursor.SetCursor(_notApprovedCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeCursorToStandart()
    {
        Cursor.SetCursor(_customCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
