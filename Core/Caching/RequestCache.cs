using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace GovTown.Core.Caching
{
    
    public partial class RequestCache : ICache
    {
        private const string REGION_NAME = "$$GovTownNET$$";
        private readonly HttpContext _context;

        public RequestCache(HttpContext context)
        {
            this._context = context;
        }

        protected IDictionary<object,object> GetItems()
        {
            if (_context != null)
                return _context.Items;

            return null;
        }

        public IEnumerable<KeyValuePair<string, object>> Entries
        {
            get
            {
                var items = GetItems();
                if (items == null)
                    yield break;

                var enumerator = items.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    string key = enumerator.Current.Key as string;
                    if (key == null)
                        continue;
                    if (key.StartsWith(REGION_NAME))
                    {
                        yield return new KeyValuePair<string, object>(key.Substring(REGION_NAME.Length), enumerator.Current.Value);
                    }
                }
            }
        }

        public object Get(string key)
        {
            var items = GetItems();
            if (items == null)
                return null;

			return items[BuildKey(key)];
        }

		public void Set(string key, object value, int? cacheTime)
		{
			var items = GetItems();
			if (items == null)
				return;

			key = BuildKey(key);

			if (value != null)
			{
				if (items.Keys.Contains(key))
					items[key] = value;
				else
					items.Add(key, value);
			}
		}

        public bool Contains(string key)
        {
            var items = GetItems();
            if (items == null)
                return false;

            return items.Keys.Contains(BuildKey(key));
        }

        public void Remove(string key)
        {
            var items = GetItems();
            if (items == null)
                return;

            items.Remove(BuildKey(key));
        }

        private string BuildKey(string key)
        {
            return key.HasValue() ? REGION_NAME + key : null;
        }

		public bool IsSingleton
		{
			get { return false; }
		}

	}

}
