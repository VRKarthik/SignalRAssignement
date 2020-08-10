using System;

namespace FileMonitor.Service
{
	public interface IFileMonitorService
	{
		#region Events

		event EventHandler ContentAppendedEvent;

		event EventHandler FileCreatedEvent;

		event EventHandler FileRenamedEvent;

		event EventHandler FileDeletedEvent;

		#endregion

		#region Methods

		void Append(string contents);

		void CreateFile(string fileName);

		void RenameFile(string fileName);

		void DeleteFile();

		void StartFileWatcher();

		#endregion
	}
}
