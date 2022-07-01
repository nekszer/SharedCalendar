using SharedCalendar.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharedCalendar.API.Helpers
{
    public static class CastHelper
    {

        public static IEnumerable<T> CastList<T>(this IEnumerable<Row> rows)
        {
            var list = new List<T>(rows.Count());
            foreach (var row in rows)
                list.Add(row.Cast<T>());
            return list;
        }

        public static T Cast<T>(this Row row)
        {
            var type = typeof(T);
            var instance = Activator.CreateInstance(type);
            var properties = type.GetTypeInfo().DeclaredProperties;
            foreach (var property in properties)
            {
                ColumnProperty columnProperty = null;
                try
                {
                    columnProperty = property.GetCustomAttribute<ColumnProperty>();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex);
                }
                if (columnProperty == null) continue;
                string propertyname = columnProperty.Name;
                if (!row.ContainsKey(propertyname)) continue;
                var value = row[propertyname];
                try
                {
                    if (columnProperty.Convert != null)
                    {
                        var converter = (IValueConverter)Activator.CreateInstance(columnProperty.Convert);
                        property.SetValue(instance, converter.Convert(value));
                    }
                    else
                    {
                        var tvalue = Convert.ChangeType(value, property.PropertyType);
                        property.SetValue(instance, tvalue);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            return (T)instance;
        }

        public static Dictionary<string, object> AsDictionary<T>(this T data)
        {
            var dictionary = new Dictionary<string, object>();
            var properties = typeof(T).GetTypeInfo().DeclaredProperties;
            foreach (var property in properties)
            {
                try
                {
                    var key = property.Name;
                    var value = property.GetValue(data);
                    if (value == null || string.IsNullOrEmpty(value.ToString())) continue;
                    dictionary.Add(key, value);
                }
                catch { }
            }
            return dictionary;
        }
    }
}