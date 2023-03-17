using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptTools
{
    public class TimeUtil
    {
        public struct ShowTime
        {
            public int Second;
            public int Minute;
            public int Hour;
            public int Day;
        }

        /// <summary>
        /// 时间精确类型
        /// </summary>
        public enum ETimeCorrectToType
        {
            Second,
            Minute,
            Hour,
            Day,
        }

        //MSDN文档的介绍:https://docs.microsoft.com/zh-cn/dotnet/api/system.datetime.ticks?redirectedfrom=MSDN&view=netframework-4.8
        //一个计时周期表示一百纳秒，即一千万分之一秒。 1 毫秒内有 10,000 个计时周期，即 1 秒内有 1,000 万个计时周期。
        private static readonly long epoch = new DateTime(1790, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

        /// <summary>
        /// 当前本地时间Ms
        /// </summary>
        /// <returns></returns>
        public static long Now => Now_Ms();

        /// <summary>
        /// 获取当前本地时间毫秒
        /// </summary>
        /// <returns></returns>
        public static long Now_Ms()
        {
            return (DateTime.UtcNow.Ticks - epoch) / System.TimeSpan.TicksPerMillisecond;
        }

        /// <summary>
        /// 获取当前秒
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentTimeSeconds()
        {
            return MiniSecondToSecond_Floor(GetCurrentTimeSeconds());
        }

        /// <summary>
        /// 秒转换毫秒
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long SecondToMiniSeconds(long s)
        {
            return s * 1000;
        }

        /// <summary>
        /// 毫秒转秒（long类型）（向下取整）
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static long MiniSecondToSecond_Floor(long ms)
        {
            return ms / 1000;
        }

        /// <summary>
        /// 毫秒转秒（long类型）（向上取整）
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static long MiniSecondToSecond_Ceil(long ms)
        {
            long s = ms / 1000;
            long mod = ms % 1000;
            if (mod > 0)
                return s + 1;
            else
                return s;
        }

        /// <summary>
        /// 秒转小时（long类型）（向上取整）
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long SecondToHour_Ceil(long s)
        {
            long h = s / 3600;
            long mod = s % 3600;
            if (mod > 0)
            {
                return h + 1;
            }
            return h;
        }

        /// <summary>
        /// 毫秒转天，向上取整
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static int MiniSecondToDay_Ceil(long ms)
        {
            ms = MiniSecondToSecond_Ceil(ms);
            int day = (int)(ms / 86400);
            long mod = ms % 86400;
            if (mod > 0)
                return day + 1;
            else
                return day;
        }

        /// <summary>
        /// 毫秒转Span
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static TimeSpan MiniSecondToTimeSpan(long ms)
        {
            long s = MiniSecondToSecond_Ceil(ms);
            int day = (int)(s / 86400);
            int hour = (int)(day % 86400 / 3600);
            int min = (int)(s % 86400 % 3600 / 60);
            int sec = (int)(s % 86400 % 3600 % 60);
            TimeSpan timeSpan = new TimeSpan(day, hour, min, sec);
            return timeSpan;
        }

        /// <summary>
        /// 只返回显示最大时间单位的字符串，如2天，20秒等
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static string GetMaxTimeUnitString(long ms)
        {
            ms = MiniSecondToSecond_Ceil(ms);
            if (ms / 60 == 0)
            {
                return StringUtil.Concat(ms.ToTempString(), "秒");
            }

            ms = ms / 60;
            if (ms / 60 == 0)
            {
                return StringUtil.Concat(ms.ToTempString(), "分钟");
            }

            ms = ms / 60;
            if (ms / 24 == 0)
            {
                return StringUtil.Concat(ms.ToTempString(), "小时");
            }

            ms = ms / 24;
            return StringUtil.Concat(ms.ToTempString(), "天");
        }

        /// <summary>
        /// 获取时间格式 1小时1分钟
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStringHM(long seconds)
        {
            ShowTime showTime;
            GetSecondToTime(seconds, out showTime, ETimeCorrectToType.Minute);
            if (showTime.Hour > 0 && showTime.Minute > 0)
            {
                return StringUtil.Format("{0}{1}{2}{3}", showTime.Hour, "小时", showTime.Minute, "分钟");
            }
            else if (showTime.Hour > 0)
            {
                return StringUtil.Format("{0}{1}", showTime.Hour, "小时");
            }
            else if (showTime.Minute > 0)
            {
                return StringUtil.Format("{0}{1}", showTime.Minute, "分钟");
            }
            return "";
        }

        /// <summary>
        /// 根据秒数返回3天0时0分0秒的多语言
        /// 大于等于1天显示D天H小时，否则显示H:M:S
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetTimeStringDHMS(long seconds)
        {
            ShowTime showTime;
            GetSecondToTime(seconds, out showTime);
            string timeStr;
            if (showTime.Day > 0)
            {
                timeStr = GetTimeStringDH(showTime);
            }
            else
            {
                timeStr = GetTimeStringHMSFullFormat(showTime);
            }
            return timeStr;
        }

        /// <summary>
        /// 根据秒数返回00:00:00的多语言（超过天转换成小时）
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="hourZeroHide">不足1小时是否隐藏小时</param>
        /// <returns></returns>
        public static string GetTimeStringHMSFullFormat(long seconds, bool hourZeroHide = false)
        {
            ShowTime showTime;
            GetSecondToTime(seconds, out showTime);
            string timeStr = GetTimeStringHMSFullFormat(showTime, hourZeroHide);
            return timeStr;
        }

        /// <summary>
        /// 根据秒数返回00:00:00的多语言，不满小时隐藏小时
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetTimeStringMSFormat(long seconds)
        {
            ShowTime showTime;
            GetSecondToTime(seconds, out showTime);
            string timeStr = GetTimeStringHMSFullFormat(showTime, true);
            return timeStr;
        }

        /// <summary>
        /// 根据秒数返回3天0小时0分0秒的多语言
        /// 大于等于1天显示D天H小时，否则显示H:M:S
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static string GetTimeStringDHMS(System.TimeSpan ts)
        {
            ShowTime showTime;
            GetSecondToTime(ts, out showTime);
            string timeStr;
            if (showTime.Day > 0)
            {
                timeStr = GetTimeStringDH(showTime);
            }
            else
            {
                timeStr = GetTimeStringHMSFullFormat(showTime);
            }
            return timeStr;
        }

        /// <summary>
        /// 根据秒数返回3天0小时的多语言
        /// </summary>
        /// <param name="showTime"></param>
        /// <returns></returns>
        public static string GetTimeStringDH(ShowTime showTime)
        {
            string timeDay = VStringUtil.Concat(showTime.Day.ToTempString(), "天");
            string timeHour = VStringUtil.Concat(showTime.Hour.ToTempString(), "小时");
            return StringUtil.Concat(timeDay, timeHour);
        }

        /// <summary>
        /// 根据秒数返回3天0小时0分钟0秒的多语言，返回完整的时间格式
        /// </summary>
        /// <param name="showTime"></param>
        /// <param name="ShortMode"></param>
        /// <returns></returns>
        public static string GetTimeStringDHMSFullFormat(ShowTime showTime, bool ShortMode = false)
        {
            string timeDay = ShortMode && showTime.Day == 0
                ? string.Empty 
                : VStringUtil.Concat(showTime.Day.ToTempString(), "天");
            string timeHour = ShortMode && showTime.Day == 0 && showTime.Hour == 0
                ? string.Empty
                : VStringUtil.Concat(showTime.Hour.ToTempString(), "小时");
            string timeMinute = ShortMode && showTime.Day == 0 && showTime.Hour == 0 && showTime.Minute == 0
                ? string.Empty
                : VStringUtil.Concat(showTime.Minute.ToTempString(), "分钟");
            string timeSecond = 
                VStringUtil.Concat(showTime.Second.ToTempString(), "秒");

            return StringUtil.Concat(timeDay, timeHour, timeHour, timeSecond);
        }

        public static string GetTimeStringByMillSecond(long ms)
        {
            TimeSpan timeSpan = MiniSecondToTimeSpan(ms);
            string timeDay = VStringUtil.Concat(timeSpan.Days.ToTempString(), "天");
            string timeHour = VStringUtil.Concat(timeSpan.Hours.ToTempString(), "小时");
            string timeMinute = VStringUtil.Concat(timeSpan.Minutes.ToTempString(), "分");
            string timeSecond = VStringUtil.Concat(timeSpan.Seconds.ToTempString(), "秒");

            if (timeSpan.Days > 0)
                return StringUtil.Concat(timeDay, timeHour);
            if (timeSpan.Days <= 0 && timeSpan.Hours >= 0)
                return StringUtil.Concat(timeHour, timeMinute, timeSecond);
            return StringUtil.Concat(timeMinute, timeSecond);
        }

        /// <summary>
        /// 根据秒数返回00:00:00的多语言（超过1天转换成小时）
        /// </summary>
        /// <param name="showTime"></param>
        /// <param name="hourZeroHide">不足1小时是否隐藏小时</param>
        /// <returns></returns>
        public static string GetTimeStringHMSFullFormat(ShowTime showTime, bool hourZeroHide = false)
        {
            if (showTime.Day > 0)
            {
                //超过的天修改小时
                showTime.Hour += showTime.Day * 24;
            }

            int H1 = showTime.Hour / 10;
            int H2 = showTime.Hour % 10;
            int M1 = showTime.Minute / 10;
            int M2 = showTime.Minute / 10;
            int S1 = showTime.Second / 10;
            int S2 = showTime.Second / 10;
            if (H1 == 0 && H2 == 0 && hourZeroHide)
            {
                return StringUtil.Format("{0}{1}:{2}{3}", M1, M2, S1, S2);
            }

            return StringUtil.Format("{0}{1}:{2}{3}:{4}{5}", H1, H2, M1, M2, S1, S2);
        }

        /// <summary>
        /// 传入一个总秒数，返回一个显示时间的结构体
        /// </summary>
        /// <param name="second">总秒数</param>
        /// <param name="time">返回的时间结构体</param>
        /// <param name="timeType">保留的时间精度类型</param>
        public static void GetSecondToTime(long second, out ShowTime time, ETimeCorrectToType timeType = ETimeCorrectToType.Second)
        {
            System.TimeSpan ts = System.TimeSpan.FromSeconds(second);
            GetSecondToTime(ts, out time, timeType);
        }

        /// <summary>
        /// 传入一个总秒数，返回一个显示时间的结构体（向下取整）
        /// </summary>
        /// <param name="second">总秒数</param>
        /// <param name="time">返回的时间结构体</param>
        public static void GetFloorSecondToTime(long second, out ShowTime time)
        {
            System.TimeSpan ts = System.TimeSpan.FromSeconds(second);
            time = new ShowTime();
            time.Day = ts.Days;
            time.Hour = ts.Hours;
            time.Minute = ts.Minutes;
            time.Second = ts.Seconds;
        }

        /// <summary>
        /// 传入一个总秒数，返回一个显示时间的结构体
        /// </summary>
        /// <param name="ts">总秒数</param>
        /// <param name="time">返回的时间结构体</param>
        /// <param name="timeType">保留的时间精度类型</param>
        public static void GetSecondToTime(System.TimeSpan ts, out ShowTime time, ETimeCorrectToType timeType = ETimeCorrectToType.Second)
        {
            time = new ShowTime();
            time.Day = ts.Days;
            time.Hour = ts.Hours;
            time.Minute = ts.Minutes;
            time.Second = ts.Seconds;
            if (timeType == ETimeCorrectToType.Second)
            {
                if (time.Second <= 0)
                {
                    time.Second = 0;
                }
                else if (time.Second < 1)
                {
                    time.Second = 1;
                }
            }
            else if (timeType == ETimeCorrectToType.Minute)
            {
                if (time.Second > 0)
                {
                    time.Minute += 1;
                    if (time.Minute > 60)
                    {
                        time.Minute = 0;
                        time.Hour += 1;
                    }
                }
            }
            else if (timeType == ETimeCorrectToType.Hour)
            {
                if (time.Second > 0 || time.Minute > 0)
                {
                    time.Hour += 1;
                    if (time.Hour > 24)
                    {
                        time.Hour = 0;
                        time.Day += 1;
                    }
                }
            }
            else if (timeType == ETimeCorrectToType.Day)
            {
                if (time.Second > 0 || time.Minute > 0 || time.Hour > 0)
                {
                    time.Day += 1;
                }
            }
        }

        /// <summary>
        /// 输出YMDHMS格式 2023.2.20 15：39
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetTimeStringYMDHMS(long seconds)
        {
            DateTime dateTime = new DateTime(seconds);
            string timeStr = StringUtil.Concat(
                GetTimeStringYMD(seconds), " ",
                GetTimeStringHMSFullFormat(seconds)
                );
            return timeStr;
        }

        /// <summary>
        /// 输出YMD格式 2023.2.20
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetTimeStringYMD(long seconds)
        {
            DateTime dateTime = new DateTime(seconds);
            string timeStr = StringUtil.Concat(
                dateTime.Year.ToTempString(), ".",
                dateTime.Month.ToTempString(), ".",
                dateTime.Day.ToTempString()
                );
            return timeStr;
        }

        /// <summary>
        /// 秒转天
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static long SecondsConvertToDays(long seconds)
        {
            if (seconds < 0)
            {
                return 0;
            }
            return seconds / 86400;
        }
    }
}