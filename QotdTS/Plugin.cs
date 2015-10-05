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
using System.Reflection;

using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace QotdTS
{
	/// <summary>
	/// Plugin.
	/// </summary>
	[ApiVersion(1, 22)]
	public class Plugin : TerrariaPlugin
	{
		/// <summary>
		/// The config.
		/// </summary>
		internal static ConfigFile Config;

		/// <summary>
		/// The quote of the day.
		/// </summary>
		internal static Quote Qotd;

		/// <summary>
		/// Initializes a new instance of the <see cref="QotdTS.Plugin"/> class.
		/// </summary>
		/// <param name="game">Game.</param>
		public Plugin(Main game) : base(game)
		{
		}

		#region General plugin information
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public override string Name
		{
			get
			{
				return GetType().Assembly.GetName().Name;
			}
		}

		/// <summary>
		/// Gets the version.
		/// </summary>
		/// <value>The version.</value>
		public override Version Version
		{
			get
			{
				return GetType().Assembly.GetName().Version;
			}
		}

		/// <summary>
		/// Gets the author.
		/// </summary>
		/// <value>The author.</value>
		public override string Author
		{
			get
			{
				return ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(
					GetType().Assembly, typeof(AssemblyCompanyAttribute))).Company;
			}
		}

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public override string Description
		{
			get
			{
				return ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(
					GetType().Assembly, typeof(AssemblyDescriptionAttribute))).Description;
			}
		}
		#endregion

		/// <summary>
		/// Plugin initialization.
		/// </summary>
		public override void Initialize()
		{
			Config = ConfigManager.Read(ConfigFile.ConfigPath);
			ServerApi.Hooks.NetGreetPlayer.Register(this, OnGreetPlayer);
		}

		/// <summary>
		/// Dispose the plugin.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ServerApi.Hooks.NetGreetPlayer.Deregister(this, OnGreetPlayer);
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Greet player event.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public static void OnGreetPlayer(GreetPlayerEventArgs args)
		{
			var player = TShock.Players[args.Who];
			if (player == null)
				return;

			QotdManager.FetchQotd();

			if (!String.IsNullOrEmpty(Plugin.Qotd.Text))
				player.SendInfoMessage("Quote of the day: " + Plugin.Qotd.Text);
		}
	}
}
