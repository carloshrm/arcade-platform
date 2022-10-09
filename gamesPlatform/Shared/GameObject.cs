namespace cmArcade.Shared
{
    public abstract class GameObject
    {
        public abstract int row { get; set; }
        public abstract int col { get; set; }
        public abstract int healthPoints { get; set; }
        public abstract GraphicAsset model { get; set; }
        public virtual List<GraphicAsset>? decals { get; set; }
        public abstract int spriteSelect { get; set; }
        public abstract bool updatePosition((int row, int col) limits);

    }
}
