using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreAscents;

public static class AscentGimmickHandler {
    internal static readonly List<AscentGimmick> gimmicks = new();

    internal static void MarkGimmicksAsActive(int ascentIndex) {
        int index = 0;
        foreach (AscentData.AscentInstanceData data in AscentData.Instance.ascents) {
            AscentGimmick gimmick = GetGimmickByData(data);
            if (index <= ascentIndex && gimmick != null) {
                gimmick.active = true;
                Plugin.Logger.LogWarning($"enabled gimmick {gimmick.GetType().Name} [{gimmick.GetTitle()}]!");
            }
            index++;
        }
    }

    public static AscentGimmick GetGimmickByData(AscentData.AscentInstanceData data) {
        foreach (AscentGimmick gimmick in gimmicks) {
            if (gimmick._ascentData.data == data) {
                return gimmick;
            }
        }
        return null;
    }

    internal static void DisableGimmicks() {
        foreach (AscentGimmick gimmick in gimmicks) {
            gimmick.active = false;
        }
    }

    internal static int BaseAscents {
        get;
        private set;
    } = -1;

    internal static int GetBaseAscentCount() {
        if (BaseAscents == -1) {
            BaseAscents = AscentData.Instance.ascents.Count;
        }
        return BaseAscents;
    }

    private static readonly List<AscentData.AscentInstanceData> pendingDatas = [];
    private static bool HasInitialized = false;

    internal static void Initialize() {
        if (HasInitialized) {
            Plugin.Logger.LogError($"already initialized");
            return;
        }

        HasInitialized = true;

        AscentData ascentData = AscentData.Instance;

        List<AscentData.AscentInstanceData> originalDatas = ascentData.ascents;
        List<AscentData.AscentInstanceData> newDatas = [];

        Plugin.Logger.LogInfo($"initializing with ({pendingDatas.Count} custom) and ({originalDatas.Count} base)");

        foreach (AscentData.AscentInstanceData data in originalDatas) {
            HotLoadAscentInstanceData(newDatas, data);
        }

        // customs
        foreach (AscentData.AscentInstanceData data in pendingDatas) {
            HotLoadAscentInstanceData(newDatas, data);
        }

        ascentData.ascents = newDatas;
    }

    private static void HotLoadAscentInstanceData(List<AscentData.AscentInstanceData> targetList, AscentData.AscentInstanceData data) {
        if (data.titleReward == "") {
            data.titleReward = $"{data.title} Conqueror";
        }
        targetList.Add(data);
    }

    public static void RegisterAscent<T>() where T : AscentGimmick {
        AscentGimmick gimmick = Activator.CreateInstance<T>();
        /*if (gimmick.GetTitle() == "") {
            newData.title = $"Ascent {7 + gimmicks.Count+1}";
        }*/

        AscentData.AscentInstanceData newData = new() {
            title = gimmick.GetTitle(),
            description = gimmick.GetDescription(),
            titleReward = gimmick.GetTitleReward(),
            color = gimmick.GetColor(),
            sashSprite = AscentData.Instance.ascents[0].sashSprite,
        };

        gimmick._ascentData = new AscentStruct {
            data = newData,
            order = 9 + (gimmicks.Count + 1),
        };
        gimmicks.Add(gimmick);

        if (HasInitialized) {
            HotLoadAscentInstanceData(AscentData.Instance.ascents, newData);
            Plugin.Logger.LogInfo($"Hot loaded {newData.title}!");
            return;
        }
        pendingDatas.Add(newData);
        Plugin.Logger.LogInfo($"Queued {newData.title}");

        void AddIdentityTranslation(string key, string val) {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new NotImplementedException($"translation was empty?? {typeof(T)}");
            }
            LocalizedText.mainTable.Add(key.ToUpperInvariant(), Enumerable.Repeat(val.ToUpperInvariant(), LocalizedText.LANGUAGE_COUNT).ToList());
        }

        AddIdentityTranslation(gimmick.GetTitle(), gimmick.GetTitle());
        AddIdentityTranslation($"desc_{gimmick.GetTitle()}", gimmick.GetDescription());
    }
}