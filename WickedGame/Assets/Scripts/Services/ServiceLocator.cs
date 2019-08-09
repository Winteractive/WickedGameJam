using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{

    public static class ServiceLocator
    {
        private static IAudioService audioProvider;
        private static IDebugService DebugProvider;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            audioProvider = new NullAudioProvider();
            DebugProvider = new NullDebugProvider();
        }

        public static IAudioService GetAudioProvider()
        {
            return audioProvider;
        }

        public static IDebugService GetDebugProvider()
        {
            return DebugProvider;
        }

        public static void SetAudioProvider(IAudioService provider)
        {
            audioProvider = provider;
        }


        public static void SetDebugProvider(IDebugService provider)
        {
            DebugProvider = provider;
        }
    }
}

