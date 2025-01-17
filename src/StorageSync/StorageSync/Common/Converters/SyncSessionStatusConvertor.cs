﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.StorageSync.Models;
using System;
using System.Collections.Generic;
using StorageSyncModels = Microsoft.Azure.Management.StorageSync.Models;

namespace Microsoft.Azure.Commands.StorageSync.Common.Converters
{

    /// <summary>
    /// Class SyncSessionStatusConvertor.
    /// Implements the <see cref="Converters.ConverterBase{PSSyncSessionStatus, StorageSyncModels.SyncSessionStatus}" />
    /// </summary>
    /// <seealso cref="Converters.ConverterBase{PSSyncSessionStatus, StorageSyncModels.SyncSessionStatus}" />
    public class SyncSessionStatusConvertor : ConverterBase<PSSyncSessionStatus, StorageSyncModels.ServerEndpointSyncSessionStatus>
    {
        /// <summary>
        /// Transforms the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>StorageSyncModels.ServerEndpointSyncSessionStatus.</returns>
        protected override StorageSyncModels.ServerEndpointSyncSessionStatus Transform(PSSyncSessionStatus source)
        {
            // Sync activity properties are read-only from the RP
            throw new NotSupportedException();
        }

        /// <summary>
        /// Transforms the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>PSSyncSessionStatus.</returns>
        protected override PSSyncSessionStatus Transform(StorageSyncModels.ServerEndpointSyncSessionStatus source)
        {
            var filesNotSyncingErrors = new List<PSServerEndpointFilesNotSyncingError>();
            if (source.FilesNotSyncingErrors != null)
            {
                foreach (var error in source.FilesNotSyncingErrors)
                {
                    filesNotSyncingErrors.Add(new PSServerEndpointFilesNotSyncingError
                    {
                        ErrorCode = error.ErrorCode,
                        PersistentCount = error.PersistentCount,
                        TransientCount = error.TransientCount
                    });
                }
            }

            return new PSSyncSessionStatus()
            {
                LastSyncResult = source.LastSyncResult,
                LastSyncTimestamp = source.LastSyncTimestamp,
                LastSyncSuccessTimestamp = source.LastSyncSuccessTimestamp,
                LastSyncPerItemErrorCount = source.LastSyncPerItemErrorCount,
                LastSyncMode = source.LastSyncMode,
                PersistentFilesNotSyncingCount = source.PersistentFilesNotSyncingCount,
                TransientFilesNotSyncingCount = source.TransientFilesNotSyncingCount,
                FilesNotSyncingErrors = filesNotSyncingErrors
            };
        }
    }

}