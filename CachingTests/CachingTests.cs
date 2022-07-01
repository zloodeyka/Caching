using Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Тесты были написаны просто для того, чтобы было удобнее проверять работу
namespace CachingTests
{
	[TestClass]
	public class CachingTests
	{
		[TestMethod]
		public void TestLru()
		{
			var cache = new LRUCache<string, int>(3);

			cache.Set("Ноль", 0);
			cache.Set("Один", 1);
			cache.Set("Два", 2);
			cache.Set("Три", 3);

			int cachedValue;
			Assert.AreEqual(cache.Get("Ноль", out cachedValue), false);

			cache.Set("Два", 5);

			Assert.AreEqual(cache.Get("Два", out cachedValue), true);
			Assert.AreEqual(cachedValue, 2);
		}

		[TestMethod]
		public void TestMru()
		{
			var cache = new MRUCache<string, int>(3);

			cache.Set("Ноль", 0);
			cache.Set("Один", 1);
			cache.Set("Два", 2);
			cache.Set("Три", 3);

			int cachedValue;
			Assert.AreEqual(cache.Get("Ноль", out cachedValue), true);

			Assert.AreEqual(cache.Get("Два", out cachedValue), false);

			cache.Set("Два", 2);

			Assert.AreEqual(cache.Get("Два", out cachedValue), true);
			Assert.AreEqual(cachedValue, 2);
		}
	}
}