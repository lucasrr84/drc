using System;

namespace collector.domain.service;

public interface INewFileDetector
{
    bool IsNewFile(string fileName, string substation, string bay);
}
