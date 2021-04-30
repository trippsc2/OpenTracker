using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Logging;
using Serilog;

namespace OpenTracker.Models
{
    /// <summary>
    ///     This class contains logic for converting Avalonia logs to Serilog file logs.
    /// </summary>
    public class AvaloniaSerilogSink : ILogSink
    {
        private readonly ILogger _logger;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="file">
        ///     The file path to which the logs will output.
        /// </param>
        /// <param name="minimumLevel">
        ///     The minimum logging level to output.
        /// </param>
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
            LogEventLevel level, string area, object source, string messageTemplate, T0 propertyValue0)
        {
            if (propertyValue0 is null)
            {
                throw new ArgumentNullException(nameof(propertyValue0));
            }
            
            Log(level, area, source, messageTemplate, new object[] { propertyValue0 });
        }

        public void Log<T0, T1>(
            LogEventLevel level, string area, object source, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1)
        {
            if (propertyValue0 is null)
            {
                throw new ArgumentNullException(nameof(propertyValue0));
            }
            
            if (propertyValue1 is null)
            {
                throw new ArgumentNullException(nameof(propertyValue1));
            }

            Log(level, area, source, messageTemplate, new object[]
            {
                propertyValue0, propertyValue1
            });
        }

        public void Log<T0, T1, T2>(
            LogEventLevel level, string area, object source, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1, T2 propertyValue2)
        {
            if (propertyValue0 is null)
            {
                throw new ArgumentNullException(nameof(propertyValue0));
            }
            
            if (propertyValue1 is null)
            {
                throw new ArgumentNullException(nameof(propertyValue1));
            }
            
            if (propertyValue2 is null)
            {
                throw new ArgumentNullException(nameof(propertyValue2));
            }

            Log(level, area, source, messageTemplate, new object[]
            {
                propertyValue0, propertyValue1, propertyValue2
            });
        }

        public void Log(LogEventLevel level, string area, object source, string messageTemplate, params object[] propertyValues)
        {
            for (var i = 0; i < propertyValues.Length; i++)
            {
                propertyValues[i] = GetHierarchy(propertyValues[i]);
            }

            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.Write((Serilog.Events.LogEventLevel)level, messageTemplate, propertyValues);
        }

        /// <summary>
        ///     Gets the hierarchy of the Avalonia control object.
        /// </summary>
        /// <param name="source">
        ///     An Avalonia control from which the log is generated as an object.
        /// </param>
        /// <returns>
        ///     A string representing the visual hierarchy of the control as an object.
        /// </returns>
        private static object GetHierarchy(object source)
        {
            if (source is not IControl visual)
            {
                return source;
            }
            
            var visualString = visual.ToString() ?? throw new NullReferenceException();

            var hierarchy = new List<string>
            {
                visualString
            };

            while ((visual = visual.Parent) is not null)
            {
                visualString = visual.ToString() ??
                               throw new NullReferenceException();

                hierarchy.Insert(0, visualString);
            }

            return string.Join("/", hierarchy);

        }
    }
}
