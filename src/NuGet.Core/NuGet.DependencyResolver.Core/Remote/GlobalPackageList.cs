// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using NuGet.Versioning;

namespace NuGet.DependencyResolver.Core
{
    internal class GlobalPackageList
    {
        private Dictionary<string, NuGetVersion> _packageNameToVersion = new Dictionary<string, NuGetVersion>(StringComparer.OrdinalIgnoreCase);

        public bool AddOrUpdatePackage(string packageName, VersionRange versionRange)
        {
            lock (_packageNameToVersion)
            {
                if (!_packageNameToVersion.TryGetValue(packageName, out NuGetVersion version))
                {
                    _packageNameToVersion.Add(packageName, versionRange.MinVersion);
                    return true;
                }
                else
                {
                    if(version < versionRange.MinVersion)
                    {
                        _packageNameToVersion[packageName] = versionRange.MinVersion;
                    }
                    return false;
                }
            }
        }
    }
}
