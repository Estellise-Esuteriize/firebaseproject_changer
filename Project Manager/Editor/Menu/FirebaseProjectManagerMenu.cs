using System;
using System.IO;
using Packages.FirebaseProjectManager.Project_Manager.Editor.Core;
using UnityEditor;
using UnityEngine;

namespace Packages.FirebaseProjectManager.Project_Manager.Editor.Menu
{
    public class FirebaseProjectManagerMenu
    {

        private const string ANDROID_JSON = "google-services.json";
        private const string IOS_PLIST = "GoogleService-Info.plist";

        private const string ANDROID_JSON_FILE_DEBUG = "google-services_debug.json";
        private const string ANDROID_JSON_FILE_RELEASE = "google-services_release.json";
        
        private const string IOS_PLIST_FILE_DEBUG = "GoogleService-Info_debug.plist";
        private const string IOS_PLIST_FILE_RELEASE = "GoogleService-Info_release.plist";
        

        [MenuItem("Assets/FirebaseProject/Debug")]
        public static void SetDebug()
        {
            ThrowIfEditorPlaying();
            
            var androidServices = new GoogleServicesChanger(ANDROID_JSON, ANDROID_JSON_FILE_DEBUG);
            var iosServices = new GoogleServicesChanger(IOS_PLIST, IOS_PLIST_FILE_DEBUG);
            
            androidServices.ReplaceGoogleServices();
            iosServices.ReplaceGoogleServices();
            
            Debug.Log("Set Debug Json File : " + Path.Combine(Application.dataPath, ANDROID_JSON_FILE_DEBUG));
            Debug.Log("Set Debug Ios File : " + Path.Combine(Application.dataPath, IOS_PLIST_FILE_DEBUG));
            
            AssetDatabase.Refresh();
        }


        [MenuItem("Assets/FirebaseProject/Release")]
        public static void SetRelease()
        {
            ThrowIfEditorPlaying();
            
            var androidServices = new GoogleServicesChanger(ANDROID_JSON, ANDROID_JSON_FILE_RELEASE);
            var iosServices = new GoogleServicesChanger(IOS_PLIST, IOS_PLIST_FILE_RELEASE);
            
            androidServices.ReplaceGoogleServices();
            iosServices.ReplaceGoogleServices();
            
            Debug.Log("Set Release Json File : " + Path.Combine(Application.dataPath, ANDROID_JSON_FILE_RELEASE));
            Debug.Log("Set Release Ios File : " + Path.Combine(Application.dataPath, IOS_PLIST_FILE_RELEASE));
            
            AssetDatabase.Refresh();
        }


        [MenuItem("Assets/FirebaseProject/CurrentProject")]
        public static void CurrentFirebaseProject()
        {
            // json
            var androidServices = new GoogleServicesChanger(ANDROID_JSON, ANDROID_JSON_FILE_RELEASE);
            var androidJson = androidServices.GetCurrentAndroidServices();
            
            Debug.Log("Android Project Id : " + androidJson.project_info.project_id);
            Debug.Log("Android Database Url : " + androidJson.project_info.firebase_url);
            Debug.Log("Android Storage Bucket : " + androidJson.project_info.storage_bucket);
           
            // ---
            Debug.Log("-----------------");
            
            
            // ios
            var iosServices = new GoogleServicesChanger(IOS_PLIST, IOS_PLIST_FILE_RELEASE);
            var iosPlist = iosServices.GetCurrentIosServices();
            
            Debug.Log("IOS Project Id : " + iosPlist.PROJECT_ID);
            Debug.Log("IOS Database Url : " + iosPlist.DATABASE_URL);
            Debug.Log("IOS Storage Bucket : " + iosPlist.STORAGE_BUCKET);
        }

        private static void ThrowIfEditorPlaying()
        {
            if (Application.isPlaying)
            {
                throw new OperationCanceledException("Unity editor is currently playing.");
            }
        }

    }
}