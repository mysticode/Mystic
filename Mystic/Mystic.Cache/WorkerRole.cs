/*
 * Copyright (C) 2012 The Mysticode Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace Mystic.Cache
{
    public class WorkerRole : RoleEntryPoint
    {
        // No further implementation is required to support a cache worker role. 
        // Additional functionality may affect the performance of the cache service. 
        // For information on the dedicated cache worker role and the cache service 
        // see the MSDN documentation at http://go.microsoft.com/fwlink/?LinkID=247285
    }
}
