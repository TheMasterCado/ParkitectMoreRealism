namespace MoreRealism
{
    public static class IdGenerator
    {
        private static int m_NextId = 3000; // I just start a bit higher to keep space for hardcoded window ids.

        public static int GetId()
        {
            return m_NextId++;
        }
    }
}
