using Microsoft.AspNetCore.Http;
using TransitDesk.Web.Extensions;
using Xunit;

public class SessionExtensionsTests
{
    private class TestSession : ISession
    {
        private readonly Dictionary<string, byte[]> _storage = new();
        public bool IsAvailable => true;
        public string Id => Guid.NewGuid().ToString();
        public IEnumerable<string> Keys => _storage.Keys;
        public void Clear() => _storage.Clear();
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public void Remove(string key) => _storage.Remove(key);
        public void Set(string key, byte[] value) => _storage[key] = value;
        public bool TryGetValue(string key, out byte[] value) => _storage.TryGetValue(key, out value!);
    }

    [Fact]
    public void SessionExtensions_SerializesAndDeserializesComplexObject()
    {
        ISession session = new TestSession();
        var watchlist = new List<int> { 1000, 2072, 9999 };

        session.SetObject("WatchlistTest", watchlist);
        var retrievedList = session.GetObject<List<int>>("WatchlistTest");

        Assert.NotNull(retrievedList);
        Assert.Equal(3, retrievedList.Count);
        Assert.Contains(2072, retrievedList);
    }
}