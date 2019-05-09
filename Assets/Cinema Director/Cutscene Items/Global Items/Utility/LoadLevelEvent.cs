using UnityEngine;
using UnityEngine.SceneManagement;

namespace CinemaDirector
{
    /// <summary>
    /// Event for loading a new level
    /// </summary>
    [CutsceneItem("Utility", "Load Level", CutsceneItemGenre.GlobalItem)]
    public class LoadLevelEvent : CinemaGlobalEvent
    {
        public enum LoadLevelArgument
        {
            ByIndex,
            ByName,
        }

        public enum LoadLevelType
        {
            Standard,
            Additive,
            Async,
            AdditiveAsync,
        }

        public LoadLevelArgument Argument = LoadLevelArgument.ByIndex;
        public LoadLevelType Type = LoadLevelType.Standard;

        // The index of the level to be loaded.
        public int Level = 0;

        // The name of the level to be loaded.
        public string LevelName;

        /// <summary>
        /// Trigger the level load. Only in Runtime.
        /// </summary>
        public override void Trigger()
        {
            if (Application.isPlaying)
            {
                if (Argument == LoadLevelArgument.ByIndex)
                {
                    if (Type == LoadLevelType.Standard)
                    {
                        SceneManager.LoadScene(Level);
                    }
                    else if (Type == LoadLevelType.Additive)
                    {
                        SceneManager.LoadScene(Level);
                    }
                    else if (Type == LoadLevelType.Async)
                    {
                        SceneManager.LoadSceneAsync(Level);
                    }
                    else if (Type == LoadLevelType.AdditiveAsync)
                    {
                        SceneManager.LoadSceneAsync(Level);
                    }
                }
                else if (Argument == LoadLevelArgument.ByName)
                {
                    if (Type == LoadLevelType.Standard)
                    {
                        SceneManager.LoadScene(LevelName);
                    }
                    else if (Type == LoadLevelType.Additive)
                    {
                        SceneManager.LoadScene(LevelName);
                    }
                    else if (Type == LoadLevelType.Async)
                    {
                        SceneManager.LoadSceneAsync(LevelName);
                    }
                    else if (Type == LoadLevelType.AdditiveAsync)
                    {
                        SceneManager.LoadSceneAsync(LevelName);
                    }
                }
            }
        }
    }
}