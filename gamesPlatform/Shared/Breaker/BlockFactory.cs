namespace cmArcade.Shared.Breaker
{
    public static class BlockFactory
    {
        private static Random rng = new Random();

        internal static Block makeSpecialBlock(int row, int col, PowerUpType t, int sprite)
        {
            BlockModel hollowModel = BlockModel.blocks.Last();
            var newBlock = new Block(row, col, hollowModel, sprite);
            newBlock.setPowerup(new PowerUp(row + (hollowModel.height / 2), col + (hollowModel.width / 2), t, 0));
            newBlock.addDecal(GameDecal.breakerDecals["powerup"]);
            return newBlock;
        }

        internal static Block makeRegularBlock(int row, int col, int sprite)
        {
            return new Block(row, col, BlockModel.blocks.First(), sprite);
        }

        internal static Block makeFragileBlock(int row, int col, int sprite)
        {
            return new Block(row, col, BlockModel.blocks.First(el => el.spriteId.Contains("fragile")), sprite);
        }

        internal static Block makeStrongBlock(int row, int col, int sprite)
        {
            return new Block(row, col, BlockModel.blocks.First(el => el.spriteId.Contains("strong")), sprite);
        }

        internal static List<List<Block>> setupBlockField((int row, int col) limits, int rowCount)
        {
            var blockSet = new List<List<Block>>();
            for (int i = 0; i < rowCount; i++)
            {
                blockSet.Add(makeRandomizedRow(limits, i));
            }
            return blockSet;
        }

        internal static Block getRandomBlock(int row, int col, int spriteSelect)
        {
            switch (rng.Next(0, 10))
            {
                case < 3:
                    if (rng.Next(0, 2) > 0)
                        return makeFragileBlock(row, col, spriteSelect);
                    else
                        return makeStrongBlock(row, col, spriteSelect);
                case >= 8:
                    return makeSpecialBlock(row, col, (PowerUpType)rng.Next(0, 2), spriteSelect);
                default:
                    return makeRegularBlock(row, col, spriteSelect);
            }
        }

        internal static List<Block> makeRandomizedRow((int row, int col) limits, int heightOffset)
        {
            int blockCount = limits.col / BlockModel.widestBlockSize;
            int padding = limits.col % BlockModel.widestBlockSize / 2;

            var blockRow = new List<Block>();
            for (int j = 0; j < blockCount; j++)
            {
                int rowCoords = (limits.row / 8) + ((int)(BlockModel.highestBlockSize * 1.2) * (heightOffset + 1));
                int colCoords = (j * BlockModel.widestBlockSize) + padding;
                blockRow.Add(getRandomBlock(rowCoords, colCoords, j));
            }
            return blockRow;
        }
    }
}