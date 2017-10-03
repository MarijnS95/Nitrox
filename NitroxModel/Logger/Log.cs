using System;
using System.Diagnostics;

namespace NitroxModel.Logger
{
    public class Log
    {
        // For in-game notifications
        public static void InGame(String msg)
        {
            ErrorMessage.AddMessage(msg);

            Info(msg);
        }

        private static void Write(String fmt, params Object[] arg)
        {
            Console.WriteLine("[Nitrox] " + fmt, arg);
        }

        public static void Error(String fmt, params Object[] arg)
        {
            Write("E: " + fmt, arg);
        }

        public static void Error(String msg, Exception ex)
        {
            Error(msg + "\n{0}", (object)ex);
        }

        public static void Warn(String fmt, params Object[] arg)
        {
            Write("W: " + fmt, arg);
        }

        public static void Info(String fmt, params Object[] arg)
        {
            Write("I: " + fmt, arg);
        }

        public static void Info(Object o)
        {
            String msg = (o == null) ? "null" : o.ToString();
            Info(msg);
        }

        // Only for debug prints. Should not be displayed to general user.
        // Should we print the calling method for this for more debug context?
        [Conditional("DEBUG")]
        public static void Debug(String fmt, params Object[] arg)
        {
            Write("D: " + fmt, arg);
        }

        [Conditional("DEBUG")]
        public static void Debug(Object o)
        {
            String msg = (o == null) ? "null" : o.ToString();
            Debug(msg);
        }

        public static void Trace(String fmt, params Object[] arg)
        {
            Trace(string.Format(fmt, arg));
        }

        public static void Trace(String str = "")
        {
            Write("T: {0}:\n{1}", str, new StackTrace(1));
        }
    }
}
