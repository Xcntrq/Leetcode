namespace Problem0042
{
    public class Solution
    {
        private int highestHeight;
        private int currentHeight;

        public int Trap(int[] height)
        {
            highestHeight = 0;

            int LSP = -1;
            int RSP = height.Length;

            int highestLSP = 0;
            int highestRSP = 0;

            int airVolume = 0;
            int blocksVolume = 0;

            while (LSP < RSP)
            {
                // moving left to right
                while ((LSP < RSP) && (highestLSP <= highestRSP) && (LSP < height.Length - 1))
                {
                    LSP++;
                    blocksVolume += height[LSP];
                    currentHeight = height[LSP];

                    if (currentHeight > highestLSP)
                    {
                        airVolume += LSP * (currentHeight - highestLSP);
                        highestLSP = currentHeight;
                    }

                    CheckHH();
                }

                // moving right to left
                while ((LSP < RSP) && (highestRSP <= highestLSP))
                {
                    RSP--;
                    blocksVolume += height[RSP];
                    currentHeight = height[RSP];

                    if (currentHeight > highestRSP)
                    {
                        airVolume += (height.Length - RSP - 1) * (currentHeight - highestRSP);
                        highestRSP = currentHeight;
                    }

                    CheckHH();
                }
            }

            return (height.Length * highestHeight) - airVolume - blocksVolume + currentHeight;
        }

        private void CheckHH()
        {
            if (currentHeight > highestHeight)
            {
                highestHeight = currentHeight;
            }
        }
    }
}
