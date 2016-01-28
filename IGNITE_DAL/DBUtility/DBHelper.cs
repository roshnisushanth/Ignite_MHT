using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Text;
using System.Data.OleDb;
namespace IGNITE.DBUtility
{
    public class DBHelper
    {
        public static String getString(SqlDataReader sqlDR, string sColumnName)
        {
            try
            {
                return sqlDR[sColumnName] == DBNull.Value ? string.Empty : ((string)sqlDR[sColumnName]).Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static String getString(SqlDataReader sqlDR, int iColumnIndex)
        {
            try
            {
                return sqlDR[iColumnIndex] == DBNull.Value ? string.Empty : ((string)sqlDR[iColumnIndex]).Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static String getString(String name, DataRow row)
        {
            return row[name] != DBNull.Value ? (string)row[name] : string.Empty;
        }

        public static String getString(Object data)
        {
            return data != DBNull.Value ? ((string)data).Trim() : string.Empty;
        }

        public static String getString(DataRow row, string name)
        {
            try
            {
                return row[name] != DBNull.Value ? ((string)row[name]).Trim() : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool getBool(Object data)
        {
            return data != DBNull.Value ? (bool)data : false;
        }

        public static bool getBool(DataRow row, string name)
        {
            try
            {
                return row[name] != DBNull.Value ? (bool)row[name] : false;
            }
            catch
            {
                return false;
            }
        }
        public static bool getBool(SqlDataReader sqlDR, string sColumnName)
        {
            try
            {

                return sqlDR[sColumnName] != DBNull.Value ? (bool)sqlDR[sColumnName] : false;
            }
            catch
            {
                return false;
            }
        }

        public static Guid getGuid(Object data)
        {
            return data != DBNull.Value ? (Guid)data : new Guid();
        }

        public static Guid getGuid(DataRow row, string name)
        {
            try
            {
                return row[name] != DBNull.Value ? (Guid)row[name] : new Guid();
            }
            catch
            {
                return new Guid();
            }
        }

        public static int getInt(SqlDataReader sqlDR, string sColumnName)
        {
            try
            {
                return sqlDR[sColumnName] == DBNull.Value ? 0 : (int)sqlDR[sColumnName];
            }
            catch
            {
                return 0;
            }
        }
        public static short getInt16(SqlDataReader sqlDR, string sColumnName)
        {
            try
            {
                return sqlDR[sColumnName] == DBNull.Value ? (short)0 : (short)sqlDR[sColumnName];
            }
            catch
            {
                return (short)0;
            }
        }
        public static long getlong(SqlDataReader sqlDR, string sColumnName)
        {
            try
            {
                return sqlDR[sColumnName] == DBNull.Value ? 0 : (long)sqlDR[sColumnName];
            }
            catch
            {
                return 0;
            }
        }
        public static int getInt(SqlDataReader sqlDR, int iColumnIndex)
        {
            try
            {
                return sqlDR[iColumnIndex] == DBNull.Value ? 0 : (int)sqlDR[iColumnIndex];
            }
            catch
            {
                return 0;
            }
        }

        public static int getInt(Object data)
        {
            return data != DBNull.Value ? (int)data : 0;
        }

        public static int getInt(DataRow row, string name)
        {
            try
            {
                return (int)row[name];
            }
            catch
            {
                return 0;
            }
        }

        public static float getFloat(Object data)
        {
            return data != DBNull.Value ? (float)data : 0;
        }

        public static float getFloat(DataRow row, string name)
        {
            try
            {
                return row[name] != DBNull.Value ? (float)row[name] : 0;
            }
            catch
            {
                return 0;
            }
        }
        public static float getFloat(SqlDataReader row, string name)
        {
            try
            {
                return row[name] != DBNull.Value ? (float)row[name] : 0;
            }
            catch
            {
                return 0;
            }
        }
        public static DateTime getDateTime(Object data)
        {
            return data != DBNull.Value ? (DateTime)data : new DateTime();
        }

        public static DateTime getDateTime(DataRow row, string name)
        {
            try
            {
                return row[name] != DBNull.Value ? (DateTime)row[name] : new DateTime();
            }
            catch
            {
                return new DateTime();
            }
        }

        public static DateTime getDateTime(SqlDataReader sqlDR, string sColumnName)
        {
            try
            {
                return sqlDR[sColumnName] == DBNull.Value ? new DateTime() : (DateTime)sqlDR[sColumnName];

            }
            catch
            {
                return new DateTime();
            }
        }



        public static decimal getDecimal(Object data)
        {
            return data != DBNull.Value ? (decimal)data : 0;
        }
        public static decimal getDecimal(SqlDataReader sqlDR, string sColumnName)
        {
            try
            {
                return sqlDR[sColumnName] == DBNull.Value ? 0 : (decimal)sqlDR[sColumnName];
            }
            catch
            {
                return 0;
            }
        }

        public static double getDouble(Object data)
        {
            return data != DBNull.Value ? (double)data : 0.00;
        }

        public static double getDouble(DataRow row, string name)
        {
            try
            {
                return row[name] != DBNull.Value ? (double)row[name] : 0;
            }
            catch
            {
                return 0;
            }
        }

        public static double getDouble(SqlDataReader sqlDR, string sColumnName)
        {
            try
            {
                return sqlDR[sColumnName] == DBNull.Value ? 0 : (double)sqlDR[sColumnName];
            }
            catch
            {
                return 0;
            }
        }

        public static Single getSingle(Object data)
        {
            return data != DBNull.Value ? (Single)data : 0;
        }
        public static Int64 getInt64(Object data)
        {
            return data != DBNull.Value ? (Int64)data : 0;
        }
        public static Int64 getInt64(SqlDataReader sqlDR, string sColumnName)
        {
            try
            {
                return sqlDR[sColumnName] == DBNull.Value ? 0 : (Int64)sqlDR[sColumnName];
            }
            catch
            {
                return 0;
            }
        }
    }
}
