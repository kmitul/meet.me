/// <author>Mitul Kataria</author>
/// <created>27/11/2021</created>
/// <summary>
///		This is the unit testing file
///		for the Dashboard UX module which
///		validates that the Dashboard View Model
///		performs as expected.
/// </summary>

using Client.ViewModel;
using Dashboard.Server.Telemetry;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Testing.UX.Dashboard
{
    [TestFixture]
    public class DashboardViewModelUnitTests
    {

        [SetUp]
        public void SetUp()
        {
            _viewModel = new DashboardViewModel();
        }

        [Test]
        public void SetupTesting()
        {
            Assert.NotNull(_viewModel);
        }

        /// <summary>
        /// Tests the initial values
        /// </summary>
        [Test]
        public void TestingInitializations()
        {
            // Assert.AreEqual(expected value, actual value)
            Assert.AreEqual("Refresh to get the latest stats!",
                            _viewModel.chatSummary);
            Assert.AreEqual(1, _viewModel.usersList.Count);
            Assert.AreEqual(1, _viewModel.messagesCountList.Count);
            Assert.AreEqual(1, _viewModel.timestampList.Count);
            Assert.AreEqual(1, _viewModel.usersCountList.Count);

            Assert.AreEqual(0, _viewModel.messagesCount);
            Assert.AreEqual(1, _viewModel.participantsCount);
            Assert.AreEqual("0%", _viewModel.engagementRate);
        }

        /// <summary>
        /// Tests the analytics update
        /// </summary>
        [Test]
        public void OnAnalyticsChanged_ShouldUpdateSessionAnalytics()
        {
            // Arrange
            SessionAnalytics sampleAnalytics = new();
            sampleAnalytics.chatCountForEachUser = new();
            sampleAnalytics.chatCountForEachUser.Add(1, 5);
            sampleAnalytics.chatCountForEachUser.Add(2, 10);
            sampleAnalytics.chatCountForEachUser.Add(3, 15);

            sampleAnalytics.userCountAtAnyTime = new();
            sampleAnalytics.userCountAtAnyTime.Add(
                new DateTime(2021, 12, 1, 5, 10, 00), 5);
            sampleAnalytics.userCountAtAnyTime.Add(
                new DateTime(2021, 12, 1, 5, 15, 00), 10);
            sampleAnalytics.userCountAtAnyTime.Add(
                new DateTime(2021, 12, 1, 5, 20, 00), 15);

            sampleAnalytics.insincereMembers = new();
            sampleAnalytics.insincereMembers.Add(1);
            sampleAnalytics.insincereMembers.Add(2);
            sampleAnalytics.insincereMembers.Add(3);

            // Act
            _viewModel.OnAnalyticsChanged(sampleAnalytics);
            _sessionAnalytics = _viewModel.GetSessionAnalytics();

            // Assert
            Assert.AreEqual(sampleAnalytics.chatCountForEachUser,
                            _sessionAnalytics.chatCountForEachUser);
            Assert.AreEqual(sampleAnalytics.userCountAtAnyTime,
                            _sessionAnalytics.userCountAtAnyTime);
            Assert.AreEqual(sampleAnalytics.insincereMembers,
                            _sessionAnalytics.insincereMembers);
        }

        /// <summary>
        /// Tests the summary updates
        /// </summary>
        [Test]
        [TestCase("")]
        [TestCase("This is some non-null summary text!")]
        public void OnSummaryChanged_ShouldUpdateSummaryText(string sampleSummary)
        {
            // Act
            _viewModel.OnSummaryChanged(sampleSummary);

            // Assert
            Assert.AreEqual(sampleSummary, _viewModel.chatSummary);
        }

        /// <summary>
        /// Tests the engagement rate calculations
        /// </summary>
        /// <param name="expectedRate">Expected engagement rate</param>
        [Test]
        [TestCase("0%")]
        [TestCase("50%")]
        [TestCase("67%")]
        [TestCase("100%")]
        public void CalculateEngamentRate_ShouldReturnCorrectRate(string expectedRate)
        {

            // Arrange
            _expectedAnalytics = Utils.GenerateAnalyticsForEngagementRate(expectedRate);

            // Act
            _viewModel.OnAnalyticsChanged(_expectedAnalytics);
            _sessionAnalytics = _viewModel.GetSessionAnalytics();

            // Assert
            Assert.AreEqual(expectedRate, _viewModel.CalculateEngagementRate(
                new List<int>(_sessionAnalytics.chatCountForEachUser.Keys),
                new List<int>(_sessionAnalytics.chatCountForEachUser.Values)));
        }

        private DashboardViewModel _viewModel;
        // Actual analytics
        private SessionAnalytics _sessionAnalytics;
        // For testing
        private SessionAnalytics _expectedAnalytics;
    }
}
