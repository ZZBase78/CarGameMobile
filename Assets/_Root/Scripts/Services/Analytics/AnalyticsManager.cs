using UnityEngine;
using Services.Analytics.UnityAnalytics;
using System;

namespace Services.Analytics
{
    internal class AnalyticsManager : MonoBehaviour
    {
        private IAnalyticsService[] _services;


        private void Awake() =>
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };


        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");

        public void LevelStarted() =>
            SendEvent("LevelStarted");

        private void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }

        public UnityEngine.Analytics.AnalyticsResult[] Transaction(string productId, Decimal amount, string currency)
        {
            UnityEngine.Analytics.AnalyticsResult[] result = new UnityEngine.Analytics.AnalyticsResult[_services.Length];
            for (int i = 0; i < _services.Length; i++)
            {
                result[i] = _services[i].Transaction(productId, amount, currency);
            }
            return result;
        }
    }
}
