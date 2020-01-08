using System.Drawing;

namespace BTD6Automater
{
    public interface WindowInteractions
    {
        void SendKey(string key);
        void SendClick(int x, int y);
        void Focus(string name);
        Point GetCursorLocation();
    }
}