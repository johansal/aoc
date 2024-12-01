namespace day1
{
    public class OrderedList<T> where T : IComparable<T>
    {
        private List<T> _list = [];
        public void Add(T item)
        {
            bool added = false;
            for(int i = 0; i < _list.Count; i++)
            {
                if(item.CompareTo(_list[i]) < 0)
                {
                    _list.Insert(i, item);
                    added = true;
                    break;
                }
            }
            if(!added)
                _list.Add(item);
        }
        public T GetItemAt(int index)
        {
            if(index >= 0 && index < _list.Count)
            {
                return _list[index];
            }
            else {
                throw new Exception("Index out of range!");
            }
        }
        public int Count()
        {
            return _list.Count;
        }
        public int Count(T item)
        {
            int count = 0;
            for(int i = 0; i < _list.Count; i++)
            {
                var diff = item.CompareTo(_list[i]);
                if(diff == 0)
                {
                    count++;
                }
                else if(diff < 0)
                    break;
            }
            return count;
        }
    }
}