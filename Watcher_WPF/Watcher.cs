using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Watcher_WPF
{

    /// <summary>
    /// NotifyFilters
    /// </summary>
    public struct NFilters
    {
        public bool Size { get; set; }
        public bool FileName { get; set; }
        public bool DirectoryName { get; set; }

        public NFilters(bool size, bool fileName, bool directoryName)
        {
            Size = size;
            FileName = fileName;
            DirectoryName = directoryName;
        }
    }

    class Watcher : FileSystemWatcher
    {
        public NFilters NFilters { get; set; }

        public Watcher(NFilters filter) : base()
        {
            NFilters = filter;
            IncludeSubdirectories = true;
        }

        public new string Path
        {
            get => base.Path;

            set
            {
                string path = value.Replace('/', '\\');
                base.Path = path + (path.EndsWith("\\") ? null : "\\");
            }
        }

        public new NotifyFilters NotifyFilter
        {
            get => base.NotifyFilter;

            private set
            {
                base.NotifyFilter = value;
            }
        }

        public new bool EnableRaisingEvents
        {
            get => base.EnableRaisingEvents;

            set
            {
                if (value)
                {
                    NotifyFilter = GetFilter(NFilters);
                }
                base.EnableRaisingEvents = value;
            }
        }

        public bool IsStarted
        {
            get => EnableRaisingEvents;

            set
            {
                EnableRaisingEvents = value;
            }
        }

        public static NotifyFilters GetFilter(NFilters f)
        {
            NotifyFilters nf = new NotifyFilters();

            if (f.Size)
                nf |= NotifyFilters.Size;

            if (f.FileName)
                nf |= NotifyFilters.FileName;

            if (f.DirectoryName)
                nf |= NotifyFilters.DirectoryName;

            return nf;
        }
    }
}
