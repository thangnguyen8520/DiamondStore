using DiamondStoreService.Interfaces;
using Firebase.Storage;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Utils
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseSettings _firebaseSettings;
        private readonly FirebaseStorage _firebaseStorage;

        public FirebaseService(IOptions<FirebaseSettings> firebaseSettings)
        {
            _firebaseSettings = firebaseSettings.Value;

            var credential = GoogleCredential.FromFile(_firebaseSettings.CredentialsPath);
            _firebaseStorage = new FirebaseStorage(
                _firebaseSettings.StorageBucket1 + _firebaseSettings.StorageBucket2,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(string.Empty),
                    ThrowOnCancel = true
                });
        }

        public async Task<string> UploadImageAsync(string fileName, Stream stream)
        {
            try
            {
                var task = _firebaseStorage
                    .Child("images")
                    .Child(fileName)
                    .PutAsync(stream);

                var downloadUrl = await task;
                return downloadUrl;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading image to Firebase: {ex.Message}");
            }
        }
    }

    public class FirebaseSettings
    {
        public string ProjectId1 { get; set; }
        public string ProjectId2 { get; set; }
        public string StorageBucket1 { get; set; }
        public string StorageBucket2 { get; set; }
        public string CredentialsPath { get; set; }
    }
}
