using System.IO;

namespace Watcher_WPF
{
    class WWatcher
    {
        private readonly FileSystemWatcher watcher;

        public bool IsStarted
        {
            get
            {
                return watcher.EnableRaisingEvents;
            }
            set
            {
                watcher.EnableRaisingEvents = value;
            }
        }

        public string Path
        {
            get
            {
                return watcher.Path;
            }
            set
            {
                string path = value.Replace('/', '\\');
                watcher.Path = path + (path.EndsWith("\\") ? null : "\\");
            }
        }

        public WWatcher()
        {
            watcher = new FileSystemWatcher()
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.DirectoryName,
                IncludeSubdirectories = true
            };
        }

        public WWatcher(string path) : this()
        {
            Path = path;
        }

        ~WWatcher()
        {
            watcher.Dispose();
        }

        public void AddCreatedEvent(FileSystemEventHandler e)
        {
            watcher.Created += e;
        }

        public void RemoveCreatedEvent(FileSystemEventHandler e)
        {
            watcher.Created -= e;
        }

        public void AddChangedEvent(FileSystemEventHandler e)
        {
            watcher.Changed += e;
        }

        public void RemoveChangedEvent(FileSystemEventHandler e)
        {
            watcher.Changed -= e;
        }

        public void AddDeletedEvent(FileSystemEventHandler e)
        {
            watcher.Deleted += e;
        }

        public void RemoveDeletedEvent(FileSystemEventHandler e)
        {
            watcher.Deleted -= e;
        }

        public void AddRenamedEvent(RenamedEventHandler e)
        {
            watcher.Renamed += e;
        }

        public void RemoveRenamedEvent(RenamedEventHandler e)
        {
            watcher.Renamed -= e;
        }
    }
}
