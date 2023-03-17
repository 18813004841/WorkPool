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
        /// ʱ�侫ȷ����
        /// </summary>
        public enum ETimeCorrectToType
        {
            Second,
            Minute,
            Hour,
            Day,
        }

        //MSDN�ĵ��Ľ���:https://docs.microsoft.com/zh-cn/dotnet/api/system.datetime.ticks?redirectedfrom=MSDN&view=netframework-4.8
        //һ����ʱ���ڱ�ʾһ�����룬��һǧ���֮һ�롣 1 �������� 10,000 ����ʱ���ڣ��� 1 ������ 1,000 �����ʱ���ڡ�
        private static readonly long epoch = new DateTime(1790, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

        /// <summary>
        /// ��ǰ����ʱ��Ms
        /// </summary>
        /// <returns></returns>
        public static long Now => Now_Ms();

        /// <summary>
        /// ��ȡ��ǰ����ʱ�����
        /// </summary>
        /// <returns></returns>
        public static long Now_Ms()
        {
            return (DateTime.UtcNow.Ticks - epoch) / System.TimeSpan.TicksPerMillisecond;
        }

        /// <summary>
        /// ��ȡ��ǰ��
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentTimeSeconds()
        {
            return MiniSecondToSecond_Floor(GetCurrentTimeSeconds());
        }

        /// <summary>
        /// ��ת������
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long SecondToMiniSeconds(long s)
        {
            return s * 1000;
        }

        /// <summary>
        /// ����ת�루long���ͣ�������ȡ����
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static long MiniSecondToSecond_Floor(long ms)
        {
            return ms / 1000;
        }

        /// <summary>
        /// ����ת�루long���ͣ�������ȡ����
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
        /// ��תСʱ��long���ͣ�������ȡ����
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
        /// ����ת�죬����ȡ��
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
        /// ����תSpan
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
        /// ֻ������ʾ���ʱ�䵥λ���ַ�������2�죬20���
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static string GetMaxTimeUnitString(long ms)
        {
            ms = MiniSecondToSecond_Ceil(ms);
            if (ms / 60 == 0)
            {
                return StringUtil.Concat(ms.ToTempString(), "��");
            }

            ms = ms / 60;
            if (ms / 60 == 0)
            {
                return StringUtil.Concat(ms.ToTempString(), "����");
            }

            ms = ms / 60;
            if (ms / 24 == 0)
            {
                return StringUtil.Concat(ms.ToTempString(), "Сʱ");
            }

            ms = ms / 24;
            return StringUtil.Concat(ms.ToTempString(), "��");
        }

        /// <summary>
        /// ��ȡʱ���ʽ 1Сʱ1����
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStringHM(long seconds)
        {
            ShowTime showTime;
            GetSecondToTime(seconds, out showTime, ETimeCorrectToType.Minute);
            if (showTime.Hour > 0 && showTime.Minute > 0)
            {
                return StringUtil.Format("{0}{1}{2}{3}", showTime.Hour, "Сʱ", showTime.Minute, "����");
            }
            else if (showTime.Hour > 0)
            {
                return StringUtil.Format("{0}{1}", showTime.Hour, "Сʱ");
            }
            else if (showTime.Minute > 0)
            {
                return StringUtil.Format("{0}{1}", showTime.Minute, "����");
            }
            return "";
        }

        /// <summary>
        /// ������������3��0ʱ0��0��Ķ�����
        /// ���ڵ���1����ʾD��HСʱ��������ʾH:M:S
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
        /// ������������00:00:00�Ķ����ԣ�������ת����Сʱ��
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="hourZeroHide">����1Сʱ�Ƿ�����Сʱ</param>
        /// <returns></returns>
        public static string GetTimeStringHMSFullFormat(long seconds, bool hourZeroHide = false)
        {
            ShowTime showTime;
            GetSecondToTime(seconds, out showTime);
            string timeStr = GetTimeStringHMSFullFormat(showTime, hourZeroHide);
            return timeStr;
        }

        /// <summary>
        /// ������������00:00:00�Ķ����ԣ�����Сʱ����Сʱ
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
        /// ������������3��0Сʱ0��0��Ķ�����
        /// ���ڵ���1����ʾD��HСʱ��������ʾH:M:S
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
        /// ������������3��0Сʱ�Ķ�����
        /// </summary>
        /// <param name="showTime"></param>
        /// <returns></returns>
        public static string GetTimeStringDH(ShowTime showTime)
        {
            string timeDay = VStringUtil.Concat(showTime.Day.ToTempString(), "��");
            string timeHour = VStringUtil.Concat(showTime.Hour.ToTempString(), "Сʱ");
            return StringUtil.Concat(timeDay, timeHour);
        }

        /// <summary>
        /// ������������3��0Сʱ0����0��Ķ����ԣ�����������ʱ���ʽ
        /// </summary>
        /// <param name="showTime"></param>
        /// <param name="ShortMode"></param>
        /// <returns></returns>
        public static string GetTimeStringDHMSFullFormat(ShowTime showTime, bool ShortMode = false)
        {
            string timeDay = ShortMode && showTime.Day == 0
                ? string.Empty 
                : VStringUtil.Concat(showTime.Day.ToTempString(), "��");
            string timeHour = ShortMode && showTime.Day == 0 && showTime.Hour == 0
                ? string.Empty
                : VStringUtil.Concat(showTime.Hour.ToTempString(), "Сʱ");
            string timeMinute = ShortMode && showTime.Day == 0 && showTime.Hour == 0 && showTime.Minute == 0
                ? string.Empty
                : VStringUtil.Concat(showTime.Minute.ToTempString(), "����");
            string timeSecond = 
                VStringUtil.Concat(showTime.Second.ToTempString(), "��");

            return StringUtil.Concat(timeDay, timeHour, timeHour, timeSecond);
        }

        public static string GetTimeStringByMillSecond(long ms)
        {
            TimeSpan timeSpan = MiniSecondToTimeSpan(ms);
            string timeDay = VStringUtil.Concat(timeSpan.Days.ToTempString(), "��");
            string timeHour = VStringUtil.Concat(timeSpan.Hours.ToTempString(), "Сʱ");
            string timeMinute = VStringUtil.Concat(timeSpan.Minutes.ToTempString(), "��");
            string timeSecond = VStringUtil.Concat(timeSpan.Seconds.ToTempString(), "��");

            if (timeSpan.Days > 0)
                return StringUtil.Concat(timeDay, timeHour);
            if (timeSpan.Days <= 0 && timeSpan.Hours >= 0)
                return StringUtil.Concat(timeHour, timeMinute, timeSecond);
            return StringUtil.Concat(timeMinute, timeSecond);
        }

        /// <summary>
        /// ������������00:00:00�Ķ����ԣ�����1��ת����Сʱ��
        /// </summary>
        /// <param name="showTime"></param>
        /// <param name="hourZeroHide">����1Сʱ�Ƿ�����Сʱ</param>
        /// <returns></returns>
        public static string GetTimeStringHMSFullFormat(ShowTime showTime, bool hourZeroHide = false)
        {
            if (showTime.Day > 0)
            {
                //���������޸�Сʱ
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
        /// ����һ��������������һ����ʾʱ��Ľṹ��
        /// </summary>
        /// <param name="second">������</param>
        /// <param name="time">���ص�ʱ��ṹ��</param>
        /// <param name="timeType">������ʱ�侫������</param>
        public static void GetSecondToTime(long second, out ShowTime time, ETimeCorrectToType timeType = ETimeCorrectToType.Second)
        {
            System.TimeSpan ts = System.TimeSpan.FromSeconds(second);
            GetSecondToTime(ts, out time, timeType);
        }

        /// <summary>
        /// ����һ��������������һ����ʾʱ��Ľṹ�壨����ȡ����
        /// </summary>
        /// <param name="second">������</param>
        /// <param name="time">���ص�ʱ��ṹ��</param>
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
        /// ����һ��������������һ����ʾʱ��Ľṹ��
        /// </summary>
        /// <param name="ts">������</param>
        /// <param name="time">���ص�ʱ��ṹ��</param>
        /// <param name="timeType">������ʱ�侫������</param>
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
        /// ���YMDHMS��ʽ 2023.2.20 15��39
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
        /// ���YMD��ʽ 2023.2.20
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
        /// ��ת��
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