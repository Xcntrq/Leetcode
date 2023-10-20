namespace Problem0042
{
    internal class Program
    {
        static void Main()
        {
            var S = new Solution();
            int[] ints;
            int answer;

            ints = new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            answer = 6;
            if (S.Trap(ints) != answer)
            {
                Console.WriteLine(S.Trap(ints));
                Console.WriteLine("Case 1 failed");
            }

            ints = new int[] { 4, 2, 0, 3, 2, 5 };
            answer = 9;
            if (S.Trap(ints) != answer)
            {
                Console.WriteLine(S.Trap(ints));
                Console.WriteLine("Case 2 failed");
            }

            ints = new int[] { 4, 2, 3 };
            answer = 1;
            if (S.Trap(ints) != answer)
            {
                Console.WriteLine(S.Trap(ints));
                Console.WriteLine("Case 3 failed");
            }

            ints = new int[] { 4, 3, 0, 3, 0, 2, 1 };
            answer = 5;
            if (S.Trap(ints) != answer)
            {
                Console.WriteLine(S.Trap(ints));
                Console.WriteLine("Case 4 failed");
            }

            ints = new int[] { 0, 2, 0 };
            answer = 0;
            if (S.Trap(ints) != answer)
            {
                Console.WriteLine(S.Trap(ints));
                Console.WriteLine("Case 5 failed");
            }

            ints = new int[] { 0 };
            answer = 0;
            if (S.Trap(ints) != answer)
            {
                Console.WriteLine(S.Trap(ints));
                Console.WriteLine("Case 5 failed");
            }

            // Console.ReadLine();
        }
    }
}