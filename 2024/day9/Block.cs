namespace day9
{
    public class Block
    {
        public List<char> Content = [];
        public int TryWrite(Block b)
        {
            var firstFree = Content.IndexOf('.');
            if (firstFree == -1 || Content.Count - firstFree < b.Content.Count)
                return -1;
            for (int i = firstFree; i < Content.Count; i++)
            {
                if (b.Content.IndexOf('.') != 0)
                {
                    Content[i] = b.Content[0];
                    b.Content.RemoveAt(0);
                    b.Content.Add('.');
                }
                else
                {
                    return 1;
                }
            }
            return 1;
        }
        public string StrContent()
        {
            if (Content.Count == 0)
                throw new Exception("Empty block!");
            return string.Join("", Content);
        }
    }
}