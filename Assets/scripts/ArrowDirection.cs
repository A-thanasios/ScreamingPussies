using UnityEngine;
using UnityEngine.UI;

public class ArrowDirectionUI : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right, None }
    public RectTransform arrowImage;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (logString.Contains("GO LEFT"))
            PointArrow(Direction.Left);
        else if (logString.Contains("GO RIGHT"))
            PointArrow(Direction.Right);
        else if (logString.Contains("GO UP"))
            PointArrow(Direction.Up);
        else if (logString.Contains("GO DOWN"))
            PointArrow(Direction.Down);
    }

    public void PointArrow(Direction direction)
    {
        float angle = 0;
        switch (direction)
        {
            case Direction.Up: angle = -90; break;
            case Direction.Right: angle = 180; break;
            case Direction.Down: angle = 90; break;
            case Direction.Left: angle = 00; break;
        }

        arrowImage.rotation = Quaternion.Euler(0, 0, angle);
    }
}
