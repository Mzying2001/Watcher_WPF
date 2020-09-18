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
        public bool Filter_Size;
        public bool Filter_FileName;
        public bool Filter_DirectoryName;

        public Watcher(bool Filter_Size, bool Filter_FileName, bool Filter_DirectoryName)
        {
            this.Filter_Size = Filter_Size;
            this.Filter_FileName = Filter_FileName;
            this.Filter_DirectoryName = Filter_DirectoryName;

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
                    NotifyFilter = GetFilter(Filter_Size, Filter_FileName, Filter_DirectoryName);
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

        public static NotifyFilters GetFilter(bool Filter_Size, bool Filter_FileName, bool Filter_DirectoryName)
        {
            NotifyFilters nf = new NotifyFilters();

            if (Filter_Size)
                nf |= NotifyFilters.Size;

            if (Filter_FileName)
                nf |= NotifyFilters.FileName;

            if (Filter_DirectoryName)
                nf |= NotifyFilters.DirectoryName;

            return nf;
        }
    }
}
