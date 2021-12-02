/// <author>Mitul Kataria</author>
/// <created>1/12/2021</created>
/// <summary>
///		This file consists of utility methods
///		for testing Dashboard View Model.
/// </summary>

using Dashboard.Server.Telemetry;
using System;

namespace Testing.UX.Dashboard
{
    public class Utils
    {

        private static SessionAnalytics _sampleAnalytics;

        /// <summary>
        /// Generates an analytics object 
        /// with desired engagement rate
        /// </summary>
        /// <param name="rate">Expected rate</param>
        /// <returns>Session analytics object</returns>
        public static SessionAnalytics GenerateAnalyticsForEngagementRate(string rate)
        {
            _sampleAnalytics = new();
            if (rate == "0%")
            {
                _sampleAnalytics.chatCountForEachUser = new();
                return _sampleAnalytics;
            }
            else if (rate == "50%")
            {
                _sampleAnalytics.chatCountForEachUser = new();
                _sampleAnalytics.chatCountForEachUser.Add(1, 10);
                _sampleAnalytics.chatCountForEachUser.Add(2, 0);
                return _sampleAnalytics;
            }
            else if (rate == "67%")
            {
                _sampleAnalytics.chatCountForEachUser = new();
                _sampleAnalytics.chatCountForEachUser.Add(1, 5);
                _sampleAnalytics.chatCountForEachUser.Add(2, 5);
                _sampleAnalytics.chatCountForEachUser.Add(3, 0);
                return _sampleAnalytics;
            }
            else if (rate == "100%")
            {
                _sampleAnalytics.chatCountForEachUser = new();
                _sampleAnalytics.chatCountForEachUser.Add(1, 5);
                return _sampleAnalytics;
            }
            return _sampleAnalytics;
        }

        public static SessionAnalytics GenerateAnalyticsInstance(string instanceType)
        {
            _sampleAnalytics = new();
            if (instanceType == "OnlyUserVsChatCountDict")
            {
                _sampleAnalytics.chatCountForEachUser = new();
                _sampleAnalytics.chatCountForEachUser.Add(1, 5);
                _sampleAnalytics.chatCountForEachUser.Add(2, 10);
                _sampleAnalytics.chatCountForEachUser.Add(3, 15);

                return _sampleAnalytics;
            }
            else if(instanceType == "OnlyTimestampVsUsersDict")
            {
                _sampleAnalytics.userCountAtAnyTime = new();
                _sampleAnalytics.userCountAtAnyTime.Add(
                    new DateTime(2021, 12, 1, 5, 10, 00), 5);
                _sampleAnalytics.userCountAtAnyTime.Add(
                    new DateTime(2021, 12, 1, 5, 15, 00), 10);
                _sampleAnalytics.userCountAtAnyTime.Add(
                    new DateTime(2021, 12, 1, 5, 20, 00), 15);

                return _sampleAnalytics;
            }
            else if (instanceType == "OnlyInsincereMembers")
            {
                _sampleAnalytics.insincereMembers = new();
                _sampleAnalytics.insincereMembers.Add(1);
                _sampleAnalytics.insincereMembers.Add(2);
                _sampleAnalytics.insincereMembers.Add(3);

                return _sampleAnalytics;
            }
            else if(instanceType == "Complete")
            {
                _sampleAnalytics.chatCountForEachUser = new();
                _sampleAnalytics.chatCountForEachUser.Add(1, 5);
                _sampleAnalytics.chatCountForEachUser.Add(2, 10);
                _sampleAnalytics.chatCountForEachUser.Add(3, 15);

                _sampleAnalytics.userCountAtAnyTime = new();
                _sampleAnalytics.userCountAtAnyTime.Add(
                    new DateTime(2021, 12, 1, 5, 10, 00), 5);
                _sampleAnalytics.userCountAtAnyTime.Add(
                    new DateTime(2021, 12, 1, 5, 15, 00), 10);
                _sampleAnalytics.userCountAtAnyTime.Add(
                    new DateTime(2021, 12, 1, 5, 20, 00), 15);

                _sampleAnalytics.insincereMembers = new();
                _sampleAnalytics.insincereMembers.Add(1);
                _sampleAnalytics.insincereMembers.Add(2);
                _sampleAnalytics.insincereMembers.Add(3);

                return _sampleAnalytics;
            }
            return _sampleAnalytics;
        }
    }
}
