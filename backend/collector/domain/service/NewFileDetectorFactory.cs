using System;

namespace collector.domain.service;

public class NewFileDetectorFactory
{
    public static INewFileDetector create(string newFileDetectorType)
    {
        return newFileDetectorType switch
        {
            "existingFile" => new ExistingNewFileDetector(),

            _ => new ExistingNewFileDetector(),
        };
    }

}
