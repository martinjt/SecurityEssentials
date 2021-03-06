﻿using System;
using System.Globalization;
using SecurityEssentials.Acceptance.Tests.Web.Pages;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SecurityEssentials.Acceptance.Tests.Utility;

namespace SecurityEssentials.Acceptance.Tests.Web.Extensions
{
	[Binding]
	public class BasePageSteps
	{

		[Then(@"an error message is shown '(.*)'")]
		public void ThenAnErrorMessageIsShown(string message)
		{
			var driver = FeatureContext.Current.GetWebDriver();
			Assert.IsTrue(driver.PageSource.Contains(message));
		}

	}
}
