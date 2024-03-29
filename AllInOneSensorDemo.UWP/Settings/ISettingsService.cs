﻿using Windows.Foundation.Collections;

namespace AllInOneSensorDemo.UWP.Settings
{
    public interface ISettingsService
    {
        IPropertyMapping Converters { get; set; }
        IPropertySet Values { get; }
        bool Exists(string key);
        T Read<T>(string key, T fallback = default(T));
        void Remove(string key);
        void Write<T>(string key, T value);
        ISettingsService Open(string folderName, bool createFolderIsNotExists = true);
        void Clear(bool deleteSubContainers = true);
    }
}