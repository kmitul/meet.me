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
using System.Collections.Generic;
using System.ComponentModel;

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
        [TestCase("")]
        [TestCase("OnlyUserVsChatCountDict")]
        [TestCase("OnlyTimestampVsUsersDict")]
        [TestCase("OnlyInsincereMembers")]
        [TestCase("Complete")]
        public void OnAnalyticsChanged_ShouldUpdateSessionAnalytics(string instanceType)
        {
            // Arrange
            _expectedAnalytics = Utils.GenerateAnalyticsInstance(instanceType);

            // Act
            _viewModel.OnAnalyticsChanged(_expectedAnalytics);
            _sessionAnalytics = _viewModel.GetSessionAnalytics();

            // Assert
            Assert.AreEqual(_expectedAnalytics.chatCountForEachUser,
                _sessionAnalytics.chatCountForEachUser);
            Assert.AreEqual(_expectedAnalytics.userCountAtAnyTime,
                            _sessionAnalytics.userCountAtAnyTime);
            Assert.AreEqual(_expectedAnalytics.insincereMembers,
                            _sessionAnalytics.insincereMembers);
        }

        /// <summary>
        /// Tests the summary updates
        /// </summary>
        [Test]
        [TestCase(null)]
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

        [Test]
        public void OnPropertyChanged_ShouldRaiseEvent()
        {
            // Arrange
            List<string> receivedEvents = new();
            _viewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };

            // Act
            _viewModel.chatSummary += "New string appended!";
            _viewModel.messagesCount += 10;
            _viewModel.participantsCount += 5;

            // Assert
            Assert.AreEqual(3, receivedEvents.Count);
            Assert.AreEqual(nameof(_viewModel.chatSummary), receivedEvents[0]);
            Assert.AreEqual(nameof(_viewModel.messagesCount), receivedEvents[1]);
            Assert.AreEqual(nameof(_viewModel.participantsCount), receivedEvents[3]);
        }

        [Test]
        public void CalculateEngangeMentRate_NullContext()
        {
            // Arrange
            _expectedAnalytics = Utils.GenerateAnalyticsForEngagementRate("100%");

            // Act
            _viewModel.OnAnalyticsChanged(_expectedAnalytics);
            _sessionAnalytics = _viewModel.GetSessionAnalytics();

            // Assert
            Assert.AreEqual("0%", _viewModel.CalculateEngagementRate(null,
                new List<int>(_sessionAnalytics.chatCountForEachUser.Values)));
            Assert.AreEqual("0%", _viewModel.CalculateEngagementRate(
                new List<int>(_sessionAnalytics.chatCountForEachUser.Keys),null));
            Assert.AreEqual("0%", _viewModel.CalculateEngagementRate(null,null));
        }

        [Test]
        public void OnAnalyticsChanged_NullContext()
        {
            // Arrange
            _expectedAnalytics = null;

            // Act
            _viewModel.OnAnalyticsChanged(_expectedAnalytics);
            _sessionAnalytics = _viewModel.GetSessionAnalytics();

            // Assert
            Assert.NotNull(_sessionAnalytics);
        }

        private DashboardViewModel _viewModel;
        // Actual analytics
        private SessionAnalytics _sessionAnalytics;
        // For testing
        private SessionAnalytics _expectedAnalytics;
    }
}
