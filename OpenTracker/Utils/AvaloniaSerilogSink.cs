using Avalonia.Controls;
using Avalonia.Logging;
using Serilog;
using System;
using System.Collections.Generic;

namespace OpenTracker.Utils
{
    public class AvaloniaSerilogSink : ILogSink
    {
        private readonly ILogger _logger;

        public AvaloniaSerilogSink(string file, Serilog.Events.LogEventLevel minimumLevel)
        {
            _logger = new LoggerConfiguration().WriteTo.File(
                file, minimumLevel, rollingInterval: RollingInterval.Day).CreateLogger();
        }
        
        public bool IsEnabled(LogEventLevel level, string area)
        {
            return _logger.IsEnabled((Serilog.Events.LogEventLevel)level);
        }

        public void Log(LogEventLevel level, string area, object source, string messageTemplate)
        {
            Log(level, area, source, messageTemplate, Array.Empty<object>());
        }

        public void Log<T0>(
            LogEventLevel level, string area, object source, string messageTemplate,
            T0 propertyValue0)
        {
            Log(level, area, source, messageTemplate, new object[] { propertyValue0 });
        }

        public void Log<T0, T1>(
            LogEventLevel level, string area, object source, string messageTemplate,
            T0 propertyValue0, T1 propertyValue1)
        {
            Log(
                level, area, source, messageTemplate,
                new object[] { propertyValue0, propertyValue1 });
        }

        public void Log<T0, T1, T2>(
            LogEventLevel level, string area, object source, string messageTemplate,
            T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log(
                level, area, source, messageTemplate,
                new object[] { propertyValue0, propertyValue1, propertyValue2 });
        }

        public void Log(LogEventLevel level, string area, object source, string messageTemplate, params object[] propertyValues)
        {
            for (int i = 0; i < propertyValues.Length; i++)
            {
                propertyValues[i] = GetHierarchy(propertyValues[i]);
            }

            _logger.Write((Serilog.Events.LogEventLevel)level, messageTemplate, propertyValues);
        }

        private static object GetHierarchy(object source)
        {
            if (source is IControl visual)
            {
                List<string> hierarchy = new List<string>();
                hierarchy.Add(visual.ToString());

                while ((visual = visual.Parent) != null)
                {
                    hierarchy.Insert(0, visual.ToString());
                }

                return string.Join("/", hierarchy);
            }

            return source;
        }
    }
}
