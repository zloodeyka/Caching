namespace Caching
{
	public class LRUCache<TKey, TValue> : Cache<TKey, TValue>
	{

		public LRUCache(int capacity)
		{
			_capacity = capacity;
		}
		protected override void AddToCache(TKey key, TValue value)
		{
			if (_cache.Count >= _capacity)
			{
				var last = _valuesQueue.Last;
				_cache.Remove(last.Value);
				_storage.Remove(last.Value);
				_valuesQueue.RemoveLast();
				_valuesQueue.AddFirst(new LinkedListNode<TKey>(key));
			}
			else
			{
				_valuesQueue.AddFirst(new LinkedListNode<TKey>(key));
			}

			if (_valuesQueue.Any() && _valuesQueue.First!=null)
			{
				_cache.Add(key, _valuesQueue.First);
			}
			_storage.Add(key, value);
		}

		protected override void UpdateCache(TKey key, TValue value)
		{

			if (!_cache.ContainsKey(key))
			{
				return;
			}
			var newHead = _cache[key];
			_valuesQueue.Remove(newHead);
			_valuesQueue.AddFirst(newHead);
		}
	}
}