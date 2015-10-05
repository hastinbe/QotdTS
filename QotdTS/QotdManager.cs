#region Copyright & License Information
/*
 * Copyright 2015 Beau D. Hastings
 * This file is part of VocationPlugin, a plugin for TShock, which is free
 * software. It is made available to you under the terms of the GNU 
 * General Public License as published by the Free Software Foundation.
 * For more information, see COPYING.
 */
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using TShockAPI;

namespace QotdTS
{
	/// <summary>
	/// Qotd manager.
	/// </summary>
	internal class QotdManager
	{
		/// <summary>
		/// Gets the cache path.
		/// </summary>
		/// <value>The cache path.</value>
		public static string CachePath
		{
			get
			{
				return Path.Combine(Path.GetTempPath(), "qotd.cache");
			}
		}

		/// <summary>
		/// Gets a value indicating is cache expired.
		/// </summary>
		/// <value><c>true</c> if is cache expired; otherwise, <c>false</c>.</value>
		private static bool IsCacheExpired
		{
			get
			{
				if (!File.Exists(CachePath))
					return true;

				var modified = File.GetLastWriteTime(CachePath);
				return (DateTime.Compare(DateTime.Now, modified.AddSeconds(Plugin.Config.CacheExpires)) >= 0);
			}
		}

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>The URL.</value>
		public static string Url
		{
			get
			{
				var queryParams = new Dictionary<string, string>();

				Action<string, string> add = (name, value) =>
				{
					if (!String.IsNullOrEmpty(name))
					if (!String.IsNullOrEmpty(value))
							queryParams.Add(name, value);
				};

				add("api_key", Plugin.Config.ApiKey);
				add("category", Plugin.Config.QuoteCategory);

				var query = queryParams
					.Select(param => param.Key + "=" + WebUtility.UrlEncode(param.Value));
	
				return new UriBuilder(Plugin.Config.QotdUrl)
					.Query = String.Join("&", query)
					.ToString();
			}
		}

		/// <summary>
		/// Fetchs the Quote of the day.
		/// </summary>
		public static void FetchQotd()
		{
			if (IsCacheExpired)
				using (var Client = new WebClient())
					Client.DownloadFileTaskAsync(new Uri(Plugin.Config.QotdUrl), CachePath).Wait();

			var json = JObject.Parse(File.ReadAllText(CachePath));
			var quote = json.SelectToken("contents.quotes[0].quote").ToString();
			Plugin.Qotd = new Quote { Text = quote };
		}
	}
}
