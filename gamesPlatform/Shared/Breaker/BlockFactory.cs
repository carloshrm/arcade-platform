namespace cmArcade.Shared.Breaker
{
    public static class BlockFactory
    {
        public static Block makeSpecialBlock(int row, int col, PowerupType t, int sprite)
        {
            BlockModel hollowModel = BlockModel.blocks.Last();
            var newBlock = new Block(row, col, hollowModel, sprite);
            newBlock.setPowerup(new PowerUp(row, col, t, sprite));
            return newBlock;
        }

        public static Block makeRegularBlock(int row, int col, int sprite)
        {
            return new Block(row, col, BlockModel.blocks.First(), sprite);
        }

        public static Block makeFragileBlock(int row, int col, int sprite)
        {
            return new Block(row, col, BlockModel.blocks.First(el => el.spriteId.Contains("fragile")), sprite);
        }

        public static Block makeStrongBlock(int row, int col, int sprite)
        {
            return new Block(row, col, BlockModel.blocks.First(el => el.spriteId.Contains("strong")), sprite);
        }
    }
}