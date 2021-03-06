﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineVideos;

namespace Standalone
{
	public class Logger : MarshalByRefObject, ILog
    {
		#region MarshalByRefObject overrides
		public override object InitializeLifetimeService()
		{
			// In order to have the lease across appdomains live forever, we return null.
			return null;
		}
		#endregion

        NLog.Logger logger;

        public Logger(string dir)
        {
            NLog.Config.LoggingConfiguration config = new NLog.Config.LoggingConfiguration();
            NLog.Targets.FileTarget fileTarget = new NLog.Targets.FileTarget();
            config.AddTarget("file", fileTarget);
            fileTarget.FileName = System.IO.Path.Combine(dir, "log.txt");
            fileTarget.Layout = "${date:format=MM/dd HH\\:mm\\:ss.ffff}|${level:uppercase=true:padding=6}| ${message}";
            fileTarget.DeleteOldFileOnStartup = true;
            NLog.Config.LoggingRule rule = new NLog.Config.LoggingRule("*", NLog.LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule);
            NLog.LogManager.Configuration = config;
            logger = NLog.LogManager.GetLogger("OnlineVideos");
        }

        public void Debug(string format, params object[] arg)
        {
            logger.Debug(format, arg);
        }

        public void Error(Exception ex)
        {
            logger.Error(ex);
        }

        public void Error(string format, params object[] arg)
        {
            logger.Error(format, arg);
        }

        public void Info(string format, params object[] arg)
        {
            logger.Info(format, arg);
        }

        public void Warn(string format, params object[] arg)
        {
            logger.Warn(format, arg);
        }
    }
}
