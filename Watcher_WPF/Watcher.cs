using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Watcher_WPF
{
    class Watcher : FileSystemWatcher
    {
        public bool NotifyFilter_Size;
        public bool NotifyFilter_FileName;
        public bool NotifyFilter_DirectoryName;

        public Watcher(bool NotifyFilter_Size, bool NotifyFilter_FileName, bool NotifyFilter_DirectoryName)
        {
            this.NotifyFilter_Size = NotifyFilter_Size;
            this.NotifyFilter_FileName = NotifyFilter_FileName;
            this.NotifyFilter_DirectoryName = NotifyFilter_DirectoryName;

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
                    NotifyFilter = GetFilter(NotifyFilter_Size, NotifyFilter_FileName, NotifyFilter_DirectoryName);
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

        public static NotifyFilters GetFilter(bool NotifyFilter_Size, bool NotifyFilter_FileName, bool NotifyFilter_DirectoryName)
        {
            NotifyFilters nf = new NotifyFilters();

            if (NotifyFilter_Size)
                nf |= NotifyFilters.Size;

            if (NotifyFilter_FileName)
                nf |= NotifyFilters.FileName;

            if (NotifyFilter_DirectoryName)
                nf |= NotifyFilters.DirectoryName;

            return nf;
        }
    }
}
