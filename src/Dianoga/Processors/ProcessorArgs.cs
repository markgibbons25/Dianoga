﻿using Sitecore.Pipelines;
using System.IO;
using Sitecore.Resources.Media;

namespace Dianoga.Processors
{
	public class ProcessorArgs : PipelineArgs
	{
		public MediaStream InputStream { get; }

		public bool IsOptimized { get; set; }

		public Stream ResultStream { get; set; }

		public ProcessorArgsStatistics Statistics { get; }

		public ProcessorArgs(MediaStream inputStream)
		{
			IsOptimized = false;
			InputStream = inputStream;
			Statistics = new ProcessorArgsStatistics(this);
		}

		public class ProcessorArgsStatistics
		{
			private readonly ProcessorArgs _args;

			internal ProcessorArgsStatistics(ProcessorArgs args)
			{
				_args = args;
			}

			public long SizeBefore => _args.InputStream.Length;
			public long SizeAfter => _args.ResultStream?.Length ?? SizeBefore;
			public float PercentageSaved => 1 - ((SizeAfter/(float) SizeBefore));
			public long BytesSaved => SizeBefore - SizeAfter;
		}
	}
}