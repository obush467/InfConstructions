﻿namespace InfConstructionsDX.Common
{
    public interface IDocumentModule
    {
        string Caption { get; }
        bool IsActive { get; set; }
    }
}
