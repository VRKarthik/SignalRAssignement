using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileMonitor.Service
{
	public class DirectoryHelper
	{
		#region Global declarations

		private readonly string _directoryPath;

		#endregion

		#region Constructor

		public DirectoryHelper(string directoryPath)
		{
			_directoryPath = directoryPath;
		}

		#endregion

		#region Private methods

		private string[] GetAllFiles()
		{
			if (!Directory.Exists(_directoryPath))
				return new string[] { };

			return Directory.GetFiles(_directoryPath);
		}

		#endregion

		#region Public methods

		public void AppendContents(string contents)
		{
			var firstFile = this.GetAllFiles().FirstOrDefault();
			if (!string.IsNullOrWhiteSpace(firstFile))
			{
				if (File.Exists(firstFile))
				{
					using (var writer = File.AppendText(firstFile))
					{
						writer.WriteLine(contents);
					}
				}
			}
		}

		public void CreateFile(string fileName)
		{
			var fullFileName = Path.Combine(_directoryPath, fileName);
			if (File.Exists(fullFileName))
				File.Delete(fullFileName);

			using (var fileStream = File.Create(fullFileName))
			{
				byte[] contents = new UTF8Encoding(true).GetBytes(DateTime.Now.ToString());
				fileStream.Write(contents, 0, contents.Length);
			}
		}

		public void RenameFile(string fileName)
		{
			var firstFile = this.GetAllFiles().FirstOrDefault();
			if (!string.IsNullOrWhiteSpace(firstFile))
			{
				if (File.Exists(firstFile))
				{
					var destinationFileName = Path.Combine(_directoryPath, fileName);
					File.Move(firstFile, destinationFileName);
				}
			}
		}

		public void DeleteFile()
		{
			var firstFile = this.GetAllFiles().FirstOrDefault();
			if (!string.IsNullOrWhiteSpace(firstFile))
			{
				if (File.Exists(firstFile))
				{
					File.Delete(firstFile);
				}
			}
		}

		public FileSystemWatcher GetWatcher()
		{
			var fileWatcher = new FileSystemWatcher(_directoryPath);
			fileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
			return fileWatcher;
		}

		#endregion
	}
}
