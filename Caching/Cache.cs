namespace Caching
{
	public abstract class Cache<TKey, TValue>
	{
		protected Dictionary<TKey, TValue> _storage = new();
		protected Dictionary<TKey, LinkedListNode<TKey>> _cache = new();
		protected LinkedList<TKey> _valuesQueue = new();

		protected int _capacity;
		protected abstract void AddToCache(TKey key, TValue value);
		protected abstract void UpdateCache(TKey key, TValue value);

		public bool Get(TKey key, out TValue? value)
		{
			if (_storage.ContainsKey(key))
			{
				var keyVal = _storage[key];
				UpdateCache(key, keyVal);
				value =  keyVal;
				return true;
			}

			value = default;
			return false;
		}

		public void Set(TKey key, TValue value)
		{
			if (key == null)
			{
				return;
			}

			if (_cache.ContainsKey(key))
			{
				UpdateCache(key, value);
			}
			else
			{
				AddToCache(key, value);
			}
		}

		public List<TKey> GetCachedKeys()
		{
			return _valuesQueue.ToList();
		}
	}
}