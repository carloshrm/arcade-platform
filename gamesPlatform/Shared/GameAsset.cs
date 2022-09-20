﻿
namespace cmArcade.Shared
{
    public abstract class GameAsset
    {
        public abstract string spriteId { get; set; }
        public abstract int width { get; init; }
        public abstract int height { get; init; }
    }
}
