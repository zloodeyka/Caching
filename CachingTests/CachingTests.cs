using Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ����� ���� �������� ������ ��� ����, ����� ���� ������� ��������� ������
namespace CachingTests
{
	[TestClass]
	public class CachingTests
	{
		[TestMethod]
		public void TestLru()
		{
			var cache = new LRUCache<string, int>(3);

			cache.Set("����", 0);
			cache.Set("����", 1);
			cache.Set("���", 2);
			cache.Set("���", 3);

			int cachedValue;
			Assert.AreEqual(cache.Get("����", out cachedValue), false);

			cache.Set("���", 5);

			Assert.AreEqual(cache.Get("���", out cachedValue), true);
			Assert.AreEqual(cachedValue, 2);
		}

		[TestMethod]
		public void TestMru()
		{
			var cache = new MRUCache<string, int>(3);

			cache.Set("����", 0);
			cache.Set("����", 1);
			cache.Set("���", 2);
			cache.Set("���", 3);

			int cachedValue;
			Assert.AreEqual(cache.Get("����", out cachedValue), true);

			Assert.AreEqual(cache.Get("���", out cachedValue), false);

			cache.Set("���", 2);

			Assert.AreEqual(cache.Get("���", out cachedValue), true);
			Assert.AreEqual(cachedValue, 2);
		}
	}
}