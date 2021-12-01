/// <author>Mitul Kataria</author>
/// <created>1/12/2021</created>
/// <summary>
///		This file consists of utility methods
///		for testing Dashboard View Model.
/// </summary>

using Dashboard.Server.Telemetry;

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
            if (rate == "0%")
            {
                _sampleAnalytics = new();
                _sampleAnalytics.chatCountForEachUser = new();
                _sampleAnalytics.chatCountForEachUser.Add(1, 0);
                return _sampleAnalytics;
            }
            else if (rate == "50%")
            {
                _sampleAnalytics = new();
                _sampleAnalytics.chatCountForEachUser = new();
                _sampleAnalytics.chatCountForEachUser.Add(1, 10);
                _sampleAnalytics.chatCountForEachUser.Add(2, 0);
                return _sampleAnalytics;
            }
            else if (rate == "67%")
            {
                _sampleAnalytics = new();
                _sampleAnalytics.chatCountForEachUser = new();
                _sampleAnalytics.chatCountForEachUser.Add(1, 5);
                _sampleAnalytics.chatCountForEachUser.Add(2, 5);
                _sampleAnalytics.chatCountForEachUser.Add(3, 0);
                return _sampleAnalytics;
            }
            else if (rate == "100%")
            {
                _sampleAnalytics = new();
                _sampleAnalytics.chatCountForEachUser = new();
                _sampleAnalytics.chatCountForEachUser.Add(1, 5);
                return _sampleAnalytics;
            }
            return null;
        }
    }
}
