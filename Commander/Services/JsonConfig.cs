using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Commander.Services
{
    /// <summary>
    /// json格式配置文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonConfig<T> : IDisposable where T : new()
    {
        T _data;
        FileInfo _jsonFile;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName">配置文件名称（不需要后缀）</param>
        public JsonConfig(string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = typeof(T).FullName.ToLower();
            }

            _jsonFile = new FileInfo(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName + ".json"));

            if (!_jsonFile.Directory.Exists)
            {
                try
                {
                    _jsonFile.Directory.Create();
                }
                catch
                {
                    //可能没有权限
                }
            }
        }

        /// <summary>
        /// 配置数据
        /// </summary>
        public T Data
        {
            get
            {
                if (_data == null && _jsonFile.Exists)
                {
                    try
                    {
                        _data = JsonConvert.DeserializeObject<T>(File.ReadAllText(_jsonFile.FullName));
                    }
                    catch
                    {
                        _jsonFile.Delete();
                    }
                }

                if (_data == null)
                {
                    _data = new T();
                }

                return _data;
            }
        }

        public void Dispose()
        {
            SaveChanges();
        }

        public void Refresh()
        {
            _data = default(T);
        }

        /// <summary>
        /// 保存配置数据
        /// </summary>
        public void SaveChanges()
        {
            if (_data != null)
            {
                //由于设置了文件共享模式为允许随后写入，所以即使多个线程同时写入文件，也会等待之前的线程写入结束之后再执行，而不会出现错误
                using (var fs = new FileStream(_jsonFile.FullName, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    fs.Seek(0, SeekOrigin.End);

                    var jsonBytes = Encoding.UTF8.GetBytes(
                        JsonConvert.SerializeObject(_data, Formatting.Indented));
                    fs.Write(jsonBytes, 0, jsonBytes.Length);
                }
            }
        }
    }
}
