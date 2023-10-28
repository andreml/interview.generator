namespace interview.generator.domain.Utils
{
    public static class ListExtension
    {
        private static readonly Random _rng = new();

        public static IList<T> Randomizar<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }

            return list;
        }
    }
}
