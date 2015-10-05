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
using System.IO;

using Newtonsoft.Json;

namespace QotdTS
{
	/// <summary>
	/// Config manager.
	/// </summary>
	internal class ConfigManager
	{
		/// <summary>
		/// Reads a configuration file from a given path, creates it if it doesn't exist.
		/// </summary>
		public static ConfigFile Read(string path)
		{
			if (String.IsNullOrEmpty(path))
				throw new ArgumentException("A path is required");

			if (!File.Exists(path))
				return ConfigManager.Write(new ConfigFile());

			return JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText(path));
		}

		/// <summary>
		/// Writes the configuration to a given path.
		/// </summary>
		public static ConfigFile Write(ConfigFile config)
		{
			if (String.IsNullOrEmpty(ConfigFile.ConfigPath))
				throw new ArgumentException("A path is required");
			
			File.WriteAllText(ConfigFile.ConfigPath, JsonConvert.SerializeObject(config, Formatting.Indented));
			return config;
		}
	}
}
