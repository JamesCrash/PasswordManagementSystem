using Prism.Regions;
using System.Windows;

namespace CrashPasswordSystem.UI
{
    public static class Utils
    {
        public static bool IsActiveOnTargetRegion(this FrameworkElement instance, IRegionManager regionManager, string targetRegion)
        {
            return regionManager.Regions[targetRegion].Views.Contains(instance);
        }

        public static void RemoveFromRegion(this FrameworkElement instance, string targetRegion, IRegionManager regionManager)
        {
            regionManager.Regions[targetRegion].Remove(instance);
        }
    }
}
