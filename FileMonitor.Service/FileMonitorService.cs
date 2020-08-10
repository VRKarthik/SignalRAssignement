using System;
using System.IO;

namespace FileMonitor.Service
{
	public class FileMonitorService : IFileMonitorService
	{
		#region Global declarations

		private readonly DirectoryHelper _helper;

		#endregion

		#region Events

		public event EventHandler ContentAppendedEvent;

		public event EventHandler FileCreatedEvent;

		public event EventHandler FileRenamedEvent;

		public event EventHandler FileDeletedEvent;

		#endregion

		#region Constructor

		public FileMonitorService(string directoryPath)
		{
			_helper = new DirectoryHelper(directoryPath);
		}

		#endregion

		#region IFileMonitorService implementation

		public void Append(string contents)
		{
			_helper.AppendContents(contents);
		}

		public void CreateFile(string fileName)
		{
			_helper.CreateFile(fileName);
		}

		public void RenameFile(string fileName)
		{
			_helper.RenameFile(fileName);
		}

		public void DeleteFile()
		{
			_helper.DeleteFile();
		}

		#endregion

		#region FileWatcher implementation

		public void StartFileWatcher()
		{
			var fileWatcher = _helper.GetWatcher();
			fileWatcher.Changed += this.OnFileChanged;
			fileWatcher.Created += this.OnFileChanged;
			fileWatcher.Deleted += this.OnFileChanged;
			fileWatcher.Renamed += this.OnFileRenamed;
			fileWatcher.EnableRaisingEvents = true;
		}

		private void OnFileRenamed(object sender, RenamedEventArgs e)
		{
			this.FileRenamedEvent?.Invoke(sender, e);
		}

		private void OnFileChanged(object sender, FileSystemEventArgs e)
		{
			if (e.ChangeType == WatcherChangeTypes.Changed)
			{
				this.ContentAppendedEvent?.Invoke(sender, e);
			}

			if (e.ChangeType == WatcherChangeTypes.Created)
			{
				this.FileCreatedEvent?.Invoke(sender, e);
			}

			if (e.ChangeType == WatcherChangeTypes.Deleted)
			{
				this.FileDeletedEvent?.Invoke(sender, e);
			}
		}

		#endregion
	}
}
