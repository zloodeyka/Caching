
namespace Caching
{
	public  class MRUCache<TKey, TValue>:Cache<TKey, TValue>
	{

		public MRUCache(int capacity)
		{
			_capacity = capacity;
		}
		protected override void AddToCache(TKey key, TValue value)
		{
			if (_storage.Count >= _capacity)
			{
				var first = _valuesQueue.Last;
				_cache.Remove(first.Value);
				_storage.Remove(first.Value);
				_valuesQueue.RemoveLast();
				_valuesQueue.AddLast(new LinkedListNode<TKey>(key));
			}
			else
			{
				_valuesQueue.AddLast(new LinkedListNode<TKey>(key));
			}
			if (_valuesQueue.Any() && _valuesQueue.First != null)
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
			_valuesQueue.AddLast(newHead);
		}
	}
}
