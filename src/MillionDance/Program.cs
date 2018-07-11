﻿using System.IO;
using JetBrains.Annotations;
using MillionDance.Entities.Unity;
using UnityStudio.Extensions;
using UnityStudio.Models;
using UnityStudio.Serialization;

namespace MillionDance {
    internal static class Program {

        private static void Main([NotNull, ItemNotNull] string[] args) {
            var (dan, _, _) = LoadDance();
            var vmd = VmdCreator.FromDanceData(dan);

            using (var w = new VmdWriter(File.Open("out.vmd", FileMode.Create, FileAccess.Write, FileShare.Write))) {
                w.Write(vmd);
            }
        }

        private static (DanceData, DanceData, DanceData) LoadDance() {
            DanceData dan = null, apa = null, apg = null;

            var ser = new MonoBehaviourSerializer();

            using (var fileStream = File.Open("Resources/dan_shtstr_01_dan.imo.unity3d", FileMode.Open, FileAccess.Read, FileShare.Read)) {
                using (var bundle = new BundleFile(fileStream, false)) {
                    foreach (var assetFile in bundle.AssetFiles) {
                        foreach (var preloadData in assetFile.PreloadDataList) {
                            if (preloadData.KnownType != KnownClassID.MonoBehaviour) {
                                continue;
                            }

                            var behaviour = preloadData.LoadAsMonoBehaviour(true);

                            switch (behaviour.Name) {
                                case "dan_shtstr_01_dan.imo":
                                    behaviour = preloadData.LoadAsMonoBehaviour(false);
                                    dan = ser.Deserialize<DanceData>(behaviour);
                                    break;
                                case "dan_shtstr_01_apa.imo":
                                    behaviour = preloadData.LoadAsMonoBehaviour(false);
                                    apa = ser.Deserialize<DanceData>(behaviour);
                                    break;
                                case "dan_shtstr_01_apg.imo":
                                    behaviour = preloadData.LoadAsMonoBehaviour(false);
                                    apg = ser.Deserialize<DanceData>(behaviour);
                                    break;
                            }

                            if (dan != null && apa != null && apg != null) {
                                break;
                            }
                        }
                    }
                }
            }

            return (dan, apa, apg);
        }


    }
}