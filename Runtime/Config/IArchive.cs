using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Zx.Config
{
    /// <summary>
    /// 存档类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class IArchive<T> : ICloneable<T>, ISerializable where T : new()
    {
        public T Clone()
        {
            using var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return (T) formatter.Deserialize(stream);
        }

        public void SerializeBinary(string path)
        {
            using var stream = File.Exists(path) ? new FileStream(path, FileMode.Open) : File.Create(path);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }

        public T DeserializeBinary(string path)
        {
            // 文件不存在直接异常, SaveManager 会写入一个默认存档
            using var stream = new FileStream(path, FileMode.Open);
            stream.Position = 0;
            var formatter = new BinaryFormatter();
            return (T) formatter.Deserialize(stream);
        }

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);

        protected void AddValue<U>(SerializationInfo info, string name, ref U field)
        {
            try
            {
                info.AddValue(name, field, typeof(U));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }


        protected void AddValue<U, V>(SerializationInfo info, string name, ref Dictionary<U, V> dict)
        {
            try
            {
                var field = dict.Select(x => Tuple.Create(x.Key, x.Value)).ToList();
                info.AddValue(name, field, typeof(List<Tuple<U, V>>));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        protected void GetValue<U>(SerializationInfo info, string name, ref U field) where U : new()
        {
            try
            {
                field = (U) info.GetValue(name, typeof(U));
                // UnityEngine.Debug.Log($"成功读取字段 {name}");
            }
            catch (SerializationException)
            {
                // 找不到字段, 可能是新增的或者改了结构
                // 直接删了重新创建
                field = new U();
                // UnityEngine.Debug.Log($"无法读取字段 {name}");
            }
        }

        protected void GetValue<U, V>(SerializationInfo info, string name, ref Dictionary<U, V> dict) where U : new()
        {
            try
            {
                var field = (List<Tuple<U, V>>) info.GetValue(name, typeof(List<Tuple<U, V>>));
                dict = field.ToDictionary(x => x.Item1, x => x.Item2);
            }
            catch (SerializationException)
            {
                dict = new Dictionary<U, V>();
            }
        }
    }
}