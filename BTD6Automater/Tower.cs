namespace BTD6Automater
{
    public class Tower
    {
        public int X { get; set; }
        public int Y { get; set; }
        public TowerType TowerType { get; set; }
        public string Name { get; set; }
        
        public Tower(TowerType type, string name, int x, int y)
        {
            TowerType = type;
            Name = name;
            X = x;
            Y = y;
        }
    }
}
