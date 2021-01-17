using System.Drawing;

namespace Interactions
{
    public interface WindowInteractions
    {
        void SendKey(string key);
        void PlaceCursor(int x, int y);
        void SendClick(int x, int y);
        void Focus(string name);
        Point GetCursorLocation();
        void MinimizeCurrentWindow();
        void MaximizeCurrentWindow();
    }
}