﻿using HB.Framework.Common;
using HB.Framework.Common.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace HB.Framework.Database.Entity
{
    /// <summary>
    /// 单例
    /// </summary>
    internal class DefaultDatabaseEntityMapper : IDatabaseEntityMapper
    {
        private readonly IDatabaseEntityDefFactory _modelDefFactory;

        public DefaultDatabaseEntityMapper(IDatabaseEntityDefFactory modelDefFactory)
        {
            _modelDefFactory = modelDefFactory;
        }

        #region 表行与实体间映射

        public IList<T> ToList<T>(IDataReader reader)
            where T : DatabaseEntity, new()
        {
            IList<T> lst = new List<T>();

            if (reader == null)
            {
                return lst;
            }

            int len = reader.FieldCount;
            string[] propertyNames = new string[len];


            DatabaseEntityDef definition = _modelDefFactory.GetDef<T>();

            for (int i = 0; i < len; ++i)
            {
                propertyNames[i] = reader.GetName(i);
            }

            while (reader.Read())
            {
                T item = new T();

                for (int i = 0; i < len; ++i)
                {
                    DatabaseEntityPropertyDef property = definition.GetProperty(propertyNames[i]);
                    object fieldValue = reader[i];

                    if (property.PropertyName == "Id" && fieldValue == DBNull.Value)
                    {
                        item = null;
                        break;
                    }

                    object value = property.TypeConverter == null ?
                        ValueConverter.DbValueToTypeValue(property.PropertyType, fieldValue) :
                        property.TypeConverter.DbValueToTypeValue(fieldValue);

                    property.SetValue(item, value);
                }

                if (item != null && !item.Deleted)
                {
                    lst.Add(item);
                }
            }

            return lst;
        }

        public IList<Tuple<TSource, TTarget>> ToList<TSource, TTarget>(IDataReader reader)
            where TSource : DatabaseEntity, new()
            where TTarget : DatabaseEntity, new()
        {
            IList<Tuple<TSource, TTarget>> lst = new List<Tuple<TSource, TTarget>>();

            if (reader == null)
            {
                return lst;
            }

            DatabaseEntityDef definition1 = _modelDefFactory.GetDef<TSource>();
            DatabaseEntityDef definition2 = _modelDefFactory.GetDef<TTarget>();

            string[] propertyNames1 = new string[definition1.FieldCount];
            string[] propertyNames2 = new string[definition2.FieldCount];

            int j = 0;

            for (int i = 0; i < definition1.FieldCount; ++j, ++i)
            {
                propertyNames1[i] = reader.GetName(j);
            }

            for (int i = 0; i < definition2.FieldCount; ++j, ++i)
            {
                propertyNames2[i] = reader.GetName(j);
            }

            while (reader.Read())
            {
                TSource t1 = new TSource();
                TTarget t2 = new TTarget();

                j = 0;

                for (int i = 0; i < definition1.FieldCount; ++i, ++j)
                {
                    DatabaseEntityPropertyDef pDef = definition1.GetProperty(propertyNames1[i]);
                    object fieldValue = reader[j];

                    if (pDef.PropertyName == "Id" && fieldValue == DBNull.Value)
                    {
                        t1 = null;
                        break;
                    }

                    if (pDef != null)
                    {
                        object value = pDef.TypeConverter == null ?
                            ValueConverter.DbValueToTypeValue(pDef.PropertyType, fieldValue) :
                            pDef.TypeConverter.DbValueToTypeValue(fieldValue);

                        pDef.SetValue(t1, value);
                    }
                }

                for (int i = 0; i < definition2.FieldCount; ++i, ++j)
                {
                    DatabaseEntityPropertyDef pDef = definition2.GetProperty(propertyNames2[i]);
                    object fieldValue = reader[j];

                    if (pDef.PropertyName == "Id" && fieldValue == DBNull.Value)
                    {
                        t2 = null;
                        break;
                    }

                    if (pDef != null)
                    {
                        object value = pDef.TypeConverter == null ?
                            ValueConverter.DbValueToTypeValue(pDef.PropertyType, fieldValue) :
                            pDef.TypeConverter.DbValueToTypeValue(fieldValue);

                        pDef.SetValue(t2, value);
                    }
                }

                if (t1 != null && t1.Deleted)
                {
                    t1 = null;
                }
                if (t2 != null && t2.Deleted)
                {
                    t2 = null;
                }

                //删除全为空
                if (t1 != null || t2 != null)
                {
                    lst.Add(new Tuple<TSource, TTarget>(t1, t2));
                }
            }

            return lst;
        }

        public IList<Tuple<TSource, TTarget2, TTarget3>> ToList<TSource, TTarget2, TTarget3>(IDataReader reader)
            where TSource : DatabaseEntity, new()
            where TTarget2 : DatabaseEntity, new()
            where TTarget3 : DatabaseEntity, new()
        {
            IList<Tuple<TSource, TTarget2, TTarget3>> lst = new List<Tuple<TSource, TTarget2, TTarget3>>();

            if (reader == null)
            {
                return lst;
            }

            DatabaseEntityDef definition1 = _modelDefFactory.GetDef<TSource>();
            DatabaseEntityDef definition2 = _modelDefFactory.GetDef<TTarget2>();
            DatabaseEntityDef definition3 = _modelDefFactory.GetDef<TTarget3>();

            string[] propertyNames1 = new string[definition1.FieldCount];
            string[] propertyNames2 = new string[definition2.FieldCount];
            string[] propertyNames3 = new string[definition3.FieldCount];

            int j = 0;

            for (int i = 0; i < definition1.FieldCount; ++i, ++j)
            {
                propertyNames1[i] = reader.GetName(j);
            }

            for (int i = 0; i < definition2.FieldCount; ++i, ++j)
            {
                propertyNames2[i] = reader.GetName(j);
            }

            for (int i = 0; i < definition3.FieldCount; ++i, ++j)
            {
                propertyNames3[i] = reader.GetName(j);
            }

            while (reader.Read())
            {
                TSource t1 = new TSource();
                TTarget2 t2 = new TTarget2();
                TTarget3 t3 = new TTarget3();

                j = 0;

                for (int i = 0; i < definition1.FieldCount; ++i, ++j)
                {
                    DatabaseEntityPropertyDef pDef = definition1.GetProperty(propertyNames1[i]);
                    object fieldValue = reader[j];

                    if (pDef.PropertyName == "Id" && fieldValue == DBNull.Value)
                    {
                        t1 = null;
                        break;
                    }

                    if (pDef != null)
                    {
                        object value = pDef.TypeConverter == null ?
                            ValueConverter.DbValueToTypeValue(pDef.PropertyType, fieldValue) :
                            pDef.TypeConverter.DbValueToTypeValue(fieldValue);

                        pDef.SetValue(t1, value);
                    }
                }

                for (int i = 0; i < definition2.FieldCount; ++i, ++j)
                {
                    DatabaseEntityPropertyDef pDef = definition2.GetProperty(propertyNames2[i]);
                    object fieldValue = reader[j];

                    if (pDef.PropertyName == "Id" && fieldValue == DBNull.Value)
                    {
                        t2 = null;
                        break;
                    }

                    if (pDef != null)
                    {
                        object value = pDef.TypeConverter == null ?
                            ValueConverter.DbValueToTypeValue(pDef.PropertyType, fieldValue) :
                            pDef.TypeConverter.DbValueToTypeValue(fieldValue);

                        pDef.SetValue(t2, value);
                    }
                }

                for (int i = 0; i < definition3.FieldCount; ++i, ++j)
                {
                    DatabaseEntityPropertyDef pDef = definition3.GetProperty(propertyNames3[i]);
                    object fieldValue = reader[j];

                    if (pDef.PropertyName == "Id" && fieldValue == DBNull.Value)
                    {
                        t3 = null;
                        break;
                    }

                    if (pDef != null)
                    {
                        object value = pDef.TypeConverter == null ?
                            ValueConverter.DbValueToTypeValue(pDef.PropertyType, fieldValue) :
                            pDef.TypeConverter.DbValueToTypeValue(fieldValue);

                        pDef.SetValue(t3, value);
                    }
                }

                if (t1 != null && t1.Deleted)
                {
                    t1 = null;
                }

                if (t2 != null && t2.Deleted)
                {
                    t2 = null;
                }

                if (t3 != null && t3.Deleted)
                {
                    t3 = null;
                }

                if (t1 != null || t2 != null || t3 != null)
                {
                    lst.Add(new Tuple<TSource, TTarget2, TTarget3>(t1, t2, t3));
                }
            }

            return lst;
        }

        public void ToObject<T>(IDataReader reader, T item) where T : DatabaseEntity, new()
        {
            if (reader == null)
            {
                return;
            }

            int len = reader.FieldCount;
            string[] propertyNames = new string[len];

            DatabaseEntityDef definition = _modelDefFactory.GetDef<T>();

            for (int i = 0; i < len; ++i)
            {
                propertyNames[i] = reader.GetName(i);
            }

            if (reader.Read())
            {
                for (int i = 0; i < len; ++i)
                {
                    DatabaseEntityPropertyDef property = definition.GetProperty(propertyNames[i]);

                    object value = property.TypeConverter == null ?
                        ValueConverter.DbValueToTypeValue(property.PropertyType, reader[i]) :
                        property.TypeConverter.DbValueToTypeValue(reader[i]);

                    property.SetValue(item, value);
                }
            }
        }

        #endregion        
    }
}
