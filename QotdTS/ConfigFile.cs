#region Copyright & License Information
/*
 * Copyright 2015 Beau D. Hastings
 * This file is part of VocationPlugin, a plugin for TShock, which is free
 * software. It is made available to you under the terms of the GNU 
 * General Public License as published by the Free Software Foundation.
 * For more information, see COPYING.
 */
#endregion

using System.IO;
using System.ComponentModel;

namespace QotdTS
{
	internal class ConfigFile
	{
		/// <summary>
		/// Path to the file containing the config.
		/// </summary>
		public static string ConfigPath
		{
			get
			{
				return Path.Combine(TShockAPI.TShock.SavePath, "qotd.json");
			}
		}

		/// <summary>
		/// The cache expiration time in seconds.
		/// </summary>
		[Description("Cache expiration time in seconds.")]
		public int CacheExpires = 86400;

		/// <summary>
		/// The qotd URL.
		/// </summary>
		[Description("URL to retrieve QOTD")]
		public string QotdUrl = "http://api.theysaidso.com/qod.json";

		/// <summary>
		/// Your subscription API key.
		/// </summary>
		[Description("An API key (without subscription service is limited to 10 api calls/hr )")]
		public string ApiKey = "";

		/// <summary>
		/// The category.
		/// </summary>
		[Description("A quote of the day category (requires subscription)")]
		public string QuoteCategory = "";
	}
}
