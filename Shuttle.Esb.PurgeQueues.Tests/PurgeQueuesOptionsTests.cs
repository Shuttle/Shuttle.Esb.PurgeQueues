using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Shuttle.Esb.PurgeQueues.Tests
{
	[TestFixture]
	public class PurgeQueuesOptionsTests
	{
		protected PurgeQueuesOptions GetOptions()
		{
			var result = new PurgeQueuesOptions();

			new ConfigurationBuilder()
				.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\appsettings.json")).Build()
				.GetRequiredSection($"{PurgeQueuesOptions.SectionName}").Bind(result);

			return result;
		}

		[Test]
		public void Should_be_able_to_load_the_options()
		{
			var options = GetOptions();

			Assert.IsNotNull(options);
			Assert.AreEqual(2, options.Uris.Count);

			foreach (var uri in options.Uris)
			{
				Console.WriteLine(uri);
			}
		}
	}
}