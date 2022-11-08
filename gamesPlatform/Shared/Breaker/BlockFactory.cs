namespace cmArcade.Shared.Breaker
{
    public static class BlockFactory
    {
        public static Block makeSpecialBlock(int row, int col, PowerUpType t, int sprite)
        {
            BlockModel hollowModel = BlockModel.blocks.Last();
            var newBlock = new Block(row, col, hollowModel, sprite);
            newBlock.setPowerup(new PowerUp(row + (hollowModel.height / 2), col + (hollowModel.width / 2), t, sprite));
            newBlock.addDecal(GameDecal.breakerDecals["powerup"]);
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

        public static List<Block> setupBlockField((int row, int col) limits, int rowCount)
        {
            var rng = new Random();
            int widestBlockSize = BlockModel.blocks.Max(x => x.width);
            int highestBlockSize = BlockModel.blocks.Max(x => x.height);

            int blockCount = limits.col / widestBlockSize;
            int padding = limits.col % widestBlockSize;

            widestBlockSize += padding / 2;
            var blocks = new List<Block>();
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < blockCount; j++)
                {
                    int rowCoords = (limits.row / 8) + ((int)(highestBlockSize * 1.2) * (i + 1));
                    int colCoords = j * widestBlockSize;
                    if (i == rowCount / 2)
                    {
                        blocks.Add(makeStrongBlock(rowCoords, colCoords, j % 4));
                    }
                    else
                    {
                        switch (rng.Next(0, 10))
                        {
                            case <= 2:
                                blocks.Add(makeFragileBlock(rowCoords, colCoords, i % 4));
                                break;
                            case >= 9:
                                blocks.Add(makeSpecialBlock(rowCoords, colCoords, PowerUpType.ball, i % 4));
                                break;
                            default:
                                blocks.Add(makeRegularBlock(rowCoords, colCoords, i));
                                break;
                        }
                    }
                }
            }
            return blocks;
        }
    }
}