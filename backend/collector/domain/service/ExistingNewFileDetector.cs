using collector.domain.config;

namespace collector.domain.service;

public class ExistingNewFileDetector : INewFileDetector
{
    public bool IsNewFile(string fileName, string substation, string bay)
    {
        return !File.Exists($@"{Config.ServerPathDirectory}{substation}{bay}\{fileName}");
    }
}