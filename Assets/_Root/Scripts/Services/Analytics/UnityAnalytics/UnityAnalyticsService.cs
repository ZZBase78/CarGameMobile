using System;
using System.Collections.Generic;

namespace Services.Analytics.UnityAnalytics
{
    internal class UnityAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName) =>
            UnityEngine.Analytics.Analytics.CustomEvent(eventName);

        public void SendEvent(string eventName, Dictionary<string, object> eventData) =>
            UnityEngine.Analytics.Analytics.CustomEvent(eventName, eventData);

        public UnityEngine.Analytics.AnalyticsResult Transaction(string productId, Decimal amount, string currency)
        {
            return UnityEngine.Analytics.Analytics.Transaction(productId, amount, currency);
        }
    }
}
