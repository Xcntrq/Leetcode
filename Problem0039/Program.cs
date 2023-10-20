using System.Collections.Generic;

namespace Problem0039
{
    internal class Program
    {
        static void Main()
        {
            var s = new Solution();
            s.CombinationSum(new int[] { 2, 3, 6, 7 }, 7);
        }
    }

    public class Entry
    {
        public int Target;
        public List<int> Combination;

        public Entry(int target, List<int> combination)
        {
            Target = target;
            Combination = new List<int>(combination);
        }

        public void Insert(int candidate)
        {
            for (int i = 0; i < Combination.Count; i++)
            {
                if (Combination[i] >= candidate)
                {
                    Combination.Insert(i, candidate);
                    return;
                }
            }
            Combination.Add(candidate);
        }
    }

    public class Solution
    {
        private int _nonZeros;
        private List<Entry> _result;
        private int[] _candidates;

        public Solution()
        {
            _result = new List<Entry>();
            _candidates = Array.Empty<int>();
        }

        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            _candidates = candidates;
            _nonZeros = 1;
            _result = new()
            {
                new Entry(target, new List<int>())
            };

            while (_nonZeros > 0)
            {
                for (int i = _result.Count - 1; i >= 0; i--)
                {
                    if (_result[i].Target != 0)
                    {
                        List<Entry> processedEntry = ProcessEntry(_result[i]);
                        _result.RemoveAt(i);
                        _nonZeros--;
                        AddToResult(processedEntry);
                    }
                }
            }

            IList<IList<int>> a = new List<IList<int>>();
            foreach (var thing in _result)
            {
                a.Add(thing.Combination);
            }

            return a;
        }

        private List<Entry> ProcessEntry(Entry entry)
        {
            List<Entry> result = new();
            for (int i = 0; i < _candidates.Length; i++)
            {
                int compliment = entry.Target - _candidates[i];
                if ((compliment == 0) || (compliment > 1))
                {
                    Entry newEntry = new(compliment, entry.Combination);
                    newEntry.Insert(_candidates[i]);
                    result.Add(newEntry);
                }
            }

            return result;
        }

        private void AddToResult(List<Entry> entries)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                bool found = false;

                for (int j = 0; j < _result.Count; j++)
                {
                    if (entries[i].Target != _result[j].Target)
                    {
                        continue;
                    }

                    if (AreListsTheSame(entries[i].Combination, _result[j].Combination))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    _result.Add(entries[i]);
                    if (entries[i].Target > 0)
                    {
                        _nonZeros++;
                    }
                }
            }
        }

        private static bool AreListsTheSame(List<int> l1, List<int> l2)
        {
            if (l1.Count != l2.Count)
            {
                return false;
            }

            for (int i = 0; i < l1.Count; i++)
            {
                if (l1[i] != l2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}